using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tamagotchi.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    MaxAge = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    MaxWeight = table.Column<int>(type: "integer", nullable: false, defaultValue: 10),
                    HungerRate = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0.1m),
                    HappinessRate = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0.1m),
                    AgeRate = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0.1m),
                    TickRateMs = table.Column<int>(type: "integer", nullable: false, defaultValue: 5000)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    WeightGainKg = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    SpeciesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Weight = table.Column<int>(type: "integer", nullable: false, defaultValue: 10),
                    Happiness = table.Column<string>(type: "text", nullable: false, defaultValue: "Neutral"),
                    Hunger = table.Column<string>(type: "text", nullable: false, defaultValue: "Neutral"),
                    LastInteraction = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValue: new DateTime(2023, 3, 11, 9, 50, 40, 872, DateTimeKind.Utc).AddTicks(1380)),
                    SpeciesId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Species",
                columns: new[] { "Id", "HappinessRate", "HungerRate", "MaxWeight", "Name" },
                values: new object[] { 1, 0.05m, 0.1m, 100, "Dragon" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Username" },
                values: new object[] { 1, "$2a$12$6.MBf7B04S.IRrrP5FFc.uYx8yAX5ntsVuAYBxyLt09C4hQLiIVs.", "admin" });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Name", "SpeciesId" },
                values: new object[] { 1, "Cooked Ham", 1 });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "Name", "SpeciesId", "UserId" },
                values: new object[] { 1, "Toothless", 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_SpeciesId",
                table: "Foods",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_SpeciesId",
                table: "Pets",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_UserId",
                table: "Pets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
