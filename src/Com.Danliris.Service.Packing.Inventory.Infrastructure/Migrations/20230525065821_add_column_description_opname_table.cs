using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_column_description_opname_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FabricProductPackings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DyeingPrintingStockOpnameSummaries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DyeingPrintingStockOpnameProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DyeingPrintingStockOpnameMutationItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "FabricProductPackings");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DyeingPrintingStockOpnameSummaries");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DyeingPrintingStockOpnameProductionOrders");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DyeingPrintingStockOpnameMutationItems");
        }
    }
}
