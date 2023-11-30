using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Server.Features.Requests;
using CleanProFinder.Shared.Dto.SavedProviders;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CleanProFinder.Server.Features.SavedProviders
{
    public class GetOwnSavedProvidersQuery : IRequest<ServiceResponse<List<SavedProviderDto>>>
    {
        public class GetOwnSavedProvidersQueryHandler : BaseHandler<GetOwnSavedProvidersQuery, ServiceResponse<List<SavedProviderDto>>>
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

            public override async Task<ServiceResponse<List<SavedProviderDto>>> Handle(GetOwnSavedProvidersQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(GetOwnSavedProvidersQuery), ex);
                    return ServiceResponseBuilder.Failure<List<SavedProviderDto>>(ServerError.GetOwnSavedProvidersError);
                }
            }

            public async Task<ServiceResponse<List<SavedProviderDto>>> UnsafeHandleAsync(GetOwnSavedProvidersQuery request, CancellationToken cancellationToken)
            {
                var validUserId = _contextAccessor.TryGetUserId(out var userId);
                if (validUserId is false)
                {
                    return ServiceResponseBuilder.Failure<List<SavedProviderDto>>(UserError.InvalidAuthorization);
                }

                var savedProviders = await _context.SavedProviders
                    .Where(sP => sP.ServiceUserId == userId)
                    .Include(sP => sP.CleaningServiceProvider)
                    .ToListAsync();

                var result = _mapper.Map<List<SavedProviderDto>>(savedProviders);
                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
