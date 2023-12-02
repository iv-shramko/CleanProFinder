using CleanProFinder.Db.DbContexts;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
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

            public AssignForRequestCommandHandler(
                ILogger<AssignForRequestCommandHandler> logger,
                IHttpContextAccessor contextAccessor,
                ApplicationDbContext context)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _context = context;
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
                };

                _context.RequestInteractions.Add(interaction);

                if(request.Status == RequestStatus.Placed || request.Status == RequestStatus.HasAnswers)
                {
                    request.Status = RequestStatus.HasAnswers;
                }

                await _context.SaveChangesAsync(cancellationToken);

                return ServiceResponseBuilder.Success();
            }
        }
    }
}
