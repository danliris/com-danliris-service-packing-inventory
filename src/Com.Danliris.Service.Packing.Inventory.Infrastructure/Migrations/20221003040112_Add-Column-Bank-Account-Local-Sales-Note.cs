using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddColumnBankAccountLocalSalesNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "GarmentShippingLocalSalesNotes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "GarmentShippingLocalSalesNotes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "GarmentShippingLocalSalesNotes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "GarmentShippingLocalSalesNotes");
        }
    }
}
