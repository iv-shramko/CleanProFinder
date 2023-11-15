using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Db.Models;
using CleanProFinder.Shared.Dto.CleaningServices;

namespace CleanProFinder.Server.Features.Profile
{
    public class GetProviderProfileViewInfoQuery : ProviderProfileViewInfoDto, IRequest<ServiceResponse<ProviderProfileViewInfoDto>>
    {
        public class ProviderProfileViewInfoQueryHandler : IRequestHandler<GetProviderProfileViewInfoQuery,
    ServiceResponse<ProviderProfileViewInfoDto>>
        {
            private readonly IMediator _mediator;
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<ProviderProfileViewInfoQueryHandler> _logger;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public ProviderProfileViewInfoQueryHandler(IMediator mediator,
                    ApplicationDbContext context,
                    IMapper mapper,
                    ILogger<ProviderProfileViewInfoQueryHandler> logger,
                    IHttpContextAccessor httpContextAccessor)
            {
                _mediator = mediator;
                _context = context;
                _mapper = mapper;
                _logger = logger;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ServiceResponse<ProviderProfileViewInfoDto>> Handle(GetProviderProfileViewInfoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "view profile info error");
                    return ServiceResponseBuilder.Failure<ProviderProfileViewInfoDto>(ProfileInfoError.ViewProfileInfoError);
                }
            }

            public async Task<ServiceResponse<ProviderProfileViewInfoDto>> UnsafeHandleAsync(GetProviderProfileViewInfoQuery request, CancellationToken cancellationToken)
            {
                var userIdRetrieved = _httpContextAccessor.TryGetUserId(out var userId);

                if (userIdRetrieved is false)
                {
                    return ServiceResponseBuilder.Failure<ProviderProfileViewInfoDto>(UserError.InvalidAuthorization);
                }

                var user = await _context.CleaningServiceProviders.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

                if (user is null)
                {
                    return ServiceResponseBuilder.Failure<ProviderProfileViewInfoDto>(UserError.UserNotFound);
                }

                var result = _mapper.Map<ProviderProfileViewInfoDto>(user);

                var services = await _context.CleaningServiceServiceProviders
                    .Where(sD => sD.CleaningServiceProviderId == userId)
                    .Include(sD => sD.CleaningService)
                    .ToListAsync(cancellationToken);

                result.Services = _mapper.Map<List<ProviderServiceFullInfoDto>>(services);


                return ServiceResponseBuilder.Success(result);
            }
        }

    }
}
