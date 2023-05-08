using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class addColoumBuyerGShippingPaymentDispotitionInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerAgentCode",
                table: "GarmentShippingPaymentDispositionInvoiceDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuyerAgentId",
                table: "GarmentShippingPaymentDispositionInvoiceDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerAgentName",
                table: "GarmentShippingPaymentDispositionInvoiceDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerAgentCode",
                table: "GarmentShippingPaymentDispositionInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "BuyerAgentId",
                table: "GarmentShippingPaymentDispositionInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "BuyerAgentName",
                table: "GarmentShippingPaymentDispositionInvoiceDetails");
        }
    }
}
