using Tamagotchi.API.Actions;
using Tamagotchi.DataAccess.Models.User;
using Tamagotchi.DataAccess.Responses;

namespace Tamagotchi.API.Services.Interfaces;

public interface IUsersService
{
    /// <summary>
    /// Create a user
    /// </summary>
    /// <param name="createUserDto"></param>
    /// <returns></returns>
    Task<HttpActionResult<Response>> CreateUser(CreateUserDto createUserDto);
    
    /// <summary>
    /// Log the user in, returning a JWT token
    /// </summary>
    /// <param name="loginUserDto"></param>
    /// <returns></returns>
    Task<HttpActionResult<UserJwtDto>> Login(LoginUserDto loginUserDto);
}