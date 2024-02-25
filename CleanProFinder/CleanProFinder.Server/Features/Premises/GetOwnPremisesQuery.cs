using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Shared.Dto.Premises;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.Premises
{
    public class GetOwnPremisesQuery : IRequest<ServiceResponse<List<OwnPremiseShortInfoDto>>>
    {
        public class GetOwnPremisesQueryHandler : BaseHandler<GetOwnPremisesQuery, ServiceResponse<List<OwnPremiseShortInfoDto>>>
        {
            private readonly ILogger<GetOwnPremisesQueryHandler> _logger;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetOwnPremisesQueryHandler(ILogger<GetOwnPremisesQueryHandler> logger,
                IHttpContextAccessor contextAccessor,
                ApplicationDbContext applicationDbContext,
                IMapper mapper)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<List<OwnPremiseShortInfoDto>>> Handle(GetOwnPremisesQuery request, 
                CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("get own premises error", ex);
                    return ServiceResponseBuilder.Failure<List<OwnPremiseShortInfoDto>>(ServerError.GetOwnPremisesError);
                }
            }

            private async Task<ServiceResponse<List<OwnPremiseShortInfoDto>>> UnsafeHandleAsync(GetOwnPremisesQuery request,
                CancellationToken cancellationToken)
            {
                var userValid = _contextAccessor.TryGetUserId(out var userId);
                if(!userValid)
                {
                    return ServiceResponseBuilder.Failure<List<OwnPremiseShortInfoDto>>(UserError.UserNotFound);
                }

                var premises = await _applicationDbContext.Premises.Where(p => p.UserId == userId).ToListAsync(cancellationToken);

                var result = _mapper.Map<List<OwnPremiseShortInfoDto>>(premises);

                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
