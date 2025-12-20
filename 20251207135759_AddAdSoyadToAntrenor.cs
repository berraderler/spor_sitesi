using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace spor_sitesi.Migrations
{
    /// <inheritdoc />
    public partial class AddAdSoyadToAntrenor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdSoyad",
                table: "Antrenorler",
                newName: "Soyad");

            migrationBuilder.AddColumn<string>(
                name: "Ad",
                table: "Antrenorler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ad",
                table: "Antrenorler");

            migrationBuilder.RenameColumn(
                name: "Soyad",
                table: "Antrenorler",
                newName: "AdSoyad");
        }
    }
}
