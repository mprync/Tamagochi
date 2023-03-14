using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tamagotchi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPetCreationTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastPetting",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8400),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 11, 18, 7, 31, 407, DateTimeKind.Utc).AddTicks(8360));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastFed",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8250),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 11, 18, 7, 31, 407, DateTimeKind.Utc).AddTicks(8230));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8510));

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Happiness", "Hunger", "LastFed", "LastPetting", "LifeStage" },
                values: new object[] { "Neutral", "Neutral", new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8250), new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8400), "Baby" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Pets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastPetting",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 11, 18, 7, 31, 407, DateTimeKind.Utc).AddTicks(8360),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8400));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastFed",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 11, 18, 7, 31, 407, DateTimeKind.Utc).AddTicks(8230),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8250));

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Happiness", "Hunger", "LastFed", "LastPetting", "LifeStage" },
                values: new object[] { "Unknown", "Unknown", new DateTime(2023, 3, 11, 18, 7, 31, 407, DateTimeKind.Utc).AddTicks(8230), new DateTime(2023, 3, 11, 18, 7, 31, 407, DateTimeKind.Utc).AddTicks(8360), "Unknown" });
        }
    }
}
