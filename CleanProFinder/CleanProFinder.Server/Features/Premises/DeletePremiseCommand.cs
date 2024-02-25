using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.Premises
{
    public class DeletePremiseCommand : IRequest<ServiceResponse>
    {
        public Guid PremiseId { get; set; }

        public class DeletePremiseCommandHandler : BaseHandler<DeletePremiseCommand, ServiceResponse>
        {
            private readonly ILogger<DeletePremiseCommandHandler> _logger;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public DeletePremiseCommandHandler(
                ILogger<DeletePremiseCommandHandler> logger,
                IHttpContextAccessor contextAccessor,
                ApplicationDbContext applicationDbContext,
                IMapper mapper)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse> Handle(DeletePremiseCommand request, 
                CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("get own premise error", ex);
                    return ServiceResponseBuilder.Failure(ServerError.GetOwnPremiseError);
                }
            }

            private async Task<ServiceResponse> UnsafeHandleAsync(DeletePremiseCommand request,
                CancellationToken cancellationToken)
            {
                var userValid = _contextAccessor.TryGetUserId(out var userId);
                if(!userValid)
                {
                    return ServiceResponseBuilder.Failure(UserError.UserNotFound);
                }

                var premiseQuery = _applicationDbContext.Premises
                    .Where(p => p.Id == request.PremiseId && p.UserId == userId);

                var deleted = await premiseQuery.ExecuteDeleteAsync(cancellationToken);

                if(deleted == 0)
                {
                    return ServiceResponseBuilder.Failure(PremiseError.MatchPremiseError);
                }                

                return ServiceResponseBuilder.Success();
            }
        }
    }
}
