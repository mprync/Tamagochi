using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tamagotchi.Data.Enums;
using Tamagotchi.Data.Models;

namespace Tamagotchi.Data.Configuration
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255).IsUnicode();
            builder.Property(x => x.Age).IsRequired().HasDefaultValue(0.0M);
            builder.Property(x => x.Weight).IsRequired().HasDefaultValue(10);
            builder.Property(x => x.IsDead).IsRequired().HasDefaultValue(false);

            builder.Property(pet => pet.Hunger)
                .HasConversion(e => e.ToString(), 
                    from => Enum.Parse<HungerLevelType>(from)).HasDefaultValue(HungerLevelType.Neutral);
            
            builder.Property(pet => pet.Happiness)
                .HasConversion(e => e.ToString(), 
                    from => Enum.Parse<HappinessLevelType>(from)).HasDefaultValue(HappinessLevelType.Neutral);
            
            builder.Property(pet => pet.LifeStage)
                .HasConversion(e => e.ToString(), 
                    from => Enum.Parse<PetLifeStageType>(from)).HasDefaultValue(PetLifeStageType.Baby);

            builder.Property(x => x.LastFed).IsRequired(false);
            builder.Property(x => x.LastPetting).IsRequired(false);
            builder.Property(x => x.CreatedAt).IsRequired(false);

            builder.HasOne(a => a.User)
                .WithMany(b => b.Pets)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(new Pet
            {
                Id = 1,
                Name = "Toothless",
                SpeciesId = 1,
                UserId = 1
            });
        }
    }
}