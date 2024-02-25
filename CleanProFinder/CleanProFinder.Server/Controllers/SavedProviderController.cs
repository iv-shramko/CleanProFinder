using AutoMapper;
using CleanProFinder.Server.Controllers.Base;
using CleanProFinder.Server.Features.SavedProviders;
using CleanProFinder.Shared.Dto.Error;
using CleanProFinder.Shared.Dto.SavedProviders;
using CleanProFinder.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanProFinder.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedProviderController : BaseController
    {
        private readonly IMediator _mediator;

        public SavedProviderController(IMediator mediator, IMapper mapper)
            : base(mapper)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Save provider to own providers list.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpGet("my-saved-providers/save/{id}")]
        [Authorize(Roles = Roles.ServiceUser)]
        [ProducesResponseType(typeof(SavedProviderDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> SaveProvider(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new SaveProviderCommand { CleaningServiceProviderId = id }, cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Delete provider from own providers list.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpGet("my-saved-providers/delete/{id}")]
        [Authorize(Roles = Roles.ServiceUser)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> DeleteSavedProvider(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteSavedProviderCommand { CleaningServiceProviderId = id }, cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Get own providers list.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpGet("my-saved-providers")]
        [Authorize(Roles = Roles.ServiceUser)]
        [ProducesResponseType(typeof(List<SavedProviderDto>), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> GetOwnSavedProviders(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetOwnSavedProvidersQuery { }, cancellationToken);
            return ConvertFromServiceResponse(result);
        }
    }
}
