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
    
    public IRepository<Food> Foods { get; }
    public IRepository<Pet> Pets { get; }
    public IRepository<Species> Species { get; }
    public IRepository<User> Users { get; }

    public int Save()
    {
        return _context.SaveChanges();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}