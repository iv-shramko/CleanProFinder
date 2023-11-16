using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CleanProFinder.Server.Features.Requests
{
    public class GetOwnRequestsQuery : IRequest<ServiceResponse<List<RequestShortInfoDto>>>
    {
        public class GetOwnRequestsQueryHandler : BaseHandler<GetOwnRequestsQuery, ServiceResponse<List<RequestShortInfoDto>>>
        {
            private readonly ILogger<GetOwnRequestsQueryHandler> _logger;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _context;

            public GetOwnRequestsQueryHandler(
                ILogger<GetOwnRequestsQueryHandler> logger,
                IMapper mapper,
                IHttpContextAccessor contextAccessor,
                ApplicationDbContext context)
            {
                _context = context;
                _contextAccessor = contextAccessor;
                _logger = logger;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<List<RequestShortInfoDto>>> Handle(GetOwnRequestsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(GetOwnRequestsQuery), ex);
                    return ServiceResponseBuilder.Failure<List<RequestShortInfoDto>>(ServerError.ActiveRequestsError);
                }
            }

            private async Task<ServiceResponse<List<RequestShortInfoDto>>> UnsafeHandleAsync(GetOwnRequestsQuery request,
                CancellationToken cancellationToken)
            {
                var validUserId = _contextAccessor.TryGetUserId(out var userId);
                if (validUserId is false)
                {
                    return ServiceResponseBuilder.Failure<List<RequestShortInfoDto>>(UserError.InvalidAuthorization);
                }
                var requests = await _context
                    .Requests
                    .Include(r => r.Premise)
                    .Include(r => r.Services)
                    .Where(r => r.Premise.User.Id == userId)
                    .ToListAsync(cancellationToken);

                var dto = _mapper.Map<List<RequestShortInfoDto>>(requests);
                return ServiceResponseBuilder.Success(dto);
            }
        }
    }
}
