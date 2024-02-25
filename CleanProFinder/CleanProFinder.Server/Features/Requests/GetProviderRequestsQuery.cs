using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.Requests
{
    public class GetProviderRequestsQuery : IRequest<ServiceResponse<List<RequestShortInfoDto>>>
    {
        public class GetProviderRequestsQueryHandler : BaseHandler<GetProviderRequestsQuery, ServiceResponse<List<RequestShortInfoDto>>>
        {
            private readonly ILogger<GetProviderRequestsQueryHandler> _logger;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _context;

            public GetProviderRequestsQueryHandler(
                ILogger<GetProviderRequestsQueryHandler> logger,
                IMapper mapper,
                IHttpContextAccessor contextAccessor,
                ApplicationDbContext context)
            {
                _context = context;
                _contextAccessor = contextAccessor;
                _logger = logger;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<List<RequestShortInfoDto>>> Handle(GetProviderRequestsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(GetProviderRequestsQuery), ex);
                    return ServiceResponseBuilder.Failure<List<RequestShortInfoDto>>(ServerError.OwnRequestsError);
                }
            }

            private async Task<ServiceResponse<List<RequestShortInfoDto>>> UnsafeHandleAsync(GetProviderRequestsQuery request,
                CancellationToken cancellationToken)
            {
                var validUserId = _contextAccessor.TryGetUserId(out var userId);
                if (validUserId is false)
                {
                    return ServiceResponseBuilder.Failure<List<RequestShortInfoDto>>(UserError.InvalidAuthorization);
                }

                var requests = await _context
                    .Requests
                    .Include(r => r.Interactions)
                    .Include(r => r.Premise)
                    .Include(r => r.Services)
                    .Where(r => r.Interactions.Any(i => i.ProviderId == userId))
                    .ToListAsync();



                var dto = _mapper.Map<List<RequestShortInfoDto>>(requests);

                return ServiceResponseBuilder.Success(dto);
            }
        }
    }
}
