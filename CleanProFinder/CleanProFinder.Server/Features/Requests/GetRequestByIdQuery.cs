using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.Requests
{
    public class GetRequestByIdQuery : IRequest<ServiceResponse<RequestFullInfoProviderViewDto>>
    {
        public Guid Id { get; set; }

        public class GetRequestByIdQueryHandler : BaseHandler<GetRequestByIdQuery, ServiceResponse<RequestFullInfoProviderViewDto>>
        {
            private readonly ILogger<GetRequestByIdQueryHandler> _logger;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _context;

            public GetRequestByIdQueryHandler(
                ILogger<GetRequestByIdQueryHandler> logger,
                IMapper mapper,
                IHttpContextAccessor contextAccessor,
                ApplicationDbContext context)
            {
                _context = context;
                _contextAccessor = contextAccessor;
                _logger = logger;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<RequestFullInfoProviderViewDto>> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(GetOwnRequestByIdQuery), ex);
                    return ServiceResponseBuilder.Failure<RequestFullInfoProviderViewDto>(ServerError.RequestByIdError);
                }
            }

            private async Task<ServiceResponse<RequestFullInfoProviderViewDto>> UnsafeHandleAsync(GetRequestByIdQuery request,
                CancellationToken cancellationToken)
            {
                var serviceRequest = await _context
                    .Requests
                    .Include(r => r.Premise)
                    .Include(r => r.Services)
                    .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
;
                if (serviceRequest is null)
                {
                    return ServiceResponseBuilder.Failure<RequestFullInfoProviderViewDto>(RequestError.InvalidId);
                }

                var dto = _mapper.Map<RequestFullInfoProviderViewDto>(serviceRequest);

                return ServiceResponseBuilder.Success(dto);
            }
        }
    }
}
