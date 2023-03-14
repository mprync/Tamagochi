using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.API.Actions;
using Tamagotchi.API.Helpers;
using Tamagotchi.API.Security;
using Tamagotchi.API.Services.Interfaces;
using Tamagotchi.Data;
using Tamagotchi.Data.Models;
using Tamagotchi.DataAccess.Models.User;
using Tamagotchi.DataAccess.Responses;

namespace Tamagotchi.API.Services;

/// <summary>
/// The users service
///
/// TODO: Add logging
/// </summary>
public class UserService : IUsersService
{
    private readonly TamagotchiDbContext _dbContext;
    private readonly JwtConfig _jwtConfig;

    public UserService(TamagotchiDbContext dbContext, JwtConfig jwtConfig)
    {
        _dbContext = dbContext;
        _jwtConfig = jwtConfig;
    }

    /// <inheritdoc/>
    public async Task<HttpActionResult<Response>> CreateUser(CreateUserDto createUserDto)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

        if (_dbContext.Users.Any(u => u.Username == createUserDto.Username))
        {
            return await HttpActionResult<Response>.Error(
                StatusCodes.Status400BadRequest,
                "User already exists");
        }

        await _dbContext.Users.AddAsync(new User
        {
            Username = createUserDto.Username,
            PasswordHash = passwordHash
        });

        await _dbContext.SaveChangesAsync();

        return await HttpActionResult<Response>.Success(
            StatusCodes.Status200OK,
            new Response
            {
                Title = "Create New User",
                Detail = "User created successfully",
            });
    }

    /// <inheritdoc/>
    public async Task<HttpActionResult<UserJwtDto>> Login(LoginUserDto loginUserDto)
    {
        var user = _dbContext.Users
            .AsNoTracking()
            .FirstOrDefault(u => u.Username == loginUserDto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.PasswordHash))
        {
            return await HttpActionResult<UserJwtDto>.Error(
                StatusCodes.Status400BadRequest,
                "Username or password is incorrect");
        }

        // User claims should be apart of an Identity user but for the sake of this test we'll just add claims manually
        return await HttpActionResult<UserJwtDto>.Success(
            StatusCodes.Status200OK,
            new UserJwtDto
            (
                JwtHelper.GenerateJwtToken(_jwtConfig, new List<Claim>
                {
                    new(ClaimTypes.Name, user.Username),
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                }, new List<string>
                {
                    user.Username == "admin" ? "Admin" : ""
                })
            ));
    }
}