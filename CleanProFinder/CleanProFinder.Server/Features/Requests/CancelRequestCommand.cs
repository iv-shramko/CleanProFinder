using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Server.Features.Premises;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static CleanProFinder.Server.Features.Premises.DeletePremiseCommand;

namespace CleanProFinder.Server.Features.Requests
{
    public class CancelRequestCommand : IRequest<ServiceResponse>
    {
        public Guid RequestId { get; set; }

        public class CancelRequestCommandHandler : BaseHandler<CancelRequestCommand, ServiceResponse>
        {
            private readonly ILogger<DeletePremiseCommandHandler> _logger;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _context;

            public CancelRequestCommandHandler(ILogger<DeletePremiseCommandHandler> logger, IHttpContextAccessor contextAccessor, ApplicationDbContext context)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _context = context;
            }

            public override async Task<ServiceResponse> Handle(CancelRequestCommand request, CancellationToken cancellationToken)
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

            public async Task<ServiceResponse> UnsafeHandleAsync(CancelRequestCommand request, CancellationToken cancellationToken)
            {
                var validUserId = _contextAccessor.TryGetUserId(out var userId);
                if (validUserId is false)
                {
                    return ServiceResponseBuilder.Failure(UserError.InvalidAuthorization);
                }

                var serviceRequest = await _context.Requests
                    .Include(r => r.Premise)
                    .FirstOrDefaultAsync(r => r.Id == request.RequestId);

                if (serviceRequest is null)
                {
                    return ServiceResponseBuilder.Failure<RequestFullInfoDto>(RequestError.InvalidId);
                }

                if (serviceRequest.Premise.UserId != userId)
                {
                    return ServiceResponseBuilder.Failure<RequestFullInfoDto>(RequestError.NotRequestOwner);

                }

                serviceRequest.Status = Shared.Enums.RequestStatus.Canceled;
                await _context.SaveChangesAsync();

                return ServiceResponseBuilder.Success();

            }


        }
    }
}
