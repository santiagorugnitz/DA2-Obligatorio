using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TouristSpots");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "TouristSpots",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    AccomodationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Accomodations_AccomodationId",
                        column: x => x.AccomodationId,
                        principalTable: "Accomodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TouristSpots_ImageId",
                table: "TouristSpots",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AccomodationId",
                table: "Images",
                column: "AccomodationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TouristSpots_Images_ImageId",
                table: "TouristSpots",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TouristSpots_Images_ImageId",
                table: "TouristSpots");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_TouristSpots_ImageId",
                table: "TouristSpots");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "TouristSpots");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TouristSpots",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
