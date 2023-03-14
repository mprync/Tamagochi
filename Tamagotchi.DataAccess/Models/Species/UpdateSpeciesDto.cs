namespace Tamagotchi.DataAccess.Models.Species;

/// <summary>
/// Update species data transfer object
/// </summary>
public record UpdateSpeciesDto
{
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
}