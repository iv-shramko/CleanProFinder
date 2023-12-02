using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.UserRestrictions
{
    public class ChangeServiceUserRestrictionsCommand : IRequest<ServiceResponse>
    {
        public Guid ServiceUserId { get; set; }
        public class ChangeServiceUserRestrictionsCommandHandler : BaseHandler<ChangeServiceUserRestrictionsCommand, ServiceResponse>
        {
            private readonly ILogger<ChangeServiceUserRestrictionsCommandHandler> _logger;
            private readonly ApplicationDbContext _context;

            public ChangeServiceUserRestrictionsCommandHandler(ILogger<ChangeServiceUserRestrictionsCommandHandler> logger, ApplicationDbContext context)
            {
                _logger = logger;
                _context = context;
            }

            public override async Task<ServiceResponse> Handle(ChangeServiceUserRestrictionsCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(ChangeServiceUserRestrictionsCommand), ex);
                    return ServiceResponseBuilder.Failure(ServerError.EditUserRestrictions);
                }
            }

            public async Task<ServiceResponse> UnsafeHandleAsync(ChangeServiceUserRestrictionsCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.ServiceUsers.FirstOrDefaultAsync(u => u.Id == request.ServiceUserId);
                if (user is null)
                {
                    return ServiceResponseBuilder.Failure(UserError.UserNotFound);
                }

                user.IsRestricted = !user.IsRestricted;
                await _context.SaveChangesAsync(cancellationToken);
                return ServiceResponseBuilder.Success();
            }
        }
    }
}
