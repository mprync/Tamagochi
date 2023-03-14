using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tamagotchi.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedNullables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastPetting",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3570),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(8990));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastFed",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3470),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(8860));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3700),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(9120));

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Happiness", "Hunger", "LastFed", "LastPetting", "LifeStage" },
                values: new object[] { new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3700), "Neutral", "Neutral", new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3470), new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3570), "Baby" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastPetting",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(8990),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3570));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastFed",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(8860),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3470));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(9120),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3700));

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Happiness", "Hunger", "LastFed", "LastPetting", "LifeStage" },
                values: new object[] { new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(9120), "Unknown", "Unknown", new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(8860), new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(8990), "Unknown" });
        }
    }
}
