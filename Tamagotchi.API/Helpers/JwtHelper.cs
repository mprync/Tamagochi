using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Tamagotchi.API.Security;

namespace Tamagotchi.API.Helpers;

public static class JwtHelper
{
    /// <summary>
    /// Generates a jwt token for the IdentityUser.
    /// </summary>
    /// <param name="jwtConfig">Holds jwt configurations.</param>
    /// <param name="claims"></param>
    /// <param name="roles">Roles.</param>
    /// <returns>Jwt token.</returns>
    public static string GenerateJwtToken(
        JwtConfig jwtConfig, 
        IEnumerable<Claim> claims,
        IEnumerable<string> roles)
    {
        var userClaims = new List<Claim>();
        userClaims.AddRange(claims);
        userClaims.AddRange(roles.Select(role => new Claim("role", role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret));
        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            userClaims,
            expires: DateTime.UtcNow.AddMilliseconds(jwtConfig.Expiry),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    /// <summary>
    /// Verify Jwt token
    /// </summary>
    /// <param name="jwtToken">Jwt token</param>
    /// <param name="tokenValidationParameters">Jwt token validation parameters</param>
    /// <param name="validatedJwtToken">The validated jwt token</param>
    /// <returns></returns>
    public static bool VerifyJwtToken(string jwtToken,
        TokenValidationParameters tokenValidationParameters, 
        out SecurityToken validatedJwtToken)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var principal = jwtTokenHandler.ValidateToken(jwtToken,
            tokenValidationParameters,
            out validatedJwtToken);

        // Check that the token has a valid security algorithm
        if (validatedJwtToken is JwtSecurityToken jwtSecurityToken)
        {
            if (!jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }
        }

        var expiryDateClaim = principal.Claims.FirstOrDefault(x =>
            x.Type == JwtRegisteredClaimNames.Exp);

        return expiryDateClaim != null;
    }
}