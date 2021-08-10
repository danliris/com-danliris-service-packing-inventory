using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class StockOpnameProductPacking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FabricPackingId",
                table: "DyeingPrintingStockOpnameProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FabricSKUId",
                table: "DyeingPrintingStockOpnameProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasPrintingProductPacking",
                table: "DyeingPrintingStockOpnameProductionOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasPrintingProductSKU",
                table: "DyeingPrintingStockOpnameProductionOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductPackingCode",
                table: "DyeingPrintingStockOpnameProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductPackingId",
                table: "DyeingPrintingStockOpnameProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductSKUCode",
                table: "DyeingPrintingStockOpnameProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductSKUId",
                table: "DyeingPrintingStockOpnameProductionOrders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FabricPackingId",
                table: "DyeingPrintingStockOpnameProductionOrders");

            migrationBuilder.DropColumn(
                name: "FabricSKUId",
                table: "DyeingPrintingStockOpnameProductionOrders");

            migrationBuilder.DropColumn(
                name: "HasPrintingProductPacking",
                table: "DyeingPrintingStockOpnameProductionOrders");

            migrationBuilder.DropColumn(
                name: "HasPrintingProductSKU",
                table: "DyeingPrintingStockOpnameProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductPackingCode",
                table: "DyeingPrintingStockOpnameProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductPackingId",
                table: "DyeingPrintingStockOpnameProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductSKUCode",
                table: "DyeingPrintingStockOpnameProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductSKUId",
                table: "DyeingPrintingStockOpnameProductionOrders");
        }
    }
}
