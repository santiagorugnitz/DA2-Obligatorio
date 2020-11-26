using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class migration16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Accommodations_accommodationId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "accommodationId",
                table: "Reservations",
                newName: "AccommodationId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_accommodationId",
                table: "Reservations",
                newName: "IX_Reservations_AccommodationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Accommodations_AccommodationId",
                table: "Reservations",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Accommodations_AccommodationId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "AccommodationId",
                table: "Reservations",
                newName: "accommodationId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_AccommodationId",
                table: "Reservations",
                newName: "IX_Reservations_accommodationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Accommodations_accommodationId",
                table: "Reservations",
                column: "accommodationId",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
