using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentShippingCreditAdvice_PolicyValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AccountsReceivablePolicyValue",
                table: "GarmentShippingCreditAdvices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CargoPolicyValue",
                table: "GarmentShippingCreditAdvices",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountsReceivablePolicyValue",
                table: "GarmentShippingCreditAdvices");

            migrationBuilder.DropColumn(
                name: "CargoPolicyValue",
                table: "GarmentShippingCreditAdvices");
        }
    }
}
