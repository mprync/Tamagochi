using Tamagotchi.Data.Models;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Data.UnitOfWork.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Food> Foods { get; }
    IRepository<Pet> Pets { get; }
    IRepository<Species> Species { get; }
    IRepository<User> Users { get; }
    Task<int> SaveAsync();
}