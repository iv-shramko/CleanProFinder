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

namespace CleanProFinder.Server.Features.SavedProviders
{
    public class DeleteSavedProviderCommand : IRequest<ServiceResponse>
    {
        public Guid CleaningServiceProviderId { get; set; }
        public class DeleteSavedProviderCommandHandler : BaseHandler<DeleteSavedProviderCommand, ServiceResponse>
        {
            private readonly ILogger<DeleteSavedProviderCommandHandler> _logger;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _context;

            public DeleteSavedProviderCommandHandler(ILogger<DeleteSavedProviderCommandHandler> logger, IHttpContextAccessor contextAccessor, ApplicationDbContext context)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _context = context;
            }

            public override async Task<ServiceResponse> Handle(DeleteSavedProviderCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(DeleteSavedProviderCommand), ex);
                    return ServiceResponseBuilder.Failure(ServerError.DeleteSavedProviderError);
                }
            }

            public async Task<ServiceResponse> UnsafeHandleAsync(DeleteSavedProviderCommand request, CancellationToken cancellationToken)
            {
                var validUserId = _contextAccessor.TryGetUserId(out var userId);
                if (validUserId is false)
                {
                    return ServiceResponseBuilder.Failure(UserError.InvalidAuthorization);
                }

                var provider = await _context.CleaningServiceProviders
                    .FirstOrDefaultAsync(cSP => cSP.Id == request.CleaningServiceProviderId);

                if (provider is null)
                {
                    return ServiceResponseBuilder.Failure(UserError.UserNotFound);
                }

                var savedProvider = await _context.SavedProviders
                    .FirstOrDefaultAsync(sP => sP.ServiceUserId == userId && sP.CleaningServiceProviderId == request.CleaningServiceProviderId);

                if (savedProvider is null)
                {
                    return ServiceResponseBuilder.Failure(SavedProviderError.InvalidSavedProvider);
                }

                _context.SavedProviders.Remove(savedProvider);
                await _context.SaveChangesAsync(cancellationToken);

                return ServiceResponseBuilder.Success();
            }
        }
    }
}
