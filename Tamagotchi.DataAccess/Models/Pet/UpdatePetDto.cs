using System.ComponentModel.DataAnnotations;

namespace Tamagotchi.DataAccess.Models.Pet;

/// <summary>
/// The data transfer object for renaming a pet
/// </summary>
public record UpdatePetDto([Required] string NewName);