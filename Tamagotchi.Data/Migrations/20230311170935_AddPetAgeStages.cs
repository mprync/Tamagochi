using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tamagotchi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPetAgeStages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastInteraction",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 11, 17, 9, 35, 266, DateTimeKind.Utc).AddTicks(4540),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 11, 9, 50, 40, 872, DateTimeKind.Utc).AddTicks(1380));

            migrationBuilder.AddColumn<string>(
                name: "LifeStage",
                table: "Pets",
                type: "text",
                nullable: false,
                defaultValue: "Baby");

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Happiness", "Hunger", "LastInteraction" },
                values: new object[] { "Neutral", "Neutral", new DateTime(2023, 3, 11, 17, 9, 35, 266, DateTimeKind.Utc).AddTicks(4540) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LifeStage",
                table: "Pets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastInteraction",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 11, 9, 50, 40, 872, DateTimeKind.Utc).AddTicks(1380),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 11, 17, 9, 35, 266, DateTimeKind.Utc).AddTicks(4540));

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Happiness", "Hunger", "LastInteraction" },
                values: new object[] { "Unknown", "Unknown", new DateTime(2023, 3, 11, 9, 50, 40, 872, DateTimeKind.Utc).AddTicks(1380) });
        }
    }
}
