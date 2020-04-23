using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddPropertiesOnDyeingPrintingAreaInputProductionOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvalType",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "QuantityKg",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvalType",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "QuantityKg",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
