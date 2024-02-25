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
    public class EditCleaningServiceCommand : EditCleaningServiceCommandDto, IRequest<ServiceResponse<CleaningServiceDto>>
    {
        public class EditCleaningServiceCommandHandler : BaseHandler<EditCleaningServiceCommand, ServiceResponse<CleaningServiceDto>>
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

            public override async Task<ServiceResponse<CleaningServiceDto>> Handle(EditCleaningServiceCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Edit Cleaning Service", ex);
                    return ServiceResponseBuilder.Failure<CleaningServiceDto>(ServerError.EditCleaningServiceError);
                }
            }

            private async Task<ServiceResponse<CleaningServiceDto>> UnsafeHandleAsync(EditCleaningServiceCommand request, CancellationToken cancellationToken)
            {
                var existedService = await _context
                    .Set<CleaningService>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

                if (existedService is null)
                {
                    return ServiceResponseBuilder.Failure<CleaningServiceDto>(CleaningServiceError.MatchCleaningServiceError);
                }

                _mapper.Map(request, existedService);
                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<CleaningServiceDto>(existedService);

                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
