using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tamagotchi.Data.Models;

namespace Tamagotchi.Data.Configuration
{
    public class FoodConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255).IsUnicode();
            builder.Property(x => x.WeightGainKg).IsRequired().HasDefaultValue(1);

            builder.HasOne(a => a.Species)
                .WithMany(b => b.Foods)
                .HasForeignKey(c => c.SpeciesId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(new Food
            {
                Id = 1,
                Name = "Cooked Ham",
                SpeciesId = 1 
            });
        }
    }
}