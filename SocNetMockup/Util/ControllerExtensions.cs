using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocNetMockup.Data.ApiResponse;

namespace SocNetMockup.Util
{
    public static class ControllerExtensions
    {
        public static IActionResult ApiOk(this ControllerBase controller)
        {
            return controller.Ok(new ApiResponse(new {}));
        }
        
        public static IActionResult ApiOk(this ControllerBase controller, object response)
        {
            return controller.Ok(new ApiResponse(response));
        }
        
        public static IActionResult ApiOk(this ControllerBase controller, IEnumerable<object> response)
        {
            return controller.Ok(new ApiResponse(response));
        }
        
        public static IActionResult ApiErr(this ControllerBase controller, ApiError error)
        {
            return controller.BadRequest(error);
        }
    }
}
