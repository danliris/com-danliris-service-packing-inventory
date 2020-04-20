using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class _20200416170123_AddingPackagingQTYandUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UOMUnit",
                table: "DyeingPrintingAreaMovementHistories");

            migrationBuilder.AddColumn<decimal>(
                name: "PackagingQty",
                table: "DyeingPrintingAreaMovements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PackagingUnit",
                table: "DyeingPrintingAreaMovements",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PackagingQty",
                table: "DyeingPrintingAreaMovementHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PackagingUnit",
                table: "DyeingPrintingAreaMovementHistories",
                maxLength: 32,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackagingQty",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "PackagingUnit",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "PackagingQty",
                table: "DyeingPrintingAreaMovementHistories");

            migrationBuilder.DropColumn(
                name: "PackagingUnit",
                table: "DyeingPrintingAreaMovementHistories");

            migrationBuilder.AddColumn<string>(
                name: "UOMUnit",
                table: "DyeingPrintingAreaMovementHistories",
                nullable: true);
        }
    }
}
