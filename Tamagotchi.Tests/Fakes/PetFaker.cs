using Bogus;
using Tamagotchi.Data.Enums;
using Tamagotchi.Data.Models;

namespace Tamagotchi.Tests.Fakes;

public sealed class PetFaker : Faker<Pet>
{
    public PetFaker()
    {
        RuleFor(x => x.Id, prop => 1);
        RuleFor(x => x.UserId, prop => 1);
        RuleFor(x => x.Hunger, prop => HungerLevelType.Full);
        RuleFor(x => x.Happiness, prop => HappinessLevelType.Happy);
        RuleFor(x => x.LifeStage, prop => PetLifeStageType.Baby);
        RuleFor(x => x.SpeciesId, _ => 1);
        RuleFor(x => x.Species, _ => new SpeciesFaker().Generate());
        RuleFor(x => x.Name, prop => prop.Name.FirstName());
        RuleFor(x => x.Age, prop => 0);
        RuleFor(x => x.Weight, prop => prop.Random.Int(5, 200));
        RuleFor(x => x.LastFed, prop => DateTime.UtcNow);
        RuleFor(x => x.LastPetting, prop => DateTime.UtcNow);
        RuleFor(x => x.CreatedAt, prop => DateTime.UtcNow);
    }
}