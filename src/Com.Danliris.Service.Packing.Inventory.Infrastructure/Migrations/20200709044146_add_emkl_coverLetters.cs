using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_emkl_coverLetters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EMKLCode",
                table: "GarmentShippingCoverLetters",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EMKLId",
                table: "GarmentShippingCoverLetters",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EMKLCode",
                table: "GarmentShippingCoverLetters");

            migrationBuilder.DropColumn(
                name: "EMKLId",
                table: "GarmentShippingCoverLetters");
        }
    }
}
