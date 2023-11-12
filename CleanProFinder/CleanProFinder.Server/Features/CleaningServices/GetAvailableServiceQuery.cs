using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Server.Features.Premises;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.CleaningServices
{
    public class GetAvailableServiceQuery : IRequest<ServiceResponse<CleaningServiceDto>>
    {
        public Guid ServiceId { get; set; }

        public class GetAvailableServiceQueryHandler : BaseHandler<GetAvailableServiceQuery, ServiceResponse<CleaningServiceDto>>
        {
            private readonly ILogger<GetAvailableServiceQueryHandler> _logger;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetAvailableServiceQueryHandler(
                ILogger<GetAvailableServiceQueryHandler> logger,
                IHttpContextAccessor contextAccessor,
                ApplicationDbContext applicationDbContext,
                IMapper mapper)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<CleaningServiceDto>> Handle(GetAvailableServiceQuery request,
                CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("get cleaning service error", ex);
                    return ServiceResponseBuilder.Failure<CleaningServiceDto>(ServerError.GetOwnCleaningServiceError);
                }
            }

            private async Task<ServiceResponse<CleaningServiceDto>> UnsafeHandleAsync(GetAvailableServiceQuery request,
                CancellationToken cancellationToken)
            {
                var cleaningService = await _applicationDbContext.CleaningServices
                    .FirstOrDefaultAsync(s => s.Id == request.ServiceId, cancellationToken);
                
                if (cleaningService is null)
                {
                    return ServiceResponseBuilder.Failure<CleaningServiceDto>(CleaningServiceError.MatchCleaningServiceError);
                }

                var result = _mapper.Map<CleaningServiceDto>(cleaningService);

                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
