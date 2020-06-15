using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class updateColumnGShippingInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Say",
                table: "GarmentShippingInvoices");

            migrationBuilder.AlterColumn<string>(
                name: "CPrice",
                table: "GarmentShippingInvoices",
                nullable: true,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CPrice",
                table: "GarmentShippingInvoices",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Say",
                table: "GarmentShippingInvoices",
                nullable: true);
        }
    }
}
