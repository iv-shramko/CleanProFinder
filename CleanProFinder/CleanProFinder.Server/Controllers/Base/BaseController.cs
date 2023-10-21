using CleanProFinder.Shared.ServiceResponseHandling;
using Microsoft.AspNetCore.Mvc;

namespace CleanProFinder.Server.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult ConvertFromServiceResponse(ServiceResponse serviceResponse)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
