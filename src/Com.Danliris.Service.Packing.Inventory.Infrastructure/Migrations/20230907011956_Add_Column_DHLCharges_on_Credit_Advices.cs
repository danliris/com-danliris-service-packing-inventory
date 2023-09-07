using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_Column_DHLCharges_on_Credit_Advices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DHLCharges",
                table: "GarmentShippingCreditAdvices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LessFabricCost",
                table: "GarmentShippingCreditAdvices",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DHLCharges",
                table: "GarmentShippingCreditAdvices");

            migrationBuilder.DropColumn(
                name: "LessFabricCost",
                table: "GarmentShippingCreditAdvices");
        }
    }
}
