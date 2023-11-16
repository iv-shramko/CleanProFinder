﻿using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.Requests
{
    public class GetRequestByIdQuery : RequestDto, IRequest<ServiceResponse<RequestDto>>
    {
        public Guid Id { get; set; }

        public class GetActiveRequestQueryHandler : BaseHandler<GetRequestByIdQuery, ServiceResponse<RequestDto>>
        {
            private readonly ILogger<GetActiveRequestQueryHandler> _logger;
            private readonly IMapper _mapper;
            private readonly ApplicationDbContext _context;

            public GetActiveRequestQueryHandler(
                ILogger<GetActiveRequestQueryHandler> logger,
                IMapper mapper,
                ApplicationDbContext context)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<RequestDto>> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(GetRequestByIdQuery), ex);
                    return ServiceResponseBuilder.Failure<RequestDto>(ServerError.RequestByIdError);
                }
            }

            private async Task<ServiceResponse<RequestDto>> UnsafeHandleAsync(GetRequestByIdQuery request,
                CancellationToken cancellationToken)
            {
                var serviceRequest = await _context
                    .Requests
                    .Include(r => r.Premise)
                    .Include(r => r.Services)
                    .FirstOrDefaultAsync(r => r.Id == request.Id)
;
                if (serviceRequest is null)
                {
                    return ServiceResponseBuilder.Failure<RequestDto>(RequestError.InvalidId);
                }
                var dto = _mapper.Map<RequestDto>(serviceRequest);
                return ServiceResponseBuilder.Success(dto);
            }
        }
    }
}