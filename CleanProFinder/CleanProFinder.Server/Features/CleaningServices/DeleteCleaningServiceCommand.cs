using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.CleaningServices
{
    public class DeleteCleaningServiceCommand : IRequest<ServiceResponse>
    {
        public Guid ServiceId { get; set; }

        public class DeleteCleaningServiceCommandHandler : BaseHandler<DeleteCleaningServiceCommand, ServiceResponse>
        {
            private readonly ILogger<DeleteCleaningServiceCommandHandler> _logger;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public DeleteCleaningServiceCommandHandler(
                ILogger<DeleteCleaningServiceCommandHandler> logger,
                IHttpContextAccessor contextAccessor,
                ApplicationDbContext applicationDbContext,
                IMapper mapper)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse> Handle(DeleteCleaningServiceCommand request,
                CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("delete cleaning service error", ex);
                    return ServiceResponseBuilder.Failure(ServerError.DeleteCleaningServiceError);
                }
            }

            private async Task<ServiceResponse> UnsafeHandleAsync(DeleteCleaningServiceCommand request,
                CancellationToken cancellationToken)
            {

                var serviceQuery = _applicationDbContext.CleaningServices
                    .Where(s => s.Id == request.ServiceId);

                var deleted = await serviceQuery.ExecuteDeleteAsync(cancellationToken);

                if (deleted == 0)
                {
                    return ServiceResponseBuilder.Failure(CleaningServiceError.MatchCleaningServiceError);
                }

                return ServiceResponseBuilder.Success();
            }
        }
    }
}
