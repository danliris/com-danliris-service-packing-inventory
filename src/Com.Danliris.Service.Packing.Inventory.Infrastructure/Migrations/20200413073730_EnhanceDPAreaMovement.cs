using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class EnhanceDPAreaMovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "DyeingPrintingAreaMovements",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shift",
                table: "DyeingPrintingAreaMovementHistories",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "Shift",
                table: "DyeingPrintingAreaMovementHistories");
        }
    }
}
