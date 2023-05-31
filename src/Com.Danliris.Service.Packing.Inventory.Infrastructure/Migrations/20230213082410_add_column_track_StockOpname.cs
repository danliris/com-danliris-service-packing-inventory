using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_column_track_StockOpname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackId",
                table: "DyeingPrintingStockOpnameProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TrackName",
                table: "DyeingPrintingStockOpnameProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackType",
                table: "DyeingPrintingStockOpnameProductionOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "DyeingPrintingStockOpnameProductionOrders");

            migrationBuilder.DropColumn(
                name: "TrackName",
                table: "DyeingPrintingStockOpnameProductionOrders");

            migrationBuilder.DropColumn(
                name: "TrackType",
                table: "DyeingPrintingStockOpnameProductionOrders");
        }
    }
}
