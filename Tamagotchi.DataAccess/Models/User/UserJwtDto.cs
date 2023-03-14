using System.ComponentModel.DataAnnotations;

namespace Tamagotchi.DataAccess.Models.User;

/// <summary>
/// User JWT token data transfer object
/// </summary>
/// <param name="JwtToken"></param>
public record UserJwtDto([Required] string JwtToken);