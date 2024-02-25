using AutoMapper;
using CleanProFinder.Server.Controllers.Base;
using CleanProFinder.Server.Features.Premises;
using CleanProFinder.Shared.Dto.Error;
using CleanProFinder.Shared.Dto.Premises;
using CleanProFinder.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanProFinder.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremiseController : BaseController
    {
        private readonly IMediator _mediator;

        public PremiseController(IMediator mediator, IMapper mapper)
            : base(mapper)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new user premise
        /// </summary>
        /// <param name="request">The request to create new user premise.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If the operation is successful, it will return a UserPremiseViewInfo.
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpPost("create")]
        [Authorize(Roles = Roles.ServiceUser)]
        [ProducesResponseType(typeof(OwnPremiseFullInfoDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> EditPremise(CreatePremiseCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Edit existed user premise
        /// </summary>
        /// <param name="request">The request to edit existed user premise.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If the operation is successful, it will return a UserPremiseViewInfo.
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpPost("edit")]
        [Authorize(Roles = Roles.ServiceUser)]
        [ProducesResponseType(typeof(OwnPremiseFullInfoDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> EditPremise(EditPremiseCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Get user premises
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If the operation is successful, it will return a List of OwnPremiseShortInfoDto.
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpGet("my-premises")]
        [Authorize(Roles = Roles.ServiceUser)]
        [ProducesResponseType(typeof(List<OwnPremiseShortInfoDto>), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> GetPremises(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetOwnPremisesQuery(), cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Get user premise full info
        /// </summary>
        /// <param name="premiseId">Premise id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If the operation is successful, it will return a List of OwnPremiseFullInfoDto.
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpGet("full-info")]
        [ProducesResponseType(typeof(OwnPremiseFullInfoDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> GetPremiseFullInfo(Guid premiseId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetOwnPremiseFullInfoQuery() { PremiseId = premiseId }, cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Delete user premise
        /// </summary>
        /// <param name="premiseId">Premise id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpDelete]
        [Authorize(Roles = Roles.ServiceUser)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> Delete(Guid premiseId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeletePremiseCommand() { PremiseId = premiseId }, cancellationToken);
            return ConvertFromServiceResponse(result);
        }
    }
}
