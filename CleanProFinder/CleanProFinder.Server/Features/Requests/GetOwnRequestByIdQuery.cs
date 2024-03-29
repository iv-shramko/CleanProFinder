﻿using AutoMapper;
using CleanProFinder.Db.DbContexts;
using CleanProFinder.Server.Extensions;
using CleanProFinder.Server.Features.Base;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.Errors.ServiceErrors;
using CleanProFinder.Shared.ServiceResponseHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Server.Features.Requests
{
    public class GetOwnRequestByIdQuery : IRequest<ServiceResponse<RequestFullInfoDto>>
    {
        public Guid Id { get; set; }

        public class GetActiveRequestQueryHandler : BaseHandler<GetOwnRequestByIdQuery, ServiceResponse<RequestFullInfoDto>>
        {
            private readonly ILogger<GetActiveRequestQueryHandler> _logger;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly ApplicationDbContext _context;

            public GetActiveRequestQueryHandler(
                ILogger<GetActiveRequestQueryHandler> logger,
                IMapper mapper,
                IHttpContextAccessor contextAccessor,
                ApplicationDbContext context)
            {
                _context = context;
                _contextAccessor = contextAccessor;
                _logger = logger;
                _mapper = mapper;
            }

            public override async Task<ServiceResponse<RequestFullInfoDto>> Handle(GetOwnRequestByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await UnsafeHandleAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(nameof(GetOwnRequestByIdQuery), ex);
                    return ServiceResponseBuilder.Failure<RequestFullInfoDto>(ServerError.RequestByIdError);
                }
            }

            private async Task<ServiceResponse<RequestFullInfoDto>> UnsafeHandleAsync(GetOwnRequestByIdQuery request,
                CancellationToken cancellationToken)
            {
                var validUserId = _contextAccessor.TryGetUserId(out var userId);
                if (validUserId is false)
                {
                    return ServiceResponseBuilder.Failure<RequestFullInfoDto>(UserError.InvalidAuthorization);
                }

                var serviceRequest = await _context
                    .Requests
                    .Include(r => r.Premise)
                    .Include(r => r.Services)
                    .Include(r => r.Interactions)
                    .ThenInclude(i => i.Provider)
                    .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
;
                if (serviceRequest is null)
                {
                    return ServiceResponseBuilder.Failure<RequestFullInfoDto>(RequestError.InvalidId);
                }

                if (serviceRequest.Premise.UserId != userId)
                {
                    return ServiceResponseBuilder.Failure<RequestFullInfoDto>(RequestError.NotRequestOwner);
                }

                var dto = _mapper.Map<RequestFullInfoDto>(serviceRequest);
                return ServiceResponseBuilder.Success(dto);
            }
        }
    }
}
