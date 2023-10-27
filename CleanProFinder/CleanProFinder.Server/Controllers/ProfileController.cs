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
    }
}
