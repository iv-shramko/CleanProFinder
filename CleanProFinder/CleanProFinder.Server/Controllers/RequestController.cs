using AutoMapper;
using CleanProFinder.Server.Controllers.Base;
using CleanProFinder.Server.Features.Requests;
using CleanProFinder.Shared.Dto.Error;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanProFinder.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : BaseController
    {
        private readonly IMediator _mediator;

        public RequestController(IMediator mediator, IMapper mapper)
            : base(mapper)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create cleaning request.
        /// </summary>
        /// <param name="request">The request to create cleaning request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpPost("create")]
        [Authorize(Roles = Roles.ServiceUser)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> CreateRequest(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Get active requests.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpGet("active-requests")]
        [Authorize(Roles = Roles.ServiceProvider)]
        [ProducesResponseType(typeof(List<RequestShortInfoDto>), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> GetActiveRequests(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetActiveRequestsQuery(), cancellationToken);
            return ConvertFromServiceResponse(result);
        }


        /// <summary>
        /// Get request by id.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpGet("my-requests/{id}")]
        [Authorize(Roles = Roles.ServiceUser)]
        [ProducesResponseType(typeof(RequestFullInfoDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> GetRequest(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRequestByIdQuery { Id = id}, cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Get user's own requests.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpGet("my-requests")]
        [Authorize(Roles = Roles.ServiceUser)]
        [ProducesResponseType(typeof(List<RequestShortInfoDto>), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> GetOwnRequests(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetOwnRequestsQuery(), cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Cancel request.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpGet("my-requests/cancel/{id}")]
        [Authorize(Roles = Roles.ServiceUser)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> CancelRequest(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CancelRequestCommand { RequestId = id}, cancellationToken);
            return ConvertFromServiceResponse(result);
        }
    }
}
