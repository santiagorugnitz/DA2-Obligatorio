using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class migration14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_accommodations_accommodationId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "accommodationId",
                table: "Images",
                newName: "AccommodationId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_accommodationId",
                table: "Images",
                newName: "IX_Images_AccommodationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_accommodations_AccommodationId",
                table: "Images",
                column: "AccommodationId",
                principalTable: "accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_accommodations_AccommodationId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "AccommodationId",
                table: "Images",
                newName: "accommodationId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_AccommodationId",
                table: "Images",
                newName: "IX_Images_accommodationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_accommodations_accommodationId",
                table: "Images",
                column: "accommodationId",
                principalTable: "accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
