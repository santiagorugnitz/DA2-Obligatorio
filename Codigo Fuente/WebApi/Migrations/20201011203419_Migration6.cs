using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class Migration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Accomodations_AccomodationId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Accomodations_AccomodationId",
                table: "Reservations",
                column: "AccomodationId",
                principalTable: "Accomodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Accomodations_AccomodationId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Accomodations_AccomodationId",
                table: "Reservations",
                column: "AccomodationId",
                principalTable: "Accomodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
