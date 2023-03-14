using System.ComponentModel.DataAnnotations;

namespace Tamagotchi.DataAccess.Models.Food;

/// <summary>
/// Update food transfer object
/// </summary>
/// <param name="Name"></param>
/// <param name="WeightGainKg"></param>
public record UpdateFoodDto([Required] string Name, [Required] int WeightGainKg);