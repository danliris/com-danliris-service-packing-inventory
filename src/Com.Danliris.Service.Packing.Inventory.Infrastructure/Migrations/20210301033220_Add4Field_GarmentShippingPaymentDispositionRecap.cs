using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add4Field_GarmentShippingPaymentDispositionRecap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AmountService",
                table: "GarmentShippingPaymentDispositionRecapItems",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OthersPayment",
                table: "GarmentShippingPaymentDispositionRecapItems",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TruckingPayment",
                table: "GarmentShippingPaymentDispositionRecapItems",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VatService",
                table: "GarmentShippingPaymentDispositionRecapItems",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountService",
                table: "GarmentShippingPaymentDispositionRecapItems");

            migrationBuilder.DropColumn(
                name: "OthersPayment",
                table: "GarmentShippingPaymentDispositionRecapItems");

            migrationBuilder.DropColumn(
                name: "TruckingPayment",
                table: "GarmentShippingPaymentDispositionRecapItems");

            migrationBuilder.DropColumn(
                name: "VatService",
                table: "GarmentShippingPaymentDispositionRecapItems");
        }
    }
}
