using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddSPPInputIDOnOutputSPPANDBalanceRemainOnInputSPP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DyeingPrintingAreaInputProductionOrderId",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 128,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "BalanceRemains",
                table: "DyeingPrintingAreaInputProductionOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DyeingPrintingAreaInputProductionOrderId",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "BalanceRemains",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
