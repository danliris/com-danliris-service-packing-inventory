using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentPackingList_ShippingStaff_ImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RemarkImagePath",
                table: "GarmentPackingLists",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingMarkImagePath",
                table: "GarmentPackingLists",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShippingStaffId",
                table: "GarmentPackingLists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShippingStaffName",
                table: "GarmentPackingLists",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SideMarkImagePath",
                table: "GarmentPackingLists",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemarkImagePath",
                table: "GarmentPackingLists");

            migrationBuilder.DropColumn(
                name: "ShippingMarkImagePath",
                table: "GarmentPackingLists");

            migrationBuilder.DropColumn(
                name: "ShippingStaffId",
                table: "GarmentPackingLists");

            migrationBuilder.DropColumn(
                name: "ShippingStaffName",
                table: "GarmentPackingLists");

            migrationBuilder.DropColumn(
                name: "SideMarkImagePath",
                table: "GarmentPackingLists");
        }
    }
}
