using Tamagotchi.DataAccess.Models.Food;

namespace Tamagotchi.DataAccess.Models.Species;

public record SpeciesDto
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The name of the species
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The maximum age of the species, once reached the pet will die
    /// </summary>
    public int MaxAge { get; set; }
    
    /// <summary>
    /// The maximum weight of the species, the lower the weight, the more happiness the pet will receive when fed
    /// </summary>
    public int MaxWeight { get; set; }
    
    /// <summary>
    /// The hunter decrease rate of the species every <see cref="TickRateMs"/>
    /// </summary>
    public decimal HungerRate { get; set; }

    /// <summary>
    /// The rate at which the pet ages by a year, every <see cref="TickRateMs"/>. Per tick will add this value to the age 
    /// </summary>
    public decimal AgeRate { get; set; }
    
    /// <summary>
    /// The tick rate for each stat for the species, every x ms the pet will lose age, weight, happiness and hunger
    /// </summary>
    public int TickRateMs { get; set; }

    /// <summary>
    /// Many relationship to the <see cref="Food"/> table
    /// </summary>
    public virtual IEnumerable<FoodDto>? Foods { get; set; }
    
    /// <summary>
    /// Converts a <see cref="Data.Models.Species"/> to a <see cref="SpeciesDto"/>
    /// </summary>
    /// <param name="species"></param>
    /// <returns></returns>
    public static SpeciesDto FromSpecies(Data.Models.Species species)
    {
        return new SpeciesDto
        {
            Id = species.Id,
            Name = species.Name,
            MaxAge = species.MaxAge,
            MaxWeight = species.MaxWeight,
            HungerRate = species.HungerRate,
            AgeRate = species.AgeRate,
            TickRateMs = species.TickRateMs,
            Foods = species.Foods?.Select(FoodDto.FromFood)
        };
    }
}