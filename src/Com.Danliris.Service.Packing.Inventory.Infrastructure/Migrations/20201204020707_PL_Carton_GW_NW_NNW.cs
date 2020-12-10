using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class PL_Carton_GW_NW_NNW : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "GrossWeight",
                table: "GarmentPackingListDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NetNetWeight",
                table: "GarmentPackingListDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NetWeight",
                table: "GarmentPackingListDetails",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossWeight",
                table: "GarmentPackingListDetails");

            migrationBuilder.DropColumn(
                name: "NetNetWeight",
                table: "GarmentPackingListDetails");

            migrationBuilder.DropColumn(
                name: "NetWeight",
                table: "GarmentPackingListDetails");
        }
    }
}
