using Tamagotchi.Data.Models;

namespace Tamagotchi.Data.Repositories;

public class UsersRepository : RepositoryBase<User, TamagotchiDbContext>
{
    public UsersRepository(TamagotchiDbContext context) : base(context)
    {
        
    }
}