using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Server.Features.CleaningServices;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.CleaningServices
{
    public class GetOwnServicesQuery : IRequest<ServiceResponse<List<OwnCleaningServiceShortInfoDto>>>
    {
        public class GetOwnServicesQueryHandler : BaseHandler<GetOwnServicesQuery, ServiceResponse<List<OwnCleaningServiceShortInfoDto>>>
        {
            private readonly ILogger<GetOwnServicesQueryHandler> _logger;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetOwnServicesQueryHandler(ILogger<GetOwnServicesQueryHandler> logger,
                IHttpContextAccessor contextAccessor,
                ApplicationDbContext applicationDbContext,
                IMapper mapper)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<List<OwnCleaningServiceShortInfoDto>>> Handle(GetOwnServicesQuery request,
                CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("get own services error", ex);
                    return ServiceResponseBuilder.Failure<List<OwnCleaningServiceShortInfoDto>>(ServerError.GetOwnCleaningServicesError);
                }
            }

            private async Task<ServiceResponse<List<OwnCleaningServiceShortInfoDto>>> UnsafeHandleAsync(GetOwnServicesQuery request,
                CancellationToken cancellationToken)
            {
                var userValid = _contextAccessor.TryGetUserId(out var userId);
                if (!userValid)
                {
                    return ServiceResponseBuilder.Failure<List<OwnCleaningServiceShortInfoDto>>(UserError.UserNotFound);
                }

                var premises = await _applicationDbContext.CleaningServices.Where(s => s.ServiceProviderId == userId).ToListAsync(cancellationToken);

                var result = _mapper.Map<List<OwnCleaningServiceShortInfoDto>>(premises);

                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
