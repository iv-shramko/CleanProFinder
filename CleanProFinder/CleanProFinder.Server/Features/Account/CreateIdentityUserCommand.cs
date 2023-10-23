using CleanProFinder.Server.Models.Account;
using CleanProFinder.Shared.Dto.Account;
using CleanProFinder.Shared.Errors.Base;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanProFinder.Server.Features.Account
{
    public class CreateIdentityUserCommand : CredentialsDto, IRequest<ServiceResponse<CreateIdentityUserResult>>
    {
        public string Role { get; set; }

        public class CreateIdentityUserCommandHandler 
            : IRequestHandler<CreateIdentityUserCommand, ServiceResponse<CreateIdentityUserResult>>
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private ILogger<CreateIdentityUserCommandHandler> _logger;

            public CreateIdentityUserCommandHandler(UserManager<IdentityUser> userManager,
                RoleManager<IdentityRole> roleManager,
                ILogger<CreateIdentityUserCommandHandler> logger)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _logger = logger;
            }

            public async Task<ServiceResponse<CreateIdentityUserResult>> Handle(CreateIdentityUserCommand request, 
                CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "user creation error");
                    return ServiceResponseBuilder.Failure<CreateIdentityUserResult>(AccountError.IdentityCreateError);
                }
            }

            private async Task<ServiceResponse<CreateIdentityUserResult>> UnsafeHandleAsync(
                CreateIdentityUserCommand request,
                CancellationToken cancellationToken)
            {
                var identityUser = new IdentityUser()
                {
                    Email = request.Email,
                    UserName = request.Email
                };

                var createResult = await _userManager.CreateAsync(identityUser, request.Password);
                if (createResult.Succeeded is false)
                {
                    var errors = createResult.Errors.Select(e => new ServiceError()
                    { Header = "UserError", ErrorMessage = e.Description }).ToList();
                    return ServiceResponseBuilder.Failure<CreateIdentityUserResult>(errors);
                }

                await AssureRoleCreatedAsync(request.Role);
                await _userManager.AddToRoleAsync(identityUser, request.Role);

                return ServiceResponseBuilder.Success(new CreateIdentityUserResult() { IdentityUser = identityUser });
            }

            private async Task AssureRoleCreatedAsync(string role)
            {
                if(await _roleManager.RoleExistsAsync(role))
                {
                    return;
                }
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
