using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Db.Models;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.CleaningServices
{
    public class EditCleaningServiceCommand : EditCleaningServiceCommandDto, IRequest<ServiceResponse<OwnCleaningServiceFullInfoDto>>
    {
        public class EditCleaningServiceCommandHandler : BaseHandler<EditCleaningServiceCommand, ServiceResponse<OwnCleaningServiceFullInfoDto>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ILogger<EditCleaningServiceCommandHandler> _logger;
            private readonly IMapper _mapper;

            public EditCleaningServiceCommandHandler(
                ApplicationDbContext context,
                IHttpContextAccessor contextAccessor,
                ILogger<EditCleaningServiceCommandHandler> logger,
                IMapper mapper)
            {
                _context = context;
                _contextAccessor = contextAccessor;
                _logger = logger;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<OwnCleaningServiceFullInfoDto>> Handle(EditCleaningServiceCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Edit Cleaning Service", ex);
                    return ServiceResponseBuilder.Failure<OwnCleaningServiceFullInfoDto>(ServerError.EditCleaningServiceError);
                }
            }

            private async Task<ServiceResponse<OwnCleaningServiceFullInfoDto>> UnsafeHandleAsync(EditCleaningServiceCommand request, CancellationToken cancellationToken)
            {
                var validUserId = _contextAccessor.TryGetUserId(out var userId);
                if (validUserId is false)
                {
                    return ServiceResponseBuilder.Failure<OwnCleaningServiceFullInfoDto>(UserError.InvalidAuthorization);
                }

                var existedService = await _context
                    .Set<CleaningService>()
                    .FirstOrDefaultAsync(s => s.ServiceProviderId == userId && s.Id == request.Id, cancellationToken);
                if (existedService is null)
                {
                    return ServiceResponseBuilder.Failure<OwnCleaningServiceFullInfoDto>(CleaningServiceError.MatchCleaningServiceError);
                }

                _mapper.Map(request, existedService);
                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<OwnCleaningServiceFullInfoDto>(existedService);

                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
