using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class ModifyTableGarmentShippingCoverLetter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "GarmentShippingCoverLetters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PIC",
                table: "GarmentShippingCoverLetters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "GarmentShippingCoverLetters");

            migrationBuilder.DropColumn(
                name: "PIC",
                table: "GarmentShippingCoverLetters");
        }
    }
}
