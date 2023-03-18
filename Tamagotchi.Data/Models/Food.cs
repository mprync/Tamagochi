using System.ComponentModel.DataAnnotations.Schema;
using Tamagotchi.Data.Models.Interfaces;

namespace Tamagotchi.Data.Models;

/// <summary>
/// Database model for the Pet table
/// </summary>
public class Food : IEntity
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Name of the food
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// How much the food will increase the pets weight in kg
    /// </summary>
    public int WeightGainKg { get; set; }
    
    /// <summary>
    /// The species FK id
    /// </summary>
    [ForeignKey(nameof(Species))]
    public int? SpeciesId { get; set; }
    
    /// <summary>
    /// The FK relationship to the species table
    /// </summary>
    public virtual Species Species { get; set; }
}