using CleanProFinder.Server.Services.Interfaces;
using CleanProFinder.Shared.Dto.Account;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanProFinder.Server.Features.Account
{
    public class SignInCommand : SignInCommandDto, IRequest<ServiceResponse<SignInResultDto>>
    {
        public class SignInCommandHandler : IRequestHandler<SignInCommand, ServiceResponse<SignInResultDto>>
        {
            private readonly ILogger<SignInCommandHandler> _logger;
            private readonly UserManager<IdentityUser> _userManager;
            private readonly ITokenGenerator _tokenGenerator;

            public SignInCommandHandler(ILogger<SignInCommandHandler> logger, 
                UserManager<IdentityUser> userManager,
                ITokenGenerator tokenGenerator)
            {
                _logger = logger;
                _userManager = userManager;
                _tokenGenerator = tokenGenerator;
            }

            public async Task<ServiceResponse<SignInResultDto>> Handle(SignInCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandle(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("user sign in error", ex);
                    return ServiceResponseBuilder.Failure<SignInResultDto>(AccountError.LoginServiceError);
                }
            }

            public async Task<ServiceResponse<SignInResultDto>> UnsafeHandle(SignInCommand request, CancellationToken cancellationToken)
            {
                var identityUser = await _userManager.FindByEmailAsync(request.Email);
                if (identityUser is null)
                {
                    return ServiceResponseBuilder.Failure<SignInResultDto>(AccountError.LoginUserError);
                }

                var isCredentialsValid = await _userManager.CheckPasswordAsync(identityUser, request.Password);
                if (!isCredentialsValid)
                {
                    return ServiceResponseBuilder.Failure<SignInResultDto>(AccountError.LoginUserError);
                }

                var token = await _tokenGenerator.GenerateAsync(identityUser);
                var result = new SignInResultDto()
                {
                    UserId = Guid.Parse(identityUser.Id),
                    Bearer = token,
                };

                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
