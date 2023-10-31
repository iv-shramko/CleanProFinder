using AutoMapper;
using CleanProFinder.Server.Controllers.Base;
using CleanProFinder.Server.Features.Account;
using CleanProFinder.Server.Features.Profile;
using CleanProFinder.Shared.Dto.Account;
using CleanProFinder.Shared.Dto.Error;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanProFinder.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : BaseController
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator, IMapper mapper)
            : base(mapper)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get the info about service user profile.
        /// </summary>
        /// <param name="request">The request to get the info about service user profile.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If the operation is successful, it will return a ViewUserProfileInfoDto.
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpGet("service-user/info")]
        [Authorize(Roles = Roles.ServiceUser)]
        [ProducesResponseType(typeof(UserProfileViewInfoDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> GetUserProfileViewInfo()
        {
            var result = await _mediator.Send(new GetUserProfileViewInfoQuery());
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Get the info about service provider profile.
        /// </summary>
        /// <param name="request">The request to get the info about service provider profile.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If the operation is successful, it will return a ViewProviderProfileInfoDto.
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpGet("service-provider/info")]
        [Authorize(Roles = Roles.ServiceProvider)]
        [ProducesResponseType(typeof(ProviderProfileViewInfoDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> GetProviderProfileViewInfo()
        {
            var result = await _mediator.Send(new GetProviderProfileViewInfoQuery());
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Get the info about service providers for user.
        /// </summary>
        /// <param name="request">The request to get the info about service providers for user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If the operation is successful, it will return a List<ProviderPreviewDto>.
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpGet("service-user/providers")]
        [Authorize(Roles = Roles.ServiceUser)]
        [ProducesResponseType(typeof(List<ProviderPreviewDto>), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> GetProviderProfiles()
        {
            var result = await _mediator.Send(new GetProviderProfilesQuery());
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Update a service user profile.
        /// </summary>
        /// <param name="request">The request to update a service user profile.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If the operation is successful, it will return a UserProfileDto.
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpPost("service-user/edit")]
        [Authorize(Roles = Roles.ServiceUser)]
        [ProducesResponseType(typeof(UserProfileDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> EditServiceUserProfile(EditUserProfileCommand request,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        /// <summary>
        /// Update a service provider profile.
        /// </summary>
        /// <param name="request">The request to update a service provider profile.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If the operation is successful, it will return a ProviderProfileDto.
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpPost("service-provider/edit")]
        [Authorize(Roles = Roles.ServiceProvider)]
        [ProducesResponseType(typeof(ProviderProfileDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> EditServiceProviderProfile(EditProviderProfileCommand request,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return ConvertFromServiceResponse(result);
        }
    }
}
