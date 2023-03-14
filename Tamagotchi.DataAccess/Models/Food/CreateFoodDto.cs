namespace Tamagotchi.DataAccess.Models.Food;

/// <summary>
/// The create food transfer object
/// </summary>
public record CreateFoodDto
{
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
    public int SpeciesId { get; set; }
};