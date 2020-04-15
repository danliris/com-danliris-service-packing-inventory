using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddPropertiesOnDyeingPrintingAreaMovementHistoryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ProductionOrderQuantity",
                table: "DyeingPrintingAreaMovementHistories",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "QtyKg",
                table: "DyeingPrintingAreaMovementHistories",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "UOMUnit",
                table: "DyeingPrintingAreaMovementHistories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionOrderQuantity",
                table: "DyeingPrintingAreaMovementHistories");

            migrationBuilder.DropColumn(
                name: "QtyKg",
                table: "DyeingPrintingAreaMovementHistories");

            migrationBuilder.DropColumn(
                name: "UOMUnit",
                table: "DyeingPrintingAreaMovementHistories");
        }
    }
}
