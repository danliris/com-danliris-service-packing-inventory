using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddColumnMarketingPackingListAndInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MarketingName",
                table: "GarmentShippingInvoiceItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketingName",
                table: "GarmentPackingListItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketingName",
                table: "GarmentDraftPackingListItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketingName",
                table: "GarmentShippingInvoiceItems");

            migrationBuilder.DropColumn(
                name: "MarketingName",
                table: "GarmentPackingListItems");

            migrationBuilder.DropColumn(
                name: "MarketingName",
                table: "GarmentDraftPackingListItems");
        }
    }
}
