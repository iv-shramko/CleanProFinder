using AutoMapper;
using CleanProFinder.Server.Controllers.Base;
using CleanProFinder.Server.Features.CleaningServices;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Error;
using CleanProFinder.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanProFinder.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CleaningServiceController : BaseController
    {
        private readonly IMediator _mediator;

        public CleaningServiceController(IMediator mediator, IMapper mapper)
            : base(mapper)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new cleaning service
        /// </summary>
        /// <param name="request">The request to create new cleaning service.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If the operation is successful, it will return an OwnCleaningServiceFullInfoDto.
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpPost("create")]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(typeof(CleaningServiceFullInfoDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> CreateService(CreateCleaningServiceCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Edit existed cleaning service
        /// </summary>
        /// <param name="request">The request to edit existed cleaning service.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If the operation is successful, it will return an OwnCleaningServiceFullInfoDto.
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpPost("edit")]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(typeof(CleaningServiceFullInfoDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> EditService(EditCleaningServiceCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Get cleaning services
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If the operation is successful, it will return a List of OwnCleaningServiceShortInfoDto.
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpGet("services")]
        [Authorize(Roles = Roles.ServiceProvider)]
        [ProducesResponseType(typeof(List<CleaningServiceShortInfoDto>), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> GetServices(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetServicesQuery(), cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Get cleaning service full info
        /// </summary>
        /// <param name="serviceId">Cleaning service id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If the operation is successful, it will return a List of OwnCleaningServiceFullInfoDto.
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = Roles.ServiceProvider)]
        [ProducesResponseType(typeof(CleaningServiceFullInfoDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> GetAvailableService(Guid serviceId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAvailableServiceQuery { ServiceId = serviceId }, cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Delete cleaning service
        /// </summary>
        /// <param name="serviceId">Cleaning service id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpDelete]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> DeleteCleaningService(Guid serviceId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteCleaningServiceCommand { ServiceId = serviceId }, cancellationToken);
            return ConvertFromServiceResponse(result);
        }
    }
}
