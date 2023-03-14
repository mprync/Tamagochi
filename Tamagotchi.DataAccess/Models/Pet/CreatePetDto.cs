using System.ComponentModel.DataAnnotations;

namespace Tamagotchi.DataAccess.Models.Pet;

/// <summary>
/// The data transfer object for creating a new pet
/// </summary>
/// <param name="Name"></param>
/// <param name="SpeciesId"></param>
public record CreatePetDto([Required] string Name, [Required] int SpeciesId);