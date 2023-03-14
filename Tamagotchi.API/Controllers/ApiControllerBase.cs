using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Tamagotchi.API.Controllers;

/// <summary>
/// A custom base controller for all API controllers
/// </summary>
public class ApiControllerBase : ControllerBase
{
    /// <summary>
    /// Get the user id from the claims
    /// </summary>
    /// <returns></returns>
    protected int GetUserId()
    {
        return int.Parse(User.Claims
            .First(i => i.Type == ClaimTypes.NameIdentifier).Value);
    }
}