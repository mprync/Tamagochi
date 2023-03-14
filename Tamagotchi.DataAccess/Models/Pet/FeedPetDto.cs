using System.ComponentModel.DataAnnotations;

namespace Tamagotchi.DataAccess.Models.Pet;

/// <summary>
/// The data transfer object for feeding a pet
/// </summary>
public record FeedPetDto([Required] int FoodId);