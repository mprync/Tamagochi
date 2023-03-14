using Microsoft.AspNetCore.Mvc;
using Tamagotchi.API.Actions;
using Tamagotchi.API.Helpers;
using Tamagotchi.API.Services.Interfaces;
using Tamagotchi.DataAccess.Models.User;
using Tamagotchi.DataAccess.Responses;
using Tamagotchi.DataAccess.Responses.Errors;

namespace Tamagotchi.API.Controllers;

[ApiController]
[Produces("application/json")]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : ApiControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUsersService _service;

    public UsersController(ILogger<UsersController> logger, IUsersService service)
    {
        _logger = logger;
        _service = service;
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Response>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<ErrorResponse>))]
    [HttpPost]
    public async Task<HttpActionResult<Response>> Create([FromBody] CreateUserDto createUserDto)
    {
        try
        {
            _logger.LogInformation("Creating user");
            return await QueryHelper.ExecuteQuery(() => _service.CreateUser(createUserDto));
        }
        catch (Exception e)
        {
            _logger.LogError("Error creating user: {0}", e.Message);
            throw;
        }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<UserJwtDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<UserJwtDto>))]
    [HttpPost("login")]
    public async Task<HttpActionResult<UserJwtDto>> Login([FromBody] LoginUserDto loginUserDto)
    {
        try
        {
            _logger.LogInformation("Logging user in..");
            return await QueryHelper.ExecuteQuery(() => _service.Login(loginUserDto));
        }
        catch (Exception e)
        {
            _logger.LogError("Error logging user in: {0}", e.Message);
            throw;
        }
    }
}