using AutoMapper;
using CleanProFinder.Server.Controllers.Base;
using CleanProFinder.Server.Features.ProviderRestrictions;
using CleanProFinder.Server.Features.UserRestrictions;
using CleanProFinder.Shared.Dto.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanProFinder.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestrictionsController : BaseController
    {
        private readonly IMediator _mediator;

        public RestrictionsController(IMediator mediator, IMapper mapper)
            : base(mapper)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Change restrictions for a provider.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpGet("provider/change-restrictions/{id}")]
/*        [Authorize(Roles = Roles.Administrator)]
*/        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> ChangeServiceProviderRestrictions(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ChangeServiceProviderRestrictionsCommand { ServiceProviderId = id }, cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Change restrictions for a user.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpGet("user/change-restrictions/{id}")]
/*        [Authorize(Roles = Roles.Administrator)]
*/        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> ChangeServiceUserRestrictions(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ChangeServiceUserRestrictionsCommand { ServiceUserId = id }, cancellationToken);
            return ConvertFromServiceResponse(result);
        }
    }
}
