using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.Profile
{
    public class ViewUserProfileInfoQuery : ViewUserProfileInfoDto, IRequest<ServiceResponse<ViewUserProfileInfoDto>>
    {
        public class UserProfileViewInfoQueryHandler : IRequestHandler<ViewUserProfileInfoQuery,
    ServiceResponse<ViewUserProfileInfoDto>>
        {
            private readonly IMediator _mediator;
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<UserProfileViewInfoQueryHandler> _logger;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UserProfileViewInfoQueryHandler(IMediator mediator,
                    ApplicationDbContext context,
                    IMapper mapper,
                    ILogger<UserProfileViewInfoQueryHandler> logger,
                    IHttpContextAccessor httpContextAccessor)
            {
                _mediator = mediator;
                _context = context;
                _mapper = mapper;
                _logger = logger;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ServiceResponse<ViewUserProfileInfoDto>> Handle(ViewUserProfileInfoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "view profile info error");
                    return ServiceResponseBuilder.Failure<ViewUserProfileInfoDto>(ProfileInfoError.ViewProfileInfoError);
                }
            }

            public async Task<ServiceResponse<ViewUserProfileInfoDto>> UnsafeHandleAsync(ViewUserProfileInfoQuery request, CancellationToken cancellationToken)
            {
                var userIdRetrieved = _httpContextAccessor.TryGetUserId(out var userId);

                if (userIdRetrieved is false)
                {
                    return ServiceResponseBuilder.Failure<ViewUserProfileInfoDto>(UserError.InvalidAuthorization);
                }

                var user = await _context.ServiceUsers.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

                if (user is null)
                {
                    return ServiceResponseBuilder.Failure<ViewUserProfileInfoDto>(UserError.UserNotFound);
                }

                var result = _mapper.Map<ViewUserProfileInfoDto>(user);

                return ServiceResponseBuilder.Success(result);
            }
        }
       
    }
}
