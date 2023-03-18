using Bogus;
using Tamagotchi.Data.Models;

namespace Tamagotchi.Tests.Fakes.Models;

public sealed class FoodsFaker : Faker<Food>
{
    public FoodsFaker(int speciesId)
    {
        RuleFor(x => x.Id, prop => speciesId);
        RuleFor(x => x.Name, prop => prop.Name.FirstName());
        RuleFor(x => x.SpeciesId, _ => speciesId);
        RuleFor(x => x.WeightGainKg, _ => _.Random.Int(1, 2));
    }
}