using CleanProFinder.Db.DbContexts;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Services.Interfaces;
using CleanProFinder.Shared.Dto.Account;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.Helpers;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;

namespace CleanProFinder.Server.Features.Account
{
    public class CreateServiceUserCommand : CreateServiceUserCommandDto, IRequest<ServiceResponse<SignUpResultDto>>
    {
        public class CreateServiceUserCommandHandler : IRequestHandler<CreateServiceUserCommand,
            ServiceResponse<SignUpResultDto>>
        {
            private readonly IMediator _mediator;
            private readonly ApplicationDbContext _context;
            private readonly ITokenGenerator _tokenGenerator;
            private ILogger<CreateServiceUserCommandHandler> _logger;

            public CreateServiceUserCommandHandler(IMediator mediator,
                ApplicationDbContext context,
                ITokenGenerator tokenGenerator,
                ILogger<CreateServiceUserCommandHandler> logger)
            {
                _mediator = mediator;
                _context = context;
                _tokenGenerator = tokenGenerator;
                _logger = logger;
            }

            public async Task<ServiceResponse<SignUpResultDto>> Handle(CreateServiceUserCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "user creation error");
                    return ServiceResponseBuilder.Failure<SignUpResultDto>(AccountError.UserCreateError);
                }
            }

            private async Task<ServiceResponse<SignUpResultDto>> UnsafeHandleAsync(CreateServiceUserCommand request, 
                CancellationToken cancellationToken)
            {
                var createIdentityUserCommand = new CreateIdentityUserCommand()
                {
                    Email = request.Email,
                    Password = request.Password,
                    Role = Roles.ServiceUser
                };
                var createIdentityResponse = await _mediator.Send(createIdentityUserCommand, cancellationToken);
                if (createIdentityResponse.IsSuccess is false)
                {
                    return createIdentityResponse.MapErrorResult<SignUpResultDto>();
                }

                var serviceUser = new ServiceUser()
                {
                    Id = Guid.Parse(createIdentityResponse.Result.IdentityUser.Id),
                    Email = request.Email
                };

                _context.ServiceUsers.Add(serviceUser);
                await _context.SaveChangesAsync(cancellationToken);

                var token = await _tokenGenerator.GenerateAsync(createIdentityResponse.Result.IdentityUser);

                var result = new SignUpResultDto()
                {
                    UserId = serviceUser.Id,
                    Bearer = token
                };
                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
