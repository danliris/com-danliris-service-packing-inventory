using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddField_RoType_GarmentPackingListItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoType",
                table: "GarmentPackingLists");

            migrationBuilder.AddColumn<string>(
                name: "RoType",
                table: "GarmentPackingListItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoType",
                table: "GarmentPackingListItems");

            migrationBuilder.AddColumn<string>(
                name: "RoType",
                table: "GarmentPackingLists",
                nullable: true);
        }
    }
}
