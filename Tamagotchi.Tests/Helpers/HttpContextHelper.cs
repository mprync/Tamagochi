using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tamagotchi.Tests.Helpers;

public static class HttpContextHelper
{
    public static void AddAuthentication(ControllerBase controller, int userId)
    {
        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new GenericIdentity("TestUser"))
        };
        
        controller.HttpContext.User.AddIdentity(new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        }));
        
        var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), 
            new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor());
        controller.ControllerContext = new ControllerContext(actionContext);
    }
}