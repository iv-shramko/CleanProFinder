using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.Profile
{
    public class AddServiceToProviderCommand : AddProviderServiceDto, IRequest<ServiceResponse<AddProviderServiceDto>>
    {
        public class AddServiceToProviderCommandHandler : IRequestHandler<AddServiceToProviderCommand,
                    ServiceResponse<AddProviderServiceDto>>
        {
            private readonly IMediator _mediator;
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<AddServiceToProviderCommandHandler> _logger;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public AddServiceToProviderCommandHandler(IMediator mediator,
                ApplicationDbContext context,
                IMapper mapper,
                ILogger<AddServiceToProviderCommandHandler> logger,
                IHttpContextAccessor httpContextAccessor)
            {
                _mediator = mediator;
                _context = context;
                _mapper = mapper;
                _logger = logger;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ServiceResponse<AddProviderServiceDto>> Handle(AddServiceToProviderCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "add service to provider error");
                    return ServiceResponseBuilder.Failure<AddProviderServiceDto>(CleaningServiceError.AddServiceToProvider);
                }
            }

            public async Task<ServiceResponse<AddProviderServiceDto>> UnsafeHandleAsync(AddServiceToProviderCommand request, CancellationToken cancellationToken)
            {
                var providerIdRetrieved = _httpContextAccessor.TryGetUserId(out var providerId);

                if (providerIdRetrieved is false)
                {
                    return ServiceResponseBuilder.Failure<AddProviderServiceDto>(UserError.InvalidAuthorization);
                }

                var existingRelation = await _context.CleaningServiceServiceProviders.FirstOrDefaultAsync(cSSP => cSSP.CleaningServiceProviderId == providerId && cSSP.CleaningServiceId == request.CleaningServiceId);

                if(!(existingRelation is null))
                {
                    return ServiceResponseBuilder.Failure<AddProviderServiceDto>(CleaningServiceError.ServiceAlreadyAdded);
                }

                var serviceProviderRelation = new CleaningServiceServiceProvider()
                {
                    CleaningServiceProviderId = providerId,
                    CleaningServiceId = request.CleaningServiceId,
                    Price = request.Price
                };

                await _context.CleaningServiceServiceProviders.AddAsync(serviceProviderRelation);
                await _context.SaveChangesAsync(cancellationToken);

                var service = await _context.CleaningServices.FirstOrDefaultAsync(s => s.Id == request.CleaningServiceId);

                if (service is null)
                {
                    return ServiceResponseBuilder.Failure<AddProviderServiceDto>(CleaningServiceError.InvaidServiceId);
                }

                var serviceDto = _mapper.Map<AddProviderServiceDto>(service);
                 _mapper.Map(serviceProviderRelation, serviceDto);

                return ServiceResponseBuilder.Success(serviceDto);
            }
        }
    }
}
