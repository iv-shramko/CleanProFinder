using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Server.Features.Requests;
using CleanProFinder.Shared.Dto.SavedProviders;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace CleanProFinder.Server.Features.SavedProviders
{
    public class SaveProviderCommand : IRequest<ServiceResponse<SavedProviderDto>>
    {
        public Guid CleaningServiceProviderId { get; set; }
        public class SaveProviderCommandHandler : BaseHandler<SaveProviderCommand, ServiceResponse<SavedProviderDto>>
        {
            private readonly ILogger<SaveProviderCommandHandler> _logger;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public SaveProviderCommandHandler(ILogger<SaveProviderCommandHandler> logger, IHttpContextAccessor contextAccessor, ApplicationDbContext context, IMapper mapper)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _context = context;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<SavedProviderDto>> Handle(SaveProviderCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(SaveProviderCommand), ex);
                    return ServiceResponseBuilder.Failure<SavedProviderDto>(ServerError.SaveProviderError);
                }
            }

            public async Task<ServiceResponse<SavedProviderDto>> UnsafeHandleAsync(SaveProviderCommand request, CancellationToken cancellationToken)
            {
                var validUserId = _contextAccessor.TryGetUserId(out var userId);
                if (validUserId is false)
                {
                    return ServiceResponseBuilder.Failure<SavedProviderDto>(UserError.InvalidAuthorization);
                }

                var provider = await _context.CleaningServiceProviders
                    .FirstOrDefaultAsync(cSP => cSP.Id == request.CleaningServiceProviderId);

                if (provider is null)
                {
                    return ServiceResponseBuilder.Failure<SavedProviderDto>(UserError.UserNotFound);
                }

                if (_context.SavedProviders.Any(sP => sP.ServiceUserId == userId && sP.CleaningServiceProviderId == request.CleaningServiceProviderId))
                {
                    return ServiceResponseBuilder.Failure<SavedProviderDto>(SavedProviderError.AlreadySavedProvider);
                }

                var savedProvider = new SavedProvider
                {
                    ServiceUserId = userId,
                    CleaningServiceProviderId = request.CleaningServiceProviderId,
                    SubscribedAt = DateTime.UtcNow
                };

                await _context.SavedProviders.AddAsync(savedProvider);
                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<SavedProviderDto>(savedProvider);

                return ServiceResponseBuilder.Success(result);

            }
        }
    }
}
