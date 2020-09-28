using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentPackingListDetail_Length_Width_Height : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CartonsQuantity",
                table: "GarmentPackingListDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "GarmentPackingListDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "GarmentPackingListDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "GarmentPackingListDetails",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartonsQuantity",
                table: "GarmentPackingListDetails");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "GarmentPackingListDetails");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "GarmentPackingListDetails");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "GarmentPackingListDetails");
        }
    }
}
