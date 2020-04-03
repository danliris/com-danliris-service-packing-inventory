using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class EnhanceModelDyeingPrintingAreaMovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "DyeingPrintingAreaMovements",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceArea",
                table: "DyeingPrintingAreaMovements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "SourceArea",
                table: "DyeingPrintingAreaMovements");
        }
    }
}
