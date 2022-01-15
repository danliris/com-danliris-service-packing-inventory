using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class IndexingInputOutput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaOutputProductionOrders_ProcessTypeName",
                table: "DyeingPrintingAreaOutputProductionOrders",
                column: "ProcessTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaOutputProductionOrders_ProductionOrderId",
                table: "DyeingPrintingAreaOutputProductionOrders",
                column: "ProductionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaOutputProductionOrders_ProductionOrderNo",
                table: "DyeingPrintingAreaOutputProductionOrders",
                column: "ProductionOrderNo");

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaInputProductionOrders_ProcessTypeName",
                table: "DyeingPrintingAreaInputProductionOrders",
                column: "ProcessTypeName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DyeingPrintingAreaOutputProductionOrders_ProcessTypeName",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropIndex(
                name: "IX_DyeingPrintingAreaOutputProductionOrders_ProductionOrderId",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropIndex(
                name: "IX_DyeingPrintingAreaOutputProductionOrders_ProductionOrderNo",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropIndex(
                name: "IX_DyeingPrintingAreaInputProductionOrders_ProcessTypeName",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
