using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_column_DyeingPrintingSummary_remains_end : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BalanceEnd",
                table: "DyeingPrintingStockOpnameSummaries",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BalanceRemains",
                table: "DyeingPrintingStockOpnameSummaries",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "PackagingQtyEnd",
                table: "DyeingPrintingStockOpnameSummaries",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PackagingQtyRemains",
                table: "DyeingPrintingStockOpnameSummaries",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalanceEnd",
                table: "DyeingPrintingStockOpnameSummaries");

            migrationBuilder.DropColumn(
                name: "BalanceRemains",
                table: "DyeingPrintingStockOpnameSummaries");

            migrationBuilder.DropColumn(
                name: "PackagingQtyEnd",
                table: "DyeingPrintingStockOpnameSummaries");

            migrationBuilder.DropColumn(
                name: "PackagingQtyRemains",
                table: "DyeingPrintingStockOpnameSummaries");
        }
    }
}
