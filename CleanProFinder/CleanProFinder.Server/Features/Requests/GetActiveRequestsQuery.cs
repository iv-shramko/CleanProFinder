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
    public class GetActiveRequestsQuery : IRequest<ServiceResponse<List<RequestShortInfoDto>>>
    {
        public class GetActiveRequestsQueryHandler : BaseHandler<GetActiveRequestsQuery, ServiceResponse<List<RequestShortInfoDto>>>
        {
            private readonly ILogger<GetActiveRequestsQueryHandler> _logger;
            private readonly IMapper _mapper;
            private readonly ApplicationDbContext _context;

            public GetActiveRequestsQueryHandler(
                ILogger<GetActiveRequestsQueryHandler> logger,
                IMapper mapper,
                ApplicationDbContext context)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<List<RequestShortInfoDto>>> Handle(GetActiveRequestsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(GetActiveRequestsQuery), ex);
                    return ServiceResponseBuilder.Failure<List<RequestShortInfoDto>>(ServerError.ActiveRequestsError);
                }
            }

            private async Task<ServiceResponse<List<RequestShortInfoDto>>> UnsafeHandleAsync(GetActiveRequestsQuery request, 
                CancellationToken cancellationToken)
            {
                var requests = await _context
                    .Requests
                    .Include(r => r.Premise)
                    .Include(r => r.Services)
                    .ToListAsync(cancellationToken);
                var dto = _mapper.Map<List<RequestShortInfoDto>>(requests);
                return ServiceResponseBuilder.Success(dto);
            }
        }
    }
}
