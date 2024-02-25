using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.ProviderRestrictions
{
    public class ChangeServiceProviderRestrictionsCommand : IRequest<ServiceResponse>
    {
        public Guid ServiceProviderId { get; set; }
        public class ChangeServiceProviderRestrictionsCommandHandler : BaseHandler<ChangeServiceProviderRestrictionsCommand, ServiceResponse>
        {
            private readonly ILogger<ChangeServiceProviderRestrictionsCommandHandler> _logger;
            private readonly ApplicationDbContext _context;

            public ChangeServiceProviderRestrictionsCommandHandler(ILogger<ChangeServiceProviderRestrictionsCommandHandler> logger, ApplicationDbContext context)
            {
                _logger = logger;
                _context = context;
            }

            public override async Task<ServiceResponse> Handle(ChangeServiceProviderRestrictionsCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(ChangeServiceProviderRestrictionsCommand), ex);
                    return ServiceResponseBuilder.Failure(ServerError.EditProviderRestrictions);
                }
            }

            public async Task<ServiceResponse> UnsafeHandleAsync(ChangeServiceProviderRestrictionsCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.CleaningServiceProviders.FirstOrDefaultAsync(u => u.Id == request.ServiceProviderId);
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
