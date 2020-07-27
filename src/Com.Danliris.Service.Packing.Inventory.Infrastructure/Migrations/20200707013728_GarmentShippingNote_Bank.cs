using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentShippingNote_Bank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankCurrencyCode",
                table: "GarmentShippingNotes",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "GarmentShippingNotes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "GarmentShippingNotes",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankCurrencyCode",
                table: "GarmentShippingNotes");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "GarmentShippingNotes");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "GarmentShippingNotes");
        }
    }
}
