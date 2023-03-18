using System.Security.Claims;

namespace Tamagotchi.Tests.Auth;

public class TestPrincipal : ClaimsPrincipal
{
    public TestPrincipal(params Claim[] claims) : base(new TestIdentity(claims))
    {
    }
}