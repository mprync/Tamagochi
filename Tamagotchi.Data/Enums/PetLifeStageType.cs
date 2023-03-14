namespace Tamagotchi.Data.Enums;

/// <summary>
/// The types of life stages for a pet
/// </summary>
public enum PetLifeStageType
{
    /// <summary>
    /// Should never be known!
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// The pet is a cute baby!
    /// </summary>
    Baby = 1,
    
    /// <summary>
    /// The pet is less like a baby?
    /// </summary>
    Child = 2,
    
    /// <summary>
    /// The pet now has a teen attitude!
    /// </summary>
    Teen = 3,
    
    /// <summary>
    /// The pets responsibilities are now starting to kick in!
    /// </summary>
    Adult = 4,
}