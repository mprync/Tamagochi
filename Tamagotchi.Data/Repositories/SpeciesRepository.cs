using Tamagotchi.Data.Models;

namespace Tamagotchi.Data.Repositories;

public class SpeciesRepository : RepositoryBase<Species, TamagotchiDbContext>
{
    public SpeciesRepository(TamagotchiDbContext context) : base(context)
    {
        
    }
}