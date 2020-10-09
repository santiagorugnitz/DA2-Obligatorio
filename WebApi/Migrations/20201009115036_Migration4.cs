using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactInformation",
                table: "Reservations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateDescription",
                table: "Reservations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Reservations",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Reservations",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactInformation",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "StateDescription",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Reservations");
        }
    }
}
