using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CleanProFinder.Shared.Errors.ServiceErrors;

namespace CleanProFinder.Server.Features.Profile
{
    public class ViewProviderProfileInfoQuery : ViewProviderProfileInfoDto, IRequest<ServiceResponse<ViewProviderProfileInfoDto>>
    {
        public class ProviderProfileViewInfoQueryHandler : IRequestHandler<ViewProviderProfileInfoQuery,
    ServiceResponse<ViewProviderProfileInfoDto>>
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

            public async Task<ServiceResponse<ViewProviderProfileInfoDto>> Handle(ViewProviderProfileInfoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "view profile info error");
                    return ServiceResponseBuilder.Failure<ViewProviderProfileInfoDto>(ProfileInfoError.ViewProfileInfoError);
                }
            }

            public async Task<ServiceResponse<ViewProviderProfileInfoDto>> UnsafeHandleAsync(ViewProviderProfileInfoQuery request, CancellationToken cancellationToken)
            {
                var userIdRetrieved = _httpContextAccessor.TryGetUserId(out var userId);

                if (userIdRetrieved is false)
                {
                    return ServiceResponseBuilder.Failure<ViewProviderProfileInfoDto>(UserError.InvalidAuthorization);
                }

                var user = await _context.CleaningServiceProviders.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

                if (user is null)
                {
                    return ServiceResponseBuilder.Failure<ViewProviderProfileInfoDto>(UserError.UserNotFound);
                }

                var result = _mapper.Map<ViewProviderProfileInfoDto>(user);

                return ServiceResponseBuilder.Success(result);
            }
        }
       
    }
}
