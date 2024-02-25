using CleanProFinder.Db.DbContexts;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Server.Hubs;
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
    public class AssignForRequestCommand : AssignForRequestCommandDto, IRequest<ServiceResponse>
    {
        public class AssignForRequestCommandHandler : BaseHandler<AssignForRequestCommand, ServiceResponse>
        {
            private readonly ILogger<AssignForRequestCommandHandler> _logger;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _context;
            private readonly RequestNotifier _notifier;

            public AssignForRequestCommandHandler(
                ILogger<AssignForRequestCommandHandler> logger,
                IHttpContextAccessor contextAccessor,
                ApplicationDbContext context,
                RequestNotifier notifier)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _context = context;
                _notifier = notifier;
            }

            public override async Task<ServiceResponse> Handle(AssignForRequestCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(CreateRequestCommand), ex);
                    return ServiceResponseBuilder.Failure(ServerError.CancelRequestError);
                }
            }

            private async Task<ServiceResponse> UnsafeHandleAsync(
                AssignForRequestCommand command, 
                CancellationToken cancellationToken)
            {
                var validUserId = _contextAccessor.TryGetUserId(out var userId);
                if (validUserId is false)
                {
                    return ServiceResponseBuilder.Failure(UserError.InvalidAuthorization);
                }

                var serviceProvider = await _context.CleaningServiceProviders.FirstOrDefaultAsync(u => u.Id == userId);

                if (serviceProvider.IsRestricted is true)
                {
                    return ServiceResponseBuilder.Failure(UserError.UserIsRestricted);
                }

                var request = await _context
                    .Requests
                    .Include(r => r.Premise)
                    .FirstOrDefaultAsync(r => r.Id == command.RequestId, cancellationToken);

                if(request is null)
                {
                    return ServiceResponseBuilder.Failure(RequestError.InvalidId);
                }

                var interaction = new RequestInteraction()
                {
                    ProviderId = userId,
                    RequestId = request.Id,
                    Price = command.Price,
                    InteractionStatus = RequestInteractionStatus.Pending
                };

                _context.RequestInteractions.Add(interaction);

                var statusChanged = false;
                if(request.Status == RequestStatus.Placed || request.Status == RequestStatus.Sent)
                {
                    request.Status = RequestStatus.HasAnswers;
                    statusChanged = true;
                }

                await _context.SaveChangesAsync(cancellationToken);

                if(statusChanged)
                {
                    await NotifyStatusUpdateAsync(request);
                }

                await NotifyProviderAssignedToRequestAsync(request, userId);

                return ServiceResponseBuilder.Success();
            }

            private async Task NotifyStatusUpdateAsync(Request request)
            {
                var message = new RequestStatusChangeMessage()
                {
                    NewStatus = request.Status.ToString(),
                    RequestId = request.Id,
                    RequestPremiseAddress = request.Premise.Address
                };
                
                await _notifier.RequestStatusChangedAsync(request.Premise.UserId, message);
            }

            private async Task NotifyProviderAssignedToRequestAsync(Request request, Guid providerId)
            {
                var provider = await _context
                    .CleaningServiceProviders
                    .FirstOrDefaultAsync(p => p.Id == providerId);

                var message = new ProviderAssignedToRequestMessage()
                {
                    RequestId = request.Id,
                    RequestPremiseAddress = request.Premise.Address,
                    ProviderId = provider.Id,
                    ProviderName = provider.Name
                };

                await _notifier.ProviderAssignedToRequestAsync(request.Premise.UserId, message);
            }
        }
    }
}
