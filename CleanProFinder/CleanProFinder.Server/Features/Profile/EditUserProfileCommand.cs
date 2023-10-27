using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Services.Interfaces;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.Helpers;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;

namespace CleanProFinder.Server.Features.Profile
{
    public class EditUserProfileCommand : IRequest<ServiceResponse<UserProfileDto>>
    {
        public UserProfileDto UserProfileDto { get; set; }
        public class EditUserProfileCommandHandler : IRequestHandler<EditUserProfileCommand,
            ServiceResponse<UserProfileDto>>
        {
            private readonly IMediator _mediator;
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            private ILogger<EditUserProfileCommandHandler> _logger;

            public EditUserProfileCommandHandler(IMediator mediator,
                ApplicationDbContext context,
                IMapper mapper,
                ILogger<EditUserProfileCommandHandler> logger)
            {
                _mediator = mediator;
                _context = context;
                _mapper = mapper;
                _logger = logger;
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
                var newUser = _mapper.Map<ServiceUser>(request.UserProfileDto);

                _context.ServiceUsers.Update(newUser);
                await _context.SaveChangesAsync();

                return ServiceResponseBuilder.Success(request.UserProfileDto);
            }
        }
    }
}
