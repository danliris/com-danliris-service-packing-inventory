using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_column_finish_width_opnametable_and_fabricpacking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FinishWidth",
                table: "FabricProductSKUs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinishWidth",
                table: "DyeingPrintingStockOpnameSummaries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinishWidth",
                table: "DyeingPrintingStockOpnameProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinishWidth",
                table: "DyeingPrintingStockOpnameMutationItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishWidth",
                table: "FabricProductSKUs");

            migrationBuilder.DropColumn(
                name: "FinishWidth",
                table: "DyeingPrintingStockOpnameSummaries");

            migrationBuilder.DropColumn(
                name: "FinishWidth",
                table: "DyeingPrintingStockOpnameProductionOrders");

            migrationBuilder.DropColumn(
                name: "FinishWidth",
                table: "DyeingPrintingStockOpnameMutationItems");
        }
    }
}
