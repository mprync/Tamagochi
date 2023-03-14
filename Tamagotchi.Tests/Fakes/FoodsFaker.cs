using Bogus;
using Tamagotchi.Data.Models;

namespace Tamagotchi.Tests.Fakes;

public sealed class FoodsFaker : Faker<Food>
{
    public FoodsFaker()
    {
        RuleFor(x => x.Id, _ => 1);
        RuleFor(x => x.Name, prop => prop.Name.FirstName());
        RuleFor(x => x.SpeciesId, _ => 1);
        RuleFor(x => x.WeightGainKg, _ => _.Random.Int(1, 2));
    }
}