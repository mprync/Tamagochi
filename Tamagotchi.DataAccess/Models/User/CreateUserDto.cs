using System.ComponentModel.DataAnnotations;

namespace Tamagotchi.DataAccess.Models.User;

/// <summary>
/// Create user data transfer object
/// </summary>
/// <param name="Username"></param>
/// <param name="Password"></param>
public record CreateUserDto([Required] string Username, [Required] string Password);