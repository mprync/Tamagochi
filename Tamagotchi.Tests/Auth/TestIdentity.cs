using System.Security.Claims;

namespace Tamagotchi.Tests.Auth;

public class TestIdentity : ClaimsIdentity
{
    public TestIdentity(params Claim[] claims) : base(claims)
    {
    }
}