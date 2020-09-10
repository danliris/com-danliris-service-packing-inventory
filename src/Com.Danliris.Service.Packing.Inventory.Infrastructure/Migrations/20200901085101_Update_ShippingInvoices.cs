using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Update_ShippingInvoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliverTo",
                table: "GarmentShippingInvoices",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Desc2",
                table: "GarmentShippingInvoiceItems",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Desc3",
                table: "GarmentShippingInvoiceItems",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Desc4",
                table: "GarmentShippingInvoiceItems",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliverTo",
                table: "GarmentShippingInvoices");

            migrationBuilder.DropColumn(
                name: "Desc2",
                table: "GarmentShippingInvoiceItems");

            migrationBuilder.DropColumn(
                name: "Desc3",
                table: "GarmentShippingInvoiceItems");

            migrationBuilder.DropColumn(
                name: "Desc4",
                table: "GarmentShippingInvoiceItems");
        }
    }
}
