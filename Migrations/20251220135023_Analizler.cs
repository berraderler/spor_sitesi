using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spor_web_sitesi.Migrations
{
    /// <inheritdoc />
    public partial class Analizler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analizler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnalizTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UyeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kilo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoyCm = table.Column<int>(type: "int", nullable: false),
                    AnalizOzeti = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analizler", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0ced721b-eb99-4877-b84b-eeaea5988f37", "AQAAAAIAAYagAAAAEOPeH1ROIDNqunxToaUnD0TOHCX4zimeJHfDCWBhXMQd1MJdtsvbBHnUjb4iLG+2vA==", "8d206548-ba45-4998-99c2-e8e8bec790ef" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analizler");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fbb80842-d036-4607-ada4-9705540576e9", "AQAAAAIAAYagAAAAENWT+7+6LcesHRjIc2d2gZapLTBVY9czAUWdWabAO+/eFvzldOUIfO5yCTaE/OF6PA==", "57ffa8c5-9942-42fc-abc6-e3bca2f85fc6" });
        }
    }
}
