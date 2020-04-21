using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class FixUnitInDYeingPrintingArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "DyeingPrintingAreaSummaries");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.RenameColumn(
                name: "UnitName",
                table: "DyeingPrintingAreaSummaries",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "UnitName",
                table: "DyeingPrintingAreaOutputProductionOrders",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "UnitName",
                table: "DyeingPrintingAreaMovements",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "UnitName",
                table: "DyeingPrintingAreaInputProductionOrders",
                newName: "Unit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "DyeingPrintingAreaSummaries",
                newName: "UnitName");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "DyeingPrintingAreaOutputProductionOrders",
                newName: "UnitName");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "DyeingPrintingAreaMovements",
                newName: "UnitName");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "DyeingPrintingAreaInputProductionOrders",
                newName: "UnitName");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "DyeingPrintingAreaSummaries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0);
        }
    }
}
