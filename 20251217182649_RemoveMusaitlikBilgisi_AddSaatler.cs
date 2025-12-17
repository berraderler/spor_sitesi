using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace spor_sitesi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMusaitlikBilgisi_AddSaatler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MusaitlikBilgisi",
                table: "Antrenorler");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MusaitBaslangic",
                table: "Antrenorler",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MusaitBitis",
                table: "Antrenorler",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MusaitBaslangic",
                table: "Antrenorler");

            migrationBuilder.DropColumn(
                name: "MusaitBitis",
                table: "Antrenorler");

            migrationBuilder.AddColumn<string>(
                name: "MusaitlikBilgisi",
                table: "Antrenorler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
