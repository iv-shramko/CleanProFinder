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
    public class EditProviderServicesCommand : EditProviderServicesDto, IRequest<ServiceResponse<EditProviderServicesDto>>
    {
        public class EditProviderServicesCommandHandler : IRequestHandler<EditProviderServicesCommand,
                    ServiceResponse<EditProviderServicesDto>>
        {
            private readonly IMediator _mediator;
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<EditProviderServicesCommandHandler> _logger;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public EditProviderServicesCommandHandler(IMediator mediator,
                ApplicationDbContext context,
                IMapper mapper,
                ILogger<EditProviderServicesCommandHandler> logger,
                IHttpContextAccessor httpContextAccessor)
            {
                _mediator = mediator;
                _context = context;
                _mapper = mapper;
                _logger = logger;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ServiceResponse<EditProviderServicesDto>> Handle(EditProviderServicesCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "update provider services error");
                    return ServiceResponseBuilder.Failure<EditProviderServicesDto>(CleaningServiceError.AddServiceToProvider);
                }
            }

            public async Task<ServiceResponse<EditProviderServicesDto>> UnsafeHandleAsync(EditProviderServicesCommand request, CancellationToken cancellationToken)
            {
                var providerIdRetrieved = _httpContextAccessor.TryGetUserId(out var providerId);

                if (providerIdRetrieved is false)
                {
                    return ServiceResponseBuilder.Failure<EditProviderServicesDto>(UserError.InvalidAuthorization);
                }

                await _context.CleaningServiceServiceProviders.Where(cSSP => cSSP.CleaningServiceProviderId == providerId).ExecuteDeleteAsync(cancellationToken);

                var newServices = _mapper.Map<IList<EditProviderServiceDto>, List<CleaningServiceServiceProvider>>(request.Services);
                newServices.ForEach(nS => nS.CleaningServiceProviderId = providerId);

                await _context.CleaningServiceServiceProviders.AddRangeAsync(newServices);
                await _context.SaveChangesAsync(cancellationToken);

                var providerServices = new EditProviderServicesDto 
                { 
                    Services = request.Services 
                };

                return ServiceResponseBuilder.Success(providerServices);
            }
        }
    }
}
