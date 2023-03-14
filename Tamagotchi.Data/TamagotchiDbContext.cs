using Microsoft.EntityFrameworkCore;
using Tamagotchi.Data.Configuration;
using Tamagotchi.Data.Models;

namespace Tamagotchi.Data;

public class TamagotchiDbContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Species> Species { get; set; }
    public virtual DbSet<Food> Foods { get; set; }
    public virtual DbSet<Pet> Pets { get; set; }

    public TamagotchiDbContext()
    {
    }

    public TamagotchiDbContext(DbContextOptions<TamagotchiDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SpeciesConfiguration());
        modelBuilder.ApplyConfiguration(new FoodConfiguration());
        modelBuilder.ApplyConfiguration(new PetConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}