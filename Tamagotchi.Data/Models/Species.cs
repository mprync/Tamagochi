using System.ComponentModel.DataAnnotations;
using Tamagotchi.Data.Models.Interfaces;

namespace Tamagotchi.Data.Models;

/// <summary>
/// Database model for the pet species table
/// </summary>
public class Species : IEntity
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
    [Range(0, 100, ErrorMessage = "The maximum age must be between 0 and 100 years")]
    public int MaxAge { get; set; }
    
    /// <summary>
    /// The maximum weight of the species, the lower the weight, the more happiness the pet will receive when fed
    /// </summary>
    [Range(1, 200, ErrorMessage = "The maximum weight must be between 1 and 200 kg")]
    public int MaxWeight { get; set; }
    
    /// <summary>
    /// The hunter decrease rate of the species every <see cref="TickRateMs"/>
    /// </summary>
    [Range(0.1f, 0.2f, ErrorMessage = "The hunger rate must be between 0.1 and 0.2")]
    public decimal HungerRate { get; set; }

    /// <summary>
    /// The rate at which the pet ages by a year, every <see cref="TickRateMs"/>. Per tick will add this value to the age 
    /// </summary>
    [Range(0.1f, 1.0f, ErrorMessage = "The age rate must be between 0.1 and 1 years")]
    public decimal AgeRate { get; set; }
    
    /// <summary>
    /// The tick rate for each stat for the species, every x ms the pet will lose age, weight, happiness and hunger
    /// </summary>
    [Range(1000, 10000, ErrorMessage = "The tick rate must be between 1000 and 10000 ms")]
    public int TickRateMs { get; set; }

    /// <summary>
    /// Many relationship to the <see cref="Food"/> table
    /// </summary>
    public virtual IEnumerable<Food>? Foods { get; set; }
}