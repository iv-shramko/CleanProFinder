using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.Profile
{
    public class GetProviderProfileByIdQuery : ProviderProfileViewInfoDto, IRequest<ServiceResponse<ProviderProfileViewInfoDto>>
    {
        public Guid ServiceProviderId { get; set; }
        public class ProviderProfileViewInfoQueryHandler : IRequestHandler<GetProviderProfileByIdQuery,
    ServiceResponse<ProviderProfileViewInfoDto>>
        {
            private readonly IMediator _mediator;
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<ProviderProfileViewInfoQueryHandler> _logger;

            public ProviderProfileViewInfoQueryHandler(IMediator mediator,
                    ApplicationDbContext context,
                    IMapper mapper,
                    ILogger<ProviderProfileViewInfoQueryHandler> logger)
            {
                _mediator = mediator;
                _context = context;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<ServiceResponse<ProviderProfileViewInfoDto>> Handle(GetProviderProfileByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "view provider profile info error");
                    return ServiceResponseBuilder.Failure<ProviderProfileViewInfoDto>(ProfileInfoError.ViewProfileInfoError);
                }
            }

            public async Task<ServiceResponse<ProviderProfileViewInfoDto>> UnsafeHandleAsync(GetProviderProfileByIdQuery request, CancellationToken cancellationToken)
            {


                var provider = await _context.CleaningServiceProviders.FirstOrDefaultAsync(x => x.Id == request.ServiceProviderId, cancellationToken);

                if (provider is null)
                {
                    return ServiceResponseBuilder.Failure<ProviderProfileViewInfoDto>(UserError.UserNotFound);
                }

                var result = _mapper.Map<ProviderProfileViewInfoDto>(provider);

                var services = await _context.CleaningServiceServiceProviders
                    .Where(sD => sD.CleaningServiceProviderId == request.ServiceProviderId)
                    .Include(sD => sD.CleaningService)
                    .ToListAsync(cancellationToken);

                result.Services = _mapper.Map<List<ProviderServiceFullInfoDto>>(services);


                return ServiceResponseBuilder.Success(result);
            }
        }

    }
}
