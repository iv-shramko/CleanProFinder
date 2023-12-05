using CleanProFinder.Db.DbContexts;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Server.Hubs.Notifiers;
using CleanProFinder.Shared.Dto.Notifications;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.Enums;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.Requests
{
    public class AcceptProviderForRequestCommand : AcceptProviderForRequestDto, IRequest<ServiceResponse>
    {
        public class AcceptProviderForRequestCommandHandler : BaseHandler<AcceptProviderForRequestCommand, ServiceResponse>
        {
            private readonly ILogger<AcceptProviderForRequestCommandHandler> _logger;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _context;
            private readonly RequestNotifier _notifier;

            public AcceptProviderForRequestCommandHandler(
                ILogger<AcceptProviderForRequestCommandHandler> logger,
                IHttpContextAccessor contextAccessor,
                ApplicationDbContext context,
                RequestNotifier notifier)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _context = context;
                _notifier = notifier;
            }

            public override async Task<ServiceResponse> Handle(AcceptProviderForRequestCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(CreateRequestCommand), ex);
                    return ServiceResponseBuilder.Failure(ServerError.AcceptProviderForRequestError);
                }
            }

            private async Task<ServiceResponse> UnsafeHandleAsync(
                AcceptProviderForRequestCommand command,
                CancellationToken cancellationToken)
            {
                var validUserId = _contextAccessor.TryGetUserId(out var userId);
                if (validUserId is false)
                {
                    return ServiceResponseBuilder.Failure(UserError.InvalidAuthorization);
                }

                var serviceUser = await _context.CleaningServiceProviders.FirstOrDefaultAsync(u => u.Id == userId);

                if (serviceUser.IsRestricted is true)
                {
                    return ServiceResponseBuilder.Failure(UserError.UserIsRestricted);
                }

                var requestInteraction = await _context
                    .RequestInteractions
                    .Include(rI => rI.Request)
                    .FirstOrDefaultAsync(rI => rI.ProviderId == command.ProviderId && rI.RequestId == command.RequestId, cancellationToken);

                if (requestInteraction is null)
                {
                    return ServiceResponseBuilder.Failure(RequestError.InvalidInteraction);
                }


                await _context
                    .RequestInteractions
                    .Where(rI => rI.RequestId == requestInteraction.RequestId)
                    .ForEachAsync(requestInteraction =>
                    {
                        requestInteraction.InteractionStatus = RequestInteractionStatus.Declined;
                    });
                
                requestInteraction.InteractionStatus = RequestInteractionStatus.Accepted;

                await _context.SaveChangesAsync(cancellationToken);

                //TODO: Notify provider
                return ServiceResponseBuilder.Success();
            }
        }
    }
}
