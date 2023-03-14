using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tamagotchi.Data.Models;

namespace Tamagotchi.Data.Configuration;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(255).IsUnicode();
        builder.Property(x => x.MaxAge).IsRequired().HasDefaultValue(10);
        builder.Property(x => x.MaxWeight).IsRequired().HasDefaultValue(10);
        builder.Property(x => x.HungerRate).IsRequired().HasDefaultValue(0.1M);
        builder.Property(x => x.AgeRate).IsRequired().HasDefaultValue(0.1M);
        builder.Property(x => x.TickRateMs).IsRequired().HasDefaultValue(5000);

        builder.HasData(new Species
        {
            Id = 1,
            Name = "Dragon",
            MaxAge = 200,
            MaxWeight = 100,
            HungerRate = 0.1M
        });
    }
}