using Tamagotchi.Data.Models;

namespace Tamagotchi.Data.Repositories;

public class FoodsRepository : RepositoryBase<Food, TamagotchiDbContext>
{
    public FoodsRepository(TamagotchiDbContext context) : base(context)
    {
        
    }
}