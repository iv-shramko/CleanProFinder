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
    public class CreateServiceProviderCommand : CreateServiceProviderCommandDto, IRequest<ServiceResponse<SignUpResultDto>>
    {
        public class CreateServiceProviderCommandHandler : IRequestHandler<CreateServiceProviderCommand,
            ServiceResponse<SignUpResultDto>>
        {
            private readonly IMediator _mediator;
            private readonly ApplicationDbContext _context;
            private readonly ITokenGenerator _tokenGenerator;
            private ILogger<CreateServiceProviderCommand> _logger;

            public CreateServiceProviderCommandHandler(IMediator mediator,
                ApplicationDbContext context,
                ITokenGenerator tokenGenerator,
                ILogger<CreateServiceProviderCommand> logger)
            {
                _mediator = mediator;
                _context = context;
                _tokenGenerator = tokenGenerator;
                _logger = logger;
            }

            public async Task<ServiceResponse<SignUpResultDto>> Handle(CreateServiceProviderCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "service provider creation error");
                    return ServiceResponseBuilder.Failure<SignUpResultDto>(AccountError.ProviderCreateError);
                }
            }

            private async Task<ServiceResponse<SignUpResultDto>> UnsafeHandleAsync(CreateServiceProviderCommand request,
                CancellationToken cancellationToken)
            {
                var createIdentityUserCommand = new CreateIdentityUserCommand()
                {
                    Email = request.Email,
                    Password = request.Password,
                    Role = Roles.ServiceProvider
                };
                var createIdentityResponse = await _mediator.Send(createIdentityUserCommand, cancellationToken);
                if (createIdentityResponse.IsSuccess is false)
                {
                    return createIdentityResponse.MapErrorResult<SignUpResultDto>();
                }

                var serviceProvider = new CleaningServiceProvider()
                {
                    Id = Guid.Parse(createIdentityResponse.Result.IdentityUser.Id),
                    Email = request.Email,
                    IsRestricted = false
                };

                _context.CleaningServiceProviders.Add(serviceProvider);
                await _context.SaveChangesAsync(cancellationToken);

                var token = await _tokenGenerator.GenerateAsync(createIdentityResponse.Result.IdentityUser);

                var result = new SignUpResultDto()
                {
                    UserId = serviceProvider.Id,
                    Bearer = token
                };
                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
