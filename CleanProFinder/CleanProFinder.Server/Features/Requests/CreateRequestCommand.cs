using CleanProFinder.Db.DbContexts;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Shared.Dto.Request;
using CleanProFinder.Shared.Enums;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.Requests
{
    public class CreateRequestCommand : CreateRequestCommandDto, IRequest<ServiceResponse>
    {
        public class CreateRequestCommandHandler : BaseHandler<CreateRequestCommand, ServiceResponse>
        {
            private readonly ILogger<CreateRequestCommandHandler> _logger;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _context;

            public CreateRequestCommandHandler(
                ILogger<CreateRequestCommandHandler> logger,
                IHttpContextAccessor contextAccessor,
                ApplicationDbContext context)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _context = context;
            }

            public override async Task<ServiceResponse> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(CreateRequestCommand), ex);
                    return ServiceResponseBuilder.Failure(ServerError.CreateRequestError);
                }
            }

            private async Task<ServiceResponse> UnsafeHandleAsync(CreateRequestCommand command, CancellationToken cancellationToken)
            {
                var validUserId = _contextAccessor.TryGetUserId(out var userId);
                if (validUserId is false)
                {
                    return ServiceResponseBuilder.Failure(UserError.InvalidAuthorization);
                }

                var serviceUser = await _context.ServiceUsers.FirstOrDefaultAsync(u => u.Id == userId);

                if(serviceUser.IsRestricted is true)
                {
                    return ServiceResponseBuilder.Failure(UserError.UserIsRestricted);
                }

                var premise = await _context.Premises.FirstOrDefaultAsync(p => p.Id == command.PremiseId && p.UserId == userId);
                if(premise is null)
                {
                    return ServiceResponseBuilder.Failure(RequestError.InvalidPremise);
                }

                var request = new Request();
                request.Description = command.Description ?? string.Empty;
                request.PremiseId = premise.Id;

                if(command.ServicesId is not null && command.ServicesId.Any())
                {
                    var services = await _context.CleaningServices.Where(s => command.ServicesId.Contains(s.Id)).ToListAsync();
                    request.Services = services;
                }
                
                if(command.SelectedProvidersIds is not null && command.SelectedProvidersIds.Any())
                {
                    request.Status = RequestStatus.Sent;                    
                    foreach (var id in command.SelectedProvidersIds)
                    {
                        AssignProvider(request, id);
                    }                    
                }
                else
                {
                    request.Status = RequestStatus.Placed;
                }
               
                _context.Requests.Add(request);
                await _context.SaveChangesAsync(cancellationToken);

                //TODO: notify provider

                return ServiceResponseBuilder.Success();
            }

            private static void AssignProvider(Request request, Guid providerId)
            {
                request.Interactions ??= new List<RequestInteraction>();

                var interaction = new RequestInteraction();
                interaction.ProviderId = providerId;
                interaction.RequestId = request.Id;

                request.Interactions.Add(interaction);;                         
            }
        }
    }
}
