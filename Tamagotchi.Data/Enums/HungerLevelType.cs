namespace Tamagotchi.Data.Enums;

/// <summary>
/// The happiness levels for a pet.
/// </summary>
public enum HungerLevelType
{
    /// <summary>
    /// Should never be unknown!
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// The pet is very hungry!
    /// </summary>
    Hungry = 1,
    
    /// <summary>
    /// The pet seems to be ok, maybe..
    /// </summary>
    Neutral = 2,
    
    /// <summary>
    /// The pet is full, yay!
    /// </summary>
    Full = 3
}