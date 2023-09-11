using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_Column_DHL_Charges_on_Table_Invoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DHLCharges",
                table: "GarmentShippingInvoices",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LessFabricCost",
                table: "GarmentShippingInvoices",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DHLCharges",
                table: "GarmentShippingInvoices");

            migrationBuilder.DropColumn(
                name: "LessFabricCost",
                table: "GarmentShippingInvoices");
        }
    }
}
