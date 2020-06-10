using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class EnhanceDyeingPrintingInputForAval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AvalQuantity",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AvalWeightQuantity",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "AvalType",
                table: "DyeingPrintingAreaInputs",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTransformedAval",
                table: "DyeingPrintingAreaInputs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "TotalAvalQuantity",
                table: "DyeingPrintingAreaInputs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalAvalWeight",
                table: "DyeingPrintingAreaInputs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AvalQuantity",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "InputAvalBonNo",
                table: "DyeingPrintingAreaInputProductionOrders",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvalQuantity",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "AvalWeightQuantity",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "AvalType",
                table: "DyeingPrintingAreaInputs");

            migrationBuilder.DropColumn(
                name: "IsTransformedAval",
                table: "DyeingPrintingAreaInputs");

            migrationBuilder.DropColumn(
                name: "TotalAvalQuantity",
                table: "DyeingPrintingAreaInputs");

            migrationBuilder.DropColumn(
                name: "TotalAvalWeight",
                table: "DyeingPrintingAreaInputs");

            migrationBuilder.DropColumn(
                name: "AvalQuantity",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "InputAvalBonNo",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
