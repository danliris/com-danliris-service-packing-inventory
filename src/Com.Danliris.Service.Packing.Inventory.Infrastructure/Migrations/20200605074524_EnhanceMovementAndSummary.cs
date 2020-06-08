using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class EnhanceMovementAndSummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DyeingPrintingAreaProductionOrderDocumentId",
                table: "DyeingPrintingAreaSummaries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DyeingPrintingAreaProductionOrderDocumentId",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DyeingPrintingAreaProductionOrderDocumentId",
                table: "DyeingPrintingAreaSummaries");

            migrationBuilder.DropColumn(
                name: "DyeingPrintingAreaProductionOrderDocumentId",
                table: "DyeingPrintingAreaMovements");
        }
    }
}
