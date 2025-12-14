using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace spor_sitesi.Migrations
{
    /// <inheritdoc />
    public partial class AddSalonToRandevu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hizmetler_Salonlar_SalonId",
                table: "Hizmetler");

            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SalonId",
                table: "Hizmetler",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_SalonId",
                table: "Randevular",
                column: "SalonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hizmetler_Salonlar_SalonId",
                table: "Hizmetler",
                column: "SalonId",
                principalTable: "Salonlar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Salonlar_SalonId",
                table: "Randevular",
                column: "SalonId",
                principalTable: "Salonlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hizmetler_Salonlar_SalonId",
                table: "Hizmetler");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Salonlar_SalonId",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_SalonId",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "Randevular");

            migrationBuilder.AlterColumn<int>(
                name: "SalonId",
                table: "Hizmetler",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Hizmetler_Salonlar_SalonId",
                table: "Hizmetler",
                column: "SalonId",
                principalTable: "Salonlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
