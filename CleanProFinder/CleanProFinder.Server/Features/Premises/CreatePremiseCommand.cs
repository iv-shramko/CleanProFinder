using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Shared.Dto.Premises;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;

namespace CleanProFinder.Server.Features.Premises
{
    public class CreatePremiseCommand : CreatePremiseCommandDto, IRequest<ServiceResponse<OwnPremiseFullInfoDto>>
    {
        public class CreatePremiseCommandHandler : BaseHandler<CreatePremiseCommand, ServiceResponse<OwnPremiseFullInfoDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ILogger<CreatePremiseCommandHandler> _logger;
            private readonly IMapper _mapper;

            public CreatePremiseCommandHandler(
                ApplicationDbContext context,
                IHttpContextAccessor contextAccessor,
                ILogger<CreatePremiseCommandHandler> logger,
                IMapper mapper)
            {
                _context = context;
                _contextAccessor = contextAccessor;
                _logger = logger;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<OwnPremiseFullInfoDto>> Handle(CreatePremiseCommand request, 
                CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Create Premise", ex);
                    return ServiceResponseBuilder.Failure<OwnPremiseFullInfoDto>(ServerError.CreatePremiseError);
                }
            }

            private async Task<ServiceResponse<OwnPremiseFullInfoDto>> UnsafeHandleAsync(CreatePremiseCommand request, 
                CancellationToken cancellationToken)
            {
                var validUserId = _contextAccessor.TryGetUserId(out var userId);
                if (validUserId is false)
                {
                    return ServiceResponseBuilder.Failure<OwnPremiseFullInfoDto>(UserError.InvalidAuthorization);
                }

                var newPremise = _mapper.Map<Premise>(request);
                newPremise.UserId = userId;

                _context.Add(newPremise);
                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<OwnPremiseFullInfoDto>(newPremise);
                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
