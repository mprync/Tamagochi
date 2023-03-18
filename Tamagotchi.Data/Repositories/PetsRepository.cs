using Tamagotchi.Data.Models;

namespace Tamagotchi.Data.Repositories;

public class PetsRepository : RepositoryBase<Pet, TamagotchiDbContext>
{
    public PetsRepository(TamagotchiDbContext context) : base(context)
    {
        
    }
}