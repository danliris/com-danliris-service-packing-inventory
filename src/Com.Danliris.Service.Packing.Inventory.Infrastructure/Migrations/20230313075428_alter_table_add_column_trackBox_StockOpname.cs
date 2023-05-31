using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class alter_table_add_column_trackBox_StockOpname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrackBox",
                table: "DyeingPrintingStockOpnameProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackBox",
                table: "DyeingPrintingStockOpnameMutationItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackBox",
                table: "DyeingPrintingStockOpnameProductionOrders");

            migrationBuilder.DropColumn(
                name: "TrackBox",
                table: "DyeingPrintingStockOpnameMutationItems");
        }
    }
}
