using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class Migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Accomodations_AccomodationId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ContactInformation",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Accomodations_AccomodationId",
                table: "Images",
                column: "AccomodationId",
                principalTable: "Accomodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Accomodations_AccomodationId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ContactInformation",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Accomodations_AccomodationId",
                table: "Images",
                column: "AccomodationId",
                principalTable: "Accomodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
