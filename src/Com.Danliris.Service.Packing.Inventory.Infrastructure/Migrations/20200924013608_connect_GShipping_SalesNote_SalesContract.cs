using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class connect_GShipping_SalesNote_SalesContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocalSalesContractId",
                table: "GarmentShippingLocalSalesNotes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "GarmentShippingLocalSalesNotes",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesContractNo",
                table: "GarmentShippingLocalSalesNotes",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocalSalesContractItemId",
                table: "GarmentShippingLocalSalesNoteItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalSalesContractId",
                table: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropColumn(
                name: "SalesContractNo",
                table: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropColumn(
                name: "LocalSalesContractItemId",
                table: "GarmentShippingLocalSalesNoteItems");
        }
    }
}
