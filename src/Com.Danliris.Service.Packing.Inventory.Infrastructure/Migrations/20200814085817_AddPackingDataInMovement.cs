using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddPackingDataInMovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PackagingLength",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "PackagingQty",
                table: "DyeingPrintingAreaMovements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PackagingUnit",
                table: "DyeingPrintingAreaMovements",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackagingLength",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "PackagingQty",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "PackagingUnit",
                table: "DyeingPrintingAreaMovements");
        }
    }
}
