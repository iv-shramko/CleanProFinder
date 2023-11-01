using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Shared.Dto.Premises;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.Premises
{
    public class EditPremiseCommand : EditPremiseCommandDto, IRequest<ServiceResponse<OwnPremiseFullInfoDto>>
    {
        public class EditPremiseCommandHandler : BaseHandler<EditPremiseCommand, ServiceResponse<OwnPremiseFullInfoDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ILogger<EditPremiseCommandHandler> _logger;
            private readonly IMapper _mapper;

            public EditPremiseCommandHandler(
                ApplicationDbContext context, 
                IHttpContextAccessor contextAccessor,
                ILogger<EditPremiseCommandHandler> logger,
                IMapper mapper)
            {
                _context = context;
                _contextAccessor = contextAccessor;
                _logger = logger;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<OwnPremiseFullInfoDto>> Handle(EditPremiseCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Edit Premise", ex);
                    return ServiceResponseBuilder.Failure<OwnPremiseFullInfoDto>(ServerError.EditPremiseError);
                }
            }

            private async Task<ServiceResponse<OwnPremiseFullInfoDto>> UnsafeHandleAsync(EditPremiseCommand request, CancellationToken cancellationToken)
            {
                var validUserId = _contextAccessor.TryGetUserId(out var userId);
                if (validUserId is false)
                {
                    return ServiceResponseBuilder.Failure<OwnPremiseFullInfoDto>(UserError.InvalidAuthorization);
                }

                var existedPremise = await _context
                    .Set<Premise>()
                    .FirstOrDefaultAsync(p => p.UserId == userId && p.Id == request.Id, cancellationToken);
                if(existedPremise is null)
                {
                    return ServiceResponseBuilder.Failure<OwnPremiseFullInfoDto>(PremiseError.MatchPremiseError);
                }

                _mapper.Map(request, existedPremise);
                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<OwnPremiseFullInfoDto>(existedPremise);

                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
