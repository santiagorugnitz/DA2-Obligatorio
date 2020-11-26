using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class Migration11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Accomodations_AccomodationId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Accomodations_AccomodationId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Accomodations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_AccomodationId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Images_AccomodationId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AccomodationId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "AccomodationId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "accommodationId",
                table: "Reservations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "accommodationId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "accommodations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Stars = table.Column<double>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Fee = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Available = table.Column<bool>(nullable: false),
                    Telephone = table.Column<string>(nullable: true),
                    ContactInformation = table.Column<string>(nullable: true),
                    TouristSpotId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accommodations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_accommodations_TouristSpots_TouristSpotId",
                        column: x => x.TouristSpotId,
                        principalTable: "TouristSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_accommodationId",
                table: "Reservations",
                column: "accommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_accommodationId",
                table: "Images",
                column: "accommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_accommodations_TouristSpotId",
                table: "accommodations",
                column: "TouristSpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_accommodations_accommodationId",
                table: "Images",
                column: "accommodationId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_accommodations_accommodationId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_accommodations_accommodationId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "accommodations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_accommodationId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Images_accommodationId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "accommodationId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "accommodationId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "AccomodationId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccomodationId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Accomodations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    ContactInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fee = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stars = table.Column<double>(type: "float", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TouristSpotId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accomodations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accomodations_TouristSpots_TouristSpotId",
                        column: x => x.TouristSpotId,
                        principalTable: "TouristSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_AccomodationId",
                table: "Reservations",
                column: "AccomodationId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AccomodationId",
                table: "Images",
                column: "AccomodationId");

            migrationBuilder.CreateIndex(
                name: "IX_Accomodations_TouristSpotId",
                table: "Accomodations",
                column: "TouristSpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Accomodations_AccomodationId",
                table: "Images",
                column: "AccomodationId",
                principalTable: "Accomodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Accomodations_AccomodationId",
                table: "Reservations",
                column: "AccomodationId",
                principalTable: "Accomodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
