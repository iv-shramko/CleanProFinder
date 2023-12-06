using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Server.Features.Requests;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Dto.SavedProviders;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.SavedProviders
{
    public class GetOwnSavedProvidersQuery : IRequest<ServiceResponse<List<ProviderPreviewDto>>>
    {
        public class GetOwnSavedProvidersQueryHandler : BaseHandler<GetOwnSavedProvidersQuery, ServiceResponse<List<ProviderPreviewDto>>>
        {
            private readonly ILogger<GetOwnSavedProvidersQueryHandler> _logger;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetOwnSavedProvidersQueryHandler(ILogger<GetOwnSavedProvidersQueryHandler> logger, IHttpContextAccessor contextAccessor, ApplicationDbContext context, IMapper mapper)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _context = context;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<List<ProviderPreviewDto>>> Handle(GetOwnSavedProvidersQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(GetOwnSavedProvidersQuery), ex);
                    return ServiceResponseBuilder.Failure<List<ProviderPreviewDto>>(ServerError.GetOwnSavedProvidersError);
                }
            }

            public async Task<ServiceResponse<List<ProviderPreviewDto>>> UnsafeHandleAsync(GetOwnSavedProvidersQuery request, CancellationToken cancellationToken)
            {
                var validUserId = _contextAccessor.TryGetUserId(out var userId);
                if (validUserId is false)
                {
                    return ServiceResponseBuilder.Failure<List<ProviderPreviewDto>>(UserError.InvalidAuthorization);
                }

                var savedProviders = await _context.SavedProviders
                    .Where(sP => sP.ServiceUserId == userId)
                    .Include(sP => sP.CleaningServiceProvider)
                    .Select(s => s.CleaningServiceProvider)
                    .ToListAsync();

                var services = await _context.CleaningServiceServiceProviders
                    .Include(sD => sD.CleaningService)
                    .GroupBy(s => s.CleaningServiceProviderId)
                    .ToListAsync(cancellationToken);

                var result = _mapper.Map<List<ProviderPreviewDto>>(savedProviders);

                foreach (var serviceGroup in services)
                {
                    var providerDto = result.FirstOrDefault(p => p.Id == serviceGroup.Key);
                    if (providerDto is null)
                    {
                        continue;
                    }

                    var serviseDtos = _mapper.Map<List<ProviderServiceFullInfoDto>>(services.SelectMany(s => s).ToList());
                    providerDto.Services = serviseDtos;
                }
                
                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
