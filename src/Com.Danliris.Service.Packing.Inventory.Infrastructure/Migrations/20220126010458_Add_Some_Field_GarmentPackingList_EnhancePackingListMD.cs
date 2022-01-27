using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_Some_Field_GarmentPackingList_EnhancePackingListMD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSampleDelivered",
                table: "GarmentPackingLists",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSampleExpenditureGood",
                table: "GarmentPackingLists",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RoType",
                table: "GarmentPackingLists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSampleDelivered",
                table: "GarmentPackingLists");

            migrationBuilder.DropColumn(
                name: "IsSampleExpenditureGood",
                table: "GarmentPackingLists");

            migrationBuilder.DropColumn(
                name: "RoType",
                table: "GarmentPackingLists");
        }
    }
}
