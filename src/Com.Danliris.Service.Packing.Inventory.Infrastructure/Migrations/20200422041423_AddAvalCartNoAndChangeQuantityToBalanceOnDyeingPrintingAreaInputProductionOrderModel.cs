using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddAvalCartNoAndChangeQuantityToBalanceOnDyeingPrintingAreaInputProductionOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.AddColumn<string>(
                name: "AvalCartNo",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvalCartNo",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
