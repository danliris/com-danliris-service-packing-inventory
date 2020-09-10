using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentShippingLocalSalesDO_Remark_DLL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Volume",
                table: "GarmentShippingLocalSalesDOItems");

            migrationBuilder.RenameColumn(
                name: "CartonQuantity",
                table: "GarmentShippingLocalSalesDOItems",
                newName: "PackQuantity");

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "GarmentShippingLocalSalesDOs",
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackUomId",
                table: "GarmentShippingLocalSalesDOItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PackUomUnit",
                table: "GarmentShippingLocalSalesDOItems",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "GarmentShippingLocalSalesDOs");

            migrationBuilder.DropColumn(
                name: "PackUomId",
                table: "GarmentShippingLocalSalesDOItems");

            migrationBuilder.DropColumn(
                name: "PackUomUnit",
                table: "GarmentShippingLocalSalesDOItems");

            migrationBuilder.RenameColumn(
                name: "PackQuantity",
                table: "GarmentShippingLocalSalesDOItems",
                newName: "CartonQuantity");

            migrationBuilder.AddColumn<double>(
                name: "Volume",
                table: "GarmentShippingLocalSalesDOItems",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
