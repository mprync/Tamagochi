namespace Tamagotchi.Data.Models.Interfaces;

/// <summary>
/// Base entity interface
/// </summary>
public interface IEntity
{
    /// <summary>
    /// The id of the entity, usually a primary key
    /// </summary>
    int Id { get; set; }
}