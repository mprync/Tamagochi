namespace Tamagotchi.Data.Enums;

/// <summary>
/// The happiness levels for a pet.
/// </summary>
public enum HappinessLevelType
{
    /// <summary>
    /// Should never be unknown!
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// The pet is very unhappy!
    /// </summary>
    Unhappy = 1,
    
    /// <summary>
    /// The pet seems to be ok, maybe..
    /// </summary>
    Neutral = 2,
    
    /// <summary>
    /// The pet is very happy, yay!
    /// </summary>
    Happy = 3
}