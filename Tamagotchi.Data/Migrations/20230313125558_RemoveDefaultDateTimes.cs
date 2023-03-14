using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tamagotchi.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDefaultDateTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastPetting",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3570));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastFed",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3470));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3700));

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Happiness", "Hunger", "LastFed", "LastPetting", "LifeStage" },
                values: new object[] { new DateTime(2023, 3, 13, 12, 55, 58, 794, DateTimeKind.Utc).AddTicks(6470), "Neutral", "Neutral", new DateTime(2023, 3, 13, 12, 55, 58, 794, DateTimeKind.Utc).AddTicks(6460), new DateTime(2023, 3, 13, 12, 55, 58, 794, DateTimeKind.Utc).AddTicks(6470), "Baby" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastPetting",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3570),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastFed",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3470),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3700),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Happiness", "Hunger", "LastFed", "LastPetting", "LifeStage" },
                values: new object[] { new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3700), "Unknown", "Unknown", new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3470), new DateTime(2023, 3, 13, 9, 53, 11, 934, DateTimeKind.Utc).AddTicks(3570), "Unknown" });
        }
    }
}
