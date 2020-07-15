using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class EnhanceMovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "DyeingPrintingAreaMovements",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderType",
                table: "DyeingPrintingAreaMovements",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "DyeingPrintingAreaMovements",
                maxLength: 4096,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "ProductionOrderType",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "DyeingPrintingAreaMovements");
        }
    }
}
