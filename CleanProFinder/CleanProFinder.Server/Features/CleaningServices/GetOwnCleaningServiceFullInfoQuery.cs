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
    public class GetOwnCleaningServiceFullInfoQuery : IRequest<ServiceResponse<OwnCleaningServiceFullInfoDto>>
    {
        public Guid ServiceId { get; set; }

        public class GetOwnCleaningServiceFullInfoQueryHandler : BaseHandler<GetOwnCleaningServiceFullInfoQuery, ServiceResponse<OwnCleaningServiceFullInfoDto>>
        {
            private readonly ILogger<GetOwnCleaningServiceFullInfoQueryHandler> _logger;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetOwnCleaningServiceFullInfoQueryHandler(
                ILogger<GetOwnCleaningServiceFullInfoQueryHandler> logger,
                IHttpContextAccessor contextAccessor,
                ApplicationDbContext applicationDbContext,
                IMapper mapper)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<OwnCleaningServiceFullInfoDto>> Handle(GetOwnCleaningServiceFullInfoQuery request,
                CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("get own cleaning service error", ex);
                    return ServiceResponseBuilder.Failure<OwnCleaningServiceFullInfoDto>(ServerError.GetOwnCleaningServiceError);
                }
            }

            private async Task<ServiceResponse<OwnCleaningServiceFullInfoDto>> UnsafeHandleAsync(GetOwnCleaningServiceFullInfoQuery request,
                CancellationToken cancellationToken)
            {
                var userValid = _contextAccessor.TryGetUserId(out var userId);
                if (!userValid)
                {
                    return ServiceResponseBuilder.Failure<OwnCleaningServiceFullInfoDto>(UserError.UserNotFound);
                }

                var cleaningService = await _applicationDbContext.CleaningServices
                    .FirstOrDefaultAsync(s => s.Id == request.ServiceId && s.ServiceProviderId == userId, cancellationToken);
                if (cleaningService is null)
                {
                    return ServiceResponseBuilder.Failure<OwnCleaningServiceFullInfoDto>(CleaningServiceError.MatchCleaningServiceError);
                }

                var result = _mapper.Map<OwnCleaningServiceFullInfoDto>(cleaningService);

                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
