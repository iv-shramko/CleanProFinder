using AutoMapper;
using CleanProFinder.Server.Controllers.Base;
using CleanProFinder.Server.Features.Account;
using CleanProFinder.Shared.Dto.Account;
using CleanProFinder.Shared.Dto.Error;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanProFinder.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator, IMapper mapper)
            :base(mapper)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new service user account.
        /// </summary>
        /// <param name="request">The request to create a service user account.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// If the operation is successful, it will return a SignUpResultDto.
        /// If there is a bad request, it will return an ErrorDto.
        /// </remarks>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpPost("service-user/account/create")]
        [ProducesResponseType(typeof(SignUpResultDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> SignUpServiceUser(CreateServiceUserCommand request, 
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return ConvertFromServiceResponse(result);
        }

        [HttpPost("service-provider/account/create")]
        [ProducesResponseType(typeof(SignUpResultDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        public async Task<IActionResult> SignUpServiceProvider(CreateServiceProviderCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return ConvertFromServiceResponse(result);
        }
    }
}
