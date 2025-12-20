using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace spor_sitesi.Migrations
{
    /// <inheritdoc />
    public partial class AddAntrenorHizmet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AntrenorHizmetler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AntrenorId = table.Column<int>(type: "int", nullable: false),
                    HizmetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntrenorHizmetler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AntrenorHizmetler_Antrenorler_AntrenorId",
                        column: x => x.AntrenorId,
                        principalTable: "Antrenorler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntrenorHizmetler_Hizmetler_HizmetId",
                        column: x => x.HizmetId,
                        principalTable: "Hizmetler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AntrenorHizmetler_AntrenorId",
                table: "AntrenorHizmetler",
                column: "AntrenorId");

            migrationBuilder.CreateIndex(
                name: "IX_AntrenorHizmetler_HizmetId",
                table: "AntrenorHizmetler",
                column: "HizmetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntrenorHizmetler");
        }
    }
}
