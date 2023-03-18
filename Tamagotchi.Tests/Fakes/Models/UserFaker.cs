using Bogus;
using Tamagotchi.Data.Models;

namespace Tamagotchi.Tests.Fakes.Models;

public class UserFaker : Faker<User>
{
    public UserFaker()
    {
        RuleFor(x => x.Id, prop => prop.IndexFaker + 1);
        RuleFor(x => x.Username, prop => prop.Name.FirstName());
        RuleFor(x => x.PasswordHash, prop => prop.Internet.Random.Hash());
    }
}