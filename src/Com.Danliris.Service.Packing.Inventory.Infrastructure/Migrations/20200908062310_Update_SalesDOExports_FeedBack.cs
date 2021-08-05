using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Update_SalesDOExports_FeedBack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliverTo",
                table: "GarmentShippingExportSalesDOs",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "GarmentShippingExportSalesDOs",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipmentMode",
                table: "GarmentShippingExportSalesDOs",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliverTo",
                table: "GarmentShippingExportSalesDOs");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "GarmentShippingExportSalesDOs");

            migrationBuilder.DropColumn(
                name: "ShipmentMode",
                table: "GarmentShippingExportSalesDOs");
        }
    }
}
