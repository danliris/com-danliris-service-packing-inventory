using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class IndexInputOutput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaInputProductionOrders_ProductionOrderId",
                table: "DyeingPrintingAreaInputProductionOrders",
                column: "ProductionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaInputProductionOrders_ProductionOrderNo",
                table: "DyeingPrintingAreaInputProductionOrders",
                column: "ProductionOrderNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DyeingPrintingAreaInputProductionOrders_ProductionOrderId",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropIndex(
                name: "IX_DyeingPrintingAreaInputProductionOrders_ProductionOrderNo",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
