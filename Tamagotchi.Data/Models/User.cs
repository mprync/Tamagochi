namespace Tamagotchi.Data.Models;

/// <summary>
/// Database table for the user table
/// </summary>
public class User
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The users username
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// The users hashed password
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    /// The users tamagotchi pets
    /// </summary>
    public virtual ICollection<Pet> Pets { get; set; }
}