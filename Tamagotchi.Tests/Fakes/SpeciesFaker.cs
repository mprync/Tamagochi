using Bogus;
using Tamagotchi.Data.Models;

namespace Tamagotchi.Tests.Fakes;

public sealed class SpeciesFaker: Faker<Species>
{
    public SpeciesFaker()
    {
        RuleFor(x => x.Id, _ => 1);
        RuleFor(x => x.TickRateMs, _ => 10000);
        RuleFor(x => x.Name, prop => prop.Name.FirstName());
        RuleFor(x => x.Foods, _ => new FoodsFaker().Generate(1));
        RuleFor(x => x.AgeRate, _ => _.Random.Decimal());
        RuleFor(x => x.HungerRate, _ => _.Random.Decimal());
        RuleFor(x => x.MaxAge, _ => _.Random.Int(10, 100));
        RuleFor(x => x.MaxWeight, _ => _.Random.Int(5, 200));
    }
}