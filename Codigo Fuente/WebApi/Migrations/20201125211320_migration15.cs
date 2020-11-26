using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class migration15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accommodations_TouristSpots_TouristSpotId",
                table: "accommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_accommodations_AccommodationId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_accommodations_accommodationId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_accommodations",
                table: "accommodations");

            migrationBuilder.RenameTable(
                name: "accommodations",
                newName: "Accommodations");

            migrationBuilder.RenameIndex(
                name: "IX_accommodations_TouristSpotId",
                table: "Accommodations",
                newName: "IX_Accommodations_TouristSpotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accommodations",
                table: "Accommodations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_TouristSpots_TouristSpotId",
                table: "Accommodations",
                column: "TouristSpotId",
                principalTable: "TouristSpots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Accommodations_AccommodationId",
                table: "Images",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Accommodations_accommodationId",
                table: "Reservations",
                column: "accommodationId",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_TouristSpots_TouristSpotId",
                table: "Accommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Accommodations_AccommodationId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Accommodations_accommodationId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accommodations",
                table: "Accommodations");

            migrationBuilder.RenameTable(
                name: "Accommodations",
                newName: "accommodations");

            migrationBuilder.RenameIndex(
                name: "IX_Accommodations_TouristSpotId",
                table: "accommodations",
                newName: "IX_accommodations_TouristSpotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_accommodations",
                table: "accommodations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_accommodations_TouristSpots_TouristSpotId",
                table: "accommodations",
                column: "TouristSpotId",
                principalTable: "TouristSpots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_accommodations_AccommodationId",
                table: "Images",
                column: "AccommodationId",
                principalTable: "accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_accommodations_accommodationId",
                table: "Reservations",
                column: "accommodationId",
                principalTable: "accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
