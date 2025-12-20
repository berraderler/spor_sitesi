using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace spor_sitesi.Migrations
{
    /// <inheritdoc />
    public partial class AddTelefonToSalon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcilisSaati",
                table: "Salonlar");

            migrationBuilder.DropColumn(
                name: "KapanisSaati",
                table: "Salonlar");

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "Salonlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "Salonlar");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "AcilisSaati",
                table: "Salonlar",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "KapanisSaati",
                table: "Salonlar",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
