using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentShippingInvoiceItem_GarmentShippingInvoiceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GarmentShippingInvoiceId",
                table: "GarmentShippingInvoiceItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GarmentShippingInvoiceId",
                table: "GarmentShippingInvoiceItems",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
