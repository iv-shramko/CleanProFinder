using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Services.Interfaces;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.Helpers;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.Profile
{
    public class EditUserProfileCommand : UserProfileDto, IRequest<ServiceResponse<UserProfileDto>>
    {
        public class EditUserProfileCommandHandler : IRequestHandler<EditUserProfileCommand,
            ServiceResponse<UserProfileDto>>
        {
            private readonly IMediator _mediator;
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<EditUserProfileCommandHandler> _logger;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public EditUserProfileCommandHandler(IMediator mediator,
                ApplicationDbContext context,
                IMapper mapper,
                ILogger<EditUserProfileCommandHandler> logger,
                IHttpContextAccessor httpContextAccessor)
            {
                _mediator = mediator;
                _context = context;
                _mapper = mapper;
                _logger = logger;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ServiceResponse<UserProfileDto>> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "profile update error");
                    return ServiceResponseBuilder.Failure<UserProfileDto>(EditProfileError.ProfileUpdateError);
                }
            }

            private async Task<ServiceResponse<UserProfileDto>> UnsafeHandleAsync(EditUserProfileCommand request,
                CancellationToken cancellationToken)
            {
                var userIdRetrieved = _httpContextAccessor.TryGetUserId(out var userId);

                if (userIdRetrieved is false)
                {
                    return ServiceResponseBuilder.Failure<UserProfileDto>(UserError.InvalidAuthorization);
                }

                var oldUser = await _context.ServiceUsers.FirstOrDefaultAsync(x=> x.Id == userId, cancellationToken);

                if (oldUser is null)
                {
                    return ServiceResponseBuilder.Failure<UserProfileDto>(UserError.UserNotFound);
                }

                _mapper.Map<UserProfileDto, ServiceUser>(request, oldUser);
                _context.ServiceUsers.Update(oldUser);
                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<UserProfileDto>(oldUser);

                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
