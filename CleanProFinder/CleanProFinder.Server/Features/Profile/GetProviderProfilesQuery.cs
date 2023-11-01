using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static CleanProFinder.Server.Features.Profile.GetUserProfileViewInfoQuery;

namespace CleanProFinder.Server.Features.Profile
{
    public class GetProviderProfilesQuery : ProviderProfileDto, IRequest<ServiceResponse<List<ProviderPreviewDto>>>
    {
        public class GetProviderProfilesQueryHandler : IRequestHandler<GetProviderProfilesQuery, ServiceResponse<List<ProviderPreviewDto>>>
        {
            private readonly IMediator _mediator;
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<UserProfileViewInfoQueryHandler> _logger;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetProviderProfilesQueryHandler(IMediator mediator,
                    ApplicationDbContext context,
                    IMapper mapper,
                    ILogger<UserProfileViewInfoQueryHandler> logger,
                    IHttpContextAccessor httpContextAccessor)
            {
                _mediator = mediator;
                _context = context;
                _mapper = mapper;
                _logger = logger;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ServiceResponse<List<ProviderPreviewDto>>> Handle(GetProviderProfilesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "view available service providers error");
                    return ServiceResponseBuilder.Failure<List<ProviderPreviewDto>>(ProfileInfoError.ViewProviderProfilesError);
                }
            }

            public async Task<ServiceResponse<List<ProviderPreviewDto>>> UnsafeHandleAsync(GetProviderProfilesQuery request, CancellationToken cancellationToken)
            {
                var result = _context.CleaningServiceProviders.Select(p =>  _mapper.Map<ProviderPreviewDto>(p)).ToList();

                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
