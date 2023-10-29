using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.Profile
{
    public class EditProviderProfileCommand : ProviderProfileDto, IRequest<ServiceResponse<ProviderProfileDto>>
    {
        public class EditProviderProfileCommandHandler : IRequestHandler<EditProviderProfileCommand,
                    ServiceResponse<ProviderProfileDto>>
        {
            private readonly IMediator _mediator;
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<EditProviderProfileCommandHandler> _logger;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public EditProviderProfileCommandHandler(IMediator mediator,
                ApplicationDbContext context,
                IMapper mapper,
                ILogger<EditProviderProfileCommandHandler> logger,
                IHttpContextAccessor httpContextAccessor)
            {
                _mediator = mediator;
                _context = context;
                _mapper = mapper;
                _logger = logger;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ServiceResponse<ProviderProfileDto>> Handle(EditProviderProfileCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "profile update error");
                    return ServiceResponseBuilder.Failure<ProviderProfileDto>(EditProfileError.ProfileUpdateError);
                }
            }

            private async Task<ServiceResponse<ProviderProfileDto>> UnsafeHandleAsync(EditProviderProfileCommand request,
                CancellationToken cancellationToken)
            {
                var providerIdRetrieved = _httpContextAccessor.TryGetUserId(out var providerId);

                if (providerIdRetrieved is false)
                {
                    return ServiceResponseBuilder.Failure<ProviderProfileDto>(UserError.InvalidAuthorization);
                }

                var oldProvider = await _context.CleaningServiceProviders.FirstOrDefaultAsync(x => x.Id == providerId, cancellationToken);

                if (oldProvider is null)
                {
                    return ServiceResponseBuilder.Failure<ProviderProfileDto>(UserError.UserNotFound);
                }

                _mapper.Map<ProviderProfileDto, CleaningServiceProvider>(request, oldProvider);
                _context.CleaningServiceProviders.Update(oldProvider);
                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<ProviderProfileDto>(oldProvider);

                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
