using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class PL_Add_NetNetWeight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AVG_GW",
                table: "GarmentPackingListItems");

            migrationBuilder.DropColumn(
                name: "AVG_NW",
                table: "GarmentPackingListItems");

            migrationBuilder.DropColumn(
                name: "CartonsQuantity",
                table: "GarmentPackingListDetails");

            migrationBuilder.AddColumn<double>(
                name: "NetNetWeight",
                table: "GarmentPackingLists",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetNetWeight",
                table: "GarmentPackingLists");

            migrationBuilder.AddColumn<double>(
                name: "AVG_GW",
                table: "GarmentPackingListItems",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AVG_NW",
                table: "GarmentPackingListItems",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CartonsQuantity",
                table: "GarmentPackingListDetails",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
