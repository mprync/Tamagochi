using System.ComponentModel.DataAnnotations;

namespace Tamagotchi.DataAccess.Models.User;

/// <summary>
/// User login data transfer object
/// </summary>
/// <param name="Username"></param>
/// <param name="Password"></param>
public record LoginUserDto([Required] string Username, [Required] string Password);