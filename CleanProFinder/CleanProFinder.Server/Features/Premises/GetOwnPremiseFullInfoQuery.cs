﻿using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Shared.Dto.Premises;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.Premises
{
    public class GetOwnPremiseFullInfoQuery : IRequest<ServiceResponse<OwnPremiseFullInfoDto>>
    {
        public Guid PremiseId { get; set; }

        public class GetOwnPremiseFullQueryHandler : BaseHandler<GetOwnPremiseFullInfoQuery, ServiceResponse<OwnPremiseFullInfoDto>>
        {
            private readonly ILogger<GetOwnPremiseFullQueryHandler> _logger;
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetOwnPremiseFullQueryHandler(
                ILogger<GetOwnPremiseFullQueryHandler> logger,
                ApplicationDbContext applicationDbContext,
                IMapper mapper)
            {
                _logger = logger;
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<OwnPremiseFullInfoDto>> Handle(GetOwnPremiseFullInfoQuery request, 
                CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("get own premise error", ex);
                    return ServiceResponseBuilder.Failure<OwnPremiseFullInfoDto>(ServerError.GetOwnPremiseError);
                }
            }

            private async Task<ServiceResponse<OwnPremiseFullInfoDto>> UnsafeHandleAsync(GetOwnPremiseFullInfoQuery request,
                CancellationToken cancellationToken)
            {
                var premise = await _applicationDbContext.Premises
                    .FirstOrDefaultAsync(p => p.Id == request.PremiseId, cancellationToken);

                if(premise is null)
                {
                    return ServiceResponseBuilder.Failure<OwnPremiseFullInfoDto>(PremiseError.MatchPremiseError);
                }

                var result = _mapper.Map<OwnPremiseFullInfoDto>(premise);

                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
