using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class FixNewFabricQC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DyeingPrintingAreaMovementId",
                table: "NewFabricQualityControls",
                newName: "DyeingPrintingAreaInputProductionOrderId");

            migrationBuilder.RenameColumn(
                name: "DyeingPrintingAreaMovementBonNo",
                table: "NewFabricQualityControls",
                newName: "DyeingPrintingAreaInputBonNo");

            migrationBuilder.AddColumn<int>(
                name: "DyeingPrintingAreaInputId",
                table: "NewFabricQualityControls",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DyeingPrintingAreaInputId",
                table: "NewFabricQualityControls");

            migrationBuilder.RenameColumn(
                name: "DyeingPrintingAreaInputProductionOrderId",
                table: "NewFabricQualityControls",
                newName: "DyeingPrintingAreaMovementId");

            migrationBuilder.RenameColumn(
                name: "DyeingPrintingAreaInputBonNo",
                table: "NewFabricQualityControls",
                newName: "DyeingPrintingAreaMovementBonNo");
        }
    }
}
