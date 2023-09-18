using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_column_bankCharge_Nettnego_GarmentShippingNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BankCharge",
                table: "GarmentShippingNotes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NettNego",
                table: "GarmentShippingNotes",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankCharge",
                table: "GarmentShippingNotes");

            migrationBuilder.DropColumn(
                name: "NettNego",
                table: "GarmentShippingNotes");
        }
    }
}
