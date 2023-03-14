using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tamagotchi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPetIsDeadProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HappinessRate",
                table: "Species");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Species",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaxAge",
                table: "Species",
                type: "integer",
                nullable: false,
                defaultValue: 10,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastPetting",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(8990),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8400));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastFed",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(8860),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8250));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(9120),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8510));

            migrationBuilder.AlterColumn<decimal>(
                name: "Age",
                table: "Pets",
                type: "numeric",
                nullable: false,
                defaultValue: 0.0m,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDead",
                table: "Pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Age", "CreatedAt", "Happiness", "Hunger", "LastFed", "LastPetting", "LifeStage" },
                values: new object[] { 0.0m, new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(9120), "Neutral", "Neutral", new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(8860), new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(8990), "Baby" });

            migrationBuilder.UpdateData(
                table: "Species",
                keyColumn: "Id",
                keyValue: 1,
                column: "MaxAge",
                value: 200);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDead",
                table: "Pets");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Species",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "MaxAge",
                table: "Species",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 10);

            migrationBuilder.AddColumn<decimal>(
                name: "HappinessRate",
                table: "Species",
                type: "numeric",
                nullable: false,
                defaultValue: 0.1m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastPetting",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8400),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(8990));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastFed",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8250),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(8860));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8510),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 3, 12, 16, 44, 26, 262, DateTimeKind.Utc).AddTicks(9120));

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Pets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldDefaultValue: 0.0m);

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Age", "CreatedAt", "Happiness", "Hunger", "LastFed", "LastPetting", "LifeStage" },
                values: new object[] { 0, new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8510), "Unknown", "Unknown", new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8250), new DateTime(2023, 3, 12, 8, 28, 17, 461, DateTimeKind.Utc).AddTicks(8400), "Unknown" });

            migrationBuilder.UpdateData(
                table: "Species",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HappinessRate", "MaxAge" },
                values: new object[] { 0.05m, 0 });
        }
    }
}
