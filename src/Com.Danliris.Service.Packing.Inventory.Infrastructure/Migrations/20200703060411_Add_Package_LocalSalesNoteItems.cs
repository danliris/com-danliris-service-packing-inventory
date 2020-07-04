using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_Package_LocalSalesNoteItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PackageQuantity",
                table: "GarmentShippingLocalSalesNoteItems",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PackageUomId",
                table: "GarmentShippingLocalSalesNoteItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PackageUomUnit",
                table: "GarmentShippingLocalSalesNoteItems",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageQuantity",
                table: "GarmentShippingLocalSalesNoteItems");

            migrationBuilder.DropColumn(
                name: "PackageUomId",
                table: "GarmentShippingLocalSalesNoteItems");

            migrationBuilder.DropColumn(
                name: "PackageUomUnit",
                table: "GarmentShippingLocalSalesNoteItems");
        }
    }
}
