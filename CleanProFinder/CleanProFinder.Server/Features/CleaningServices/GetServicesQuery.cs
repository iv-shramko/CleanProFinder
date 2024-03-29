﻿using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Server.Features.CleaningServices;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.CleaningServices
{
    public class GetServicesQuery : IRequest<ServiceResponse<List<CleaningServiceDto>>>
    {
        public class GetServicesQueryHandler : BaseHandler<GetServicesQuery, ServiceResponse<List<CleaningServiceDto>>>
        {
            private readonly ILogger<GetServicesQueryHandler> _logger;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetServicesQueryHandler(ILogger<GetServicesQueryHandler> logger,
                IHttpContextAccessor contextAccessor,
                ApplicationDbContext applicationDbContext,
                IMapper mapper)
            {
                _logger = logger;
                _contextAccessor = contextAccessor;
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<List<CleaningServiceDto>>> Handle(GetServicesQuery request,
                CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("get services error", ex);
                    return ServiceResponseBuilder.Failure<List<CleaningServiceDto>>(ServerError.GetOwnCleaningServicesError);
                }
            }

            private async Task<ServiceResponse<List<CleaningServiceDto>>> UnsafeHandleAsync(GetServicesQuery request,
                CancellationToken cancellationToken)
            {
                var premises = await _applicationDbContext.CleaningServices.ToListAsync(cancellationToken);

                var result = _mapper.Map<List<CleaningServiceDto>>(premises);

                return ServiceResponseBuilder.Success(result);
            }
        }
    }
}
