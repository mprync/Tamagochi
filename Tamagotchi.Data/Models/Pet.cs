using System.ComponentModel.DataAnnotations.Schema;
using Tamagotchi.Data.Enums;
using Tamagotchi.Data.Extentions;
using Tamagotchi.Data.Models.Interfaces;

namespace Tamagotchi.Data.Models;

/// <summary>
/// Database model for the Pet table
/// </summary>
public class Pet : IEntity
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the pet
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Age of the pet, in years
    /// </summary>
    public decimal Age { get; set; }

    /// <summary>
    /// Weight of the pet, value in kg
    /// </summary>
    public int Weight { get; set; }

    /// <summary>
    /// Is the pet alive? :(
    /// </summary>
    public bool IsDead { get; set; }

    /// <summary>
    /// The Happiness of the pet, <see cref="HungerLevelType"/>. Value will decrease over time.
    /// </summary>
    public HappinessLevelType Happiness { get; set; }

    /// <summary>
    /// The pets hunter levels, <see cref="HungerLevelType"/>. Value will decrease over time.
    /// </summary>
    public HungerLevelType Hunger { get; set; }

    /// <summary>
    /// The life stage of the pet, <see cref="PetLifeStageType"/>
    /// </summary>
    public PetLifeStageType LifeStage { get; set; }

    /// <summary>
    /// The last time the user interacted with the pet
    /// </summary>
    public DateTime? LastFed { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The last time the user pet
    /// </summary>
    public DateTime? LastPetting { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Pet creation date
    /// </summary>
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The species FK id
    /// </summary>
    [ForeignKey(nameof(Species))]
    public int? SpeciesId { get; set; }

    /// <summary>
    /// The FK relationship to the species table
    /// </summary>
    public virtual Species? Species { get; set; }

    /// <summary>
    /// The FK id of the user table
    /// </summary>
    [ForeignKey(nameof(User))]
    public int? UserId { get; set; }

    /// <summary>
    /// The FK relationship to the user table
    /// </summary>
    public User? User { get; set; }

    /// <summary>
    /// Calculate the pets stats based on the time since the pet was created
    /// </summary>
    /// <returns></returns>
    public void CalculateStats()
    {
        if (IsDead)
        {
            // The pet is dead, no need to calculate stats
            return;
        }

        if (Species == null)
        {
            // There is something wrong here, we should not be able to get a pet without a species
            throw new Exception("This pet doesn't belong to a species!");
        }

        // Calculate the ticks since the pet was created, last fed and last petting
        var ticksSinceBorn = int.Clamp((int)(DateTime.UtcNow - CreatedAt!.Value).TotalMilliseconds / Species.TickRateMs, 0, int.MaxValue);
        var ticksSinceLastFed = int.Clamp((int)(DateTime.UtcNow - LastFed!.Value).TotalMilliseconds / Species.TickRateMs, 0, int.MaxValue);
        var ticksSinceLastPetting = int.Clamp((int)(DateTime.UtcNow - LastPetting!.Value).TotalMilliseconds / Species.TickRateMs, 0, int.MaxValue);

        // Set the multipliers for the happiness and hunger rates
        // Pets require less attention the older they get.
        var multiplier = (int)LifeStage switch
        {
            2 => 0.15M,
            3 => 0.1M,
            _ => 0.2M
        };

        // Calculate the age of the pet and if it is above the max age, the pet has died :(
        Age += Species.AgeRate * ticksSinceBorn;
        if (Age >= Species.MaxAge)
        {
            IsDead = true;
            return;
        }

        var ageNormalised = Age.Normalize(0, Species.MaxAge);

        LifeStage = ageNormalised switch
        {
            > 0.75M => PetLifeStageType.Adult,
            > 0.50M => PetLifeStageType.Teen,
            > 0.25M => PetLifeStageType.Child,
            _ => PetLifeStageType.Baby
        };

        Happiness = (multiplier * ticksSinceLastPetting) switch
        {
            > 0.66M => HappinessLevelType.Unhappy,
            > 0.33M => HappinessLevelType.Neutral,
            _ => HappinessLevelType.Happy
        };

        Hunger = (Species.HungerRate * multiplier * ticksSinceLastFed) switch
        {
            > 0.66M => HungerLevelType.Hungry,
            > 0.33M => HungerLevelType.Neutral,
            _ => HungerLevelType.Full
        };
    }
}