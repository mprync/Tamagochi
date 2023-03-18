using Bogus;
using Tamagotchi.Data.Models;

namespace Tamagotchi.Tests.Fakes.Models;

public sealed class SpeciesFaker: Faker<Species>
{
    public SpeciesFaker()
    {
        RuleFor(x => x.Id, prop => prop.IndexFaker + 1);
        RuleFor(x => x.TickRateMs, _ => 5000);
        RuleFor(x => x.Name, prop => prop.Name.FirstName());
        RuleFor(x => x.Foods, prop => new FoodsFaker(prop.IndexFaker + 1).Generate(2));
        RuleFor(x => x.AgeRate, _ => _.Random.Decimal());
        RuleFor(x => x.HungerRate, _ => _.Random.Decimal());
        RuleFor(x => x.MaxAge, _ => _.Random.Int(10, 100));
        RuleFor(x => x.MaxWeight, _ => _.Random.Int(5, 200));
    }
}