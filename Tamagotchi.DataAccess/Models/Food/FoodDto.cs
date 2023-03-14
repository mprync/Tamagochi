using Tamagotchi.DataAccess.Models.Species;

namespace Tamagotchi.DataAccess.Models.Food;

/// <summary>
/// The food DTO
/// </summary>
public record FoodDto()
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
    /// Species data transfer object
    /// </summary>
    public SpeciesDto Species { get; set; }

    public static FoodDto FromFood(Data.Models.Food food)
    {
        return new FoodDto
        {
            Id = food.Id,
            Name = food.Name,
            WeightGainKg = food.WeightGainKg
        };
    }
}