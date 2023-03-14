using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tamagotchi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPettingAndFeedingTimestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastInteraction",
                table: "Pets");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastFed",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 11, 18, 7, 31, 407, DateTimeKind.Utc).AddTicks(8230));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPetting",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 11, 18, 7, 31, 407, DateTimeKind.Utc).AddTicks(8360));

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Happiness", "Hunger", "LifeStage" },
                values: new object[] { "Neutral", "Neutral", "Baby" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastFed",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "LastPetting",
                table: "Pets");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastInteraction",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 11, 17, 9, 35, 266, DateTimeKind.Utc).AddTicks(4540));

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Happiness", "Hunger", "LifeStage" },
                values: new object[] { "Unknown", "Unknown", "Unknown" });
        }
    }
}
