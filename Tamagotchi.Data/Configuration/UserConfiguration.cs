using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tamagotchi.Data.Models;

namespace Tamagotchi.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Username).IsRequired().HasMaxLength(255).IsUnicode();
            builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(255).IsUnicode();

            builder.HasData(new User
            {
                Id = 1,
                Username = "admin",
                PasswordHash = "$2a$12$6.MBf7B04S.IRrrP5FFc.uYx8yAX5ntsVuAYBxyLt09C4hQLiIVs."
            });
        }
    }
}