using Tamagotchi.Data.Models;
using Tamagotchi.Data.Repositories;
using Tamagotchi.Data.Repositories.Interfaces;
using Tamagotchi.Data.UnitOfWork.Interfaces;

namespace Tamagotchi.Data.UnitOfWork;

public class UnitOfWork :IUnitOfWork
{
    private readonly TamagotchiDbContext _context;
    
    public UnitOfWork(TamagotchiDbContext context)
    {
        _context = context;
        
        Foods = new FoodsRepository(context);
        Pets = new PetsRepository(context);
        Species = new SpeciesRepository(context);
        Users = new UsersRepository(context);
    }
    
    public virtual IRepository<Food> Foods { get; }
    public virtual IRepository<Pet> Pets { get; }
    public virtual IRepository<Species> Species { get; }
    public virtual IRepository<User> Users { get; }

    public virtual async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}