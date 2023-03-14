﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tamagotchi.Data;

#nullable disable

namespace Tamagotchi.Data.Migrations
{
    [DbContext(typeof(TamagotchiDbContext))]
    [Migration("20230313125558_RemoveDefaultDateTimes")]
    partial class RemoveDefaultDateTimes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Tamagotchi.Data.Models.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("character varying(255)");

                    b.Property<int?>("SpeciesId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int>("WeightGainKg")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("SpeciesId");

                    b.ToTable("Foods");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Cooked Ham",
                            SpeciesId = 1,
                            WeightGainKg = 0
                        });
                });

            modelBuilder.Entity("Tamagotchi.Data.Models.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Age")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric")
                        .HasDefaultValue(0.0m);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Happiness")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("Neutral");

                    b.Property<string>("Hunger")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("Neutral");

                    b.Property<bool>("IsDead")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LastFed")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastPetting")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LifeStage")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("Baby");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("character varying(255)");

                    b.Property<int?>("SpeciesId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("Weight")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(10);

                    b.HasKey("Id");

                    b.HasIndex("SpeciesId");

                    b.HasIndex("UserId");

                    b.ToTable("Pets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 0m,
                            CreatedAt = new DateTime(2023, 3, 13, 12, 55, 58, 794, DateTimeKind.Utc).AddTicks(6470),
                            Happiness = "Unknown",
                            Hunger = "Unknown",
                            IsDead = false,
                            LastFed = new DateTime(2023, 3, 13, 12, 55, 58, 794, DateTimeKind.Utc).AddTicks(6460),
                            LastPetting = new DateTime(2023, 3, 13, 12, 55, 58, 794, DateTimeKind.Utc).AddTicks(6470),
                            LifeStage = "Unknown",
                            Name = "Toothless",
                            SpeciesId = 1,
                            UserId = 1,
                            Weight = 0
                        });
                });

            modelBuilder.Entity("Tamagotchi.Data.Models.Species", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AgeRate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric")
                        .HasDefaultValue(0.1m);

                    b.Property<decimal>("HungerRate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric")
                        .HasDefaultValue(0.1m);

                    b.Property<int>("MaxAge")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(10);

                    b.Property<int>("MaxWeight")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("TickRateMs")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(5000);

                    b.HasKey("Id");

                    b.ToTable("Species");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AgeRate = 0m,
                            HungerRate = 0.1m,
                            MaxAge = 200,
                            MaxWeight = 100,
                            Name = "Dragon",
                            TickRateMs = 0
                        });
                });

            modelBuilder.Entity("Tamagotchi.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PasswordHash = "$2a$12$6.MBf7B04S.IRrrP5FFc.uYx8yAX5ntsVuAYBxyLt09C4hQLiIVs.",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("Tamagotchi.Data.Models.Food", b =>
                {
                    b.HasOne("Tamagotchi.Data.Models.Species", "Species")
                        .WithMany("Foods")
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Species");
                });

            modelBuilder.Entity("Tamagotchi.Data.Models.Pet", b =>
                {
                    b.HasOne("Tamagotchi.Data.Models.Species", "Species")
                        .WithMany()
                        .HasForeignKey("SpeciesId");

                    b.HasOne("Tamagotchi.Data.Models.User", "User")
                        .WithMany("Pets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Species");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tamagotchi.Data.Models.Species", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("Tamagotchi.Data.Models.User", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
