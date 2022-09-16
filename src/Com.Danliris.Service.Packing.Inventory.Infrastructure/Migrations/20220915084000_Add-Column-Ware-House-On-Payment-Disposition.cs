using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddColumnWareHouseOnPaymentDisposition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WareHouseCode",
                table: "GarmentShippingPaymentDispositions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WareHouseId",
                table: "GarmentShippingPaymentDispositions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WareHouseName",
                table: "GarmentShippingPaymentDispositions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WareHouseCode",
                table: "GarmentShippingPaymentDispositions");

            migrationBuilder.DropColumn(
                name: "WareHouseId",
                table: "GarmentShippingPaymentDispositions");

            migrationBuilder.DropColumn(
                name: "WareHouseName",
                table: "GarmentShippingPaymentDispositions");
        }
    }
}
