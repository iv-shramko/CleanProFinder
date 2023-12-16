using CleanProFinder.Server.Extensions;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Features.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using CleanProFinder.Shared.Enums;

namespace CleanProFinder.Server.Features.Requests
{
    public class ChangeRequestStatusCommand : IRequest<ServiceResponse<RequestFullInfoDto>>
    {
        public Guid RequestId { get; set; }

        public class ChangeRequestStatusCommandHandler : BaseHandler<ChangeRequestStatusCommand, ServiceResponse<RequestFullInfoDto>>
        {
            private readonly ILogger<ChangeRequestStatusCommandHandler> _logger;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;


            public ChangeRequestStatusCommandHandler(ILogger<ChangeRequestStatusCommandHandler> logger, IHttpContextAccessor contextAccessor, ApplicationDbContext context, IMapper mapper)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _context = context;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<RequestFullInfoDto>> Handle(ChangeRequestStatusCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(ChangeRequestStatusCommand), ex);
                    return ServiceResponseBuilder.Failure<RequestFullInfoDto>(ServerError.ChangeRequestStatusError);
                }
            }

            public async Task<ServiceResponse<RequestFullInfoDto>> UnsafeHandleAsync(ChangeRequestStatusCommand request, CancellationToken cancellationToken)
            {
                var validUserId = _contextAccessor.TryGetUserId(out var userId);
                if (validUserId is false)
                {
                    return ServiceResponseBuilder.Failure<RequestFullInfoDto>(UserError.InvalidAuthorization);
                }

                var serviceRequest = await _context.Requests
                    .Include(r => r.Interactions)
                    .FirstOrDefaultAsync(r => r.Id == request.RequestId);

                if (serviceRequest is null)
                {
                    return ServiceResponseBuilder.Failure<RequestFullInfoDto>(RequestError.InvalidId);
                }

                if (serviceRequest
                    .Interactions
                    .Any(i => i.ProviderId == userId && i.InteractionStatus == RequestInteractionStatus.Accepted) is false)
                {
                    return ServiceResponseBuilder.Failure<RequestFullInfoDto>(RequestError.NoAcceptedInteractions);

                }

                var nextStatus = (int)serviceRequest.Status + 1;
                serviceRequest.Status = (RequestStatus)nextStatus;
                await _context.SaveChangesAsync();

                var result = _mapper.Map<RequestFullInfoDto>(serviceRequest);
                return ServiceResponseBuilder.Success(result);
            }


        }
    }
}