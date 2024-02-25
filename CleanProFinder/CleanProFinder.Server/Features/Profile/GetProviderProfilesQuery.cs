using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static CleanProFinder.Server.Features.Profile.GetUserProfileViewInfoQuery;

namespace CleanProFinder.Server.Features.Profile
{
    public class GetProviderProfilesQuery : ProviderProfileDto, IRequest<ServiceResponse<List<ProviderPreviewDto>>>
    {
        public class GetProviderProfilesQueryHandler : IRequestHandler<GetProviderProfilesQuery, ServiceResponse<List<ProviderPreviewDto>>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<UserProfileViewInfoQueryHandler> _logger;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetProviderProfilesQueryHandler(
                    ApplicationDbContext context,
                    IMapper mapper,
                    ILogger<UserProfileViewInfoQueryHandler> logger,
                    IHttpContextAccessor httpContextAccessor)
            {
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
                var idRetrieved = _httpContextAccessor.TryGetUserId(out var userId);
                if (idRetrieved is false)
                {
                    return ServiceResponseBuilder.Failure<List<ProviderPreviewDto>>(UserError.InvalidAuthorization);
                }

                var providers = await _context
                    .CleaningServiceProviders
                    .Include(p => p.SavedProviders)
                    .ToListAsync(cancellationToken);

                var providersDto = _mapper.Map<List<ProviderPreviewDto>>(providers);
                foreach (var provider in providersDto)
                {
                    provider.IsSaved = providers.First(p => p.Id == provider.Id).SavedProviders.Any(s => s.ServiceUserId == userId);
                }

                var services = await _context.CleaningServiceServiceProviders
                    .Include(sD => sD.CleaningService)
                    .GroupBy(s => s.CleaningServiceProviderId)
                    .ToListAsync(cancellationToken);


                foreach(var serviceGroup in services)
                {
                    var providerDto = providersDto.FirstOrDefault(p => p.Id == serviceGroup.Key);
                    if(providerDto is null)
                    {
                        continue;
                    }

                    var serviseDtos = _mapper.Map<List<ProviderServiceFullInfoDto>>(services.SelectMany(s => s).ToList());
                    providerDto.Services = serviseDtos;
                }

                return ServiceResponseBuilder.Success(providersDto);
            }
        }
    }
}
