using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using Tamagotchi.Data.Enums;

namespace Tamagotchi.DataAccess.Models.Pet;

/// <summary>
/// The data transfer object for a pet
/// </summary>
public record PetDto
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the pet
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Age of the pet, in years
    /// </summary>
    [Required]
    public decimal Age { get; set; }

    /// <summary>
    /// Weight of the pet, value in kg
    /// </summary>
    [Required]
    public int Weight { get; set; }

    /// <summary>
    /// The Happiness of the pet, <see cref="HungerLevelType"/>. Value will decrease over time.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    [Required]
    public HappinessLevelType Happiness { get; set; }

    /// <summary>
    /// The pets hunter levels, <see cref="HungerLevelType"/>. Value will decrease over time.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    [Required]
    public HungerLevelType Hunger { get; set; }

    /// <summary>
    /// The life stage of the pet, <see cref="PetLifeStageType"/>
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    [Required]
    public PetLifeStageType LifeStage { get; set; }
    
    /// <summary>
    /// Is the pet alive? :(
    /// </summary>
    public bool IsDead { get; set; }

    /// <summary>
    /// The type of species this pet is
    /// </summary>
    [Required]
    public string? Species { get; set; }

    /// <summary>
    /// The last time the user fed the pet
    /// </summary>
    [Required]
    public DateTime? LastFed { get; set; }

    /// <summary>
    /// The last time the user pet their pet
    /// </summary>
    [Required]
    public DateTime? LastPetting { get; set; }
    
    /// <summary>
    /// The last time the user pet their pet
    /// </summary>
    [Required]
    public DateTime? BornAt { get; set; }

    /// <summary>
    /// Factory method to create a new instance of <see cref="PetDto"/> from a <see cref="Data.Models.Pet"/>
    /// </summary>
    /// <param name="pet"></param>
    /// <returns></returns>
    public static PetDto FromPet(Data.Models.Pet pet)
    {
        return new PetDto
        {
            Id = pet.Id,
            Name = pet.Name,
            Age = pet.Age,
            Weight = pet.Weight,
            Happiness = pet.Happiness,
            Hunger = pet.Hunger,
            LifeStage = pet.LifeStage,
            Species = pet.Species?.Name,
            LastFed = pet.LastFed,
            LastPetting = pet.LastPetting,
            BornAt = pet.CreatedAt,
            IsDead = pet.IsDead
        };
    }
};