using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_column_SplitQuantity_DyeingPrintingStockOpnameSummaryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SplitQuantity",
                table: "DyeingPrintingStockOpnameSummaries",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SplitQuantity",
                table: "DyeingPrintingStockOpnameSummaries");
        }
    }
}
