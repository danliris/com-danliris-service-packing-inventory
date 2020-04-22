using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class _20200414202000_EnhanceDyeingHistoryBalanceAndGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "DyeingPrintingAreaMovementHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "DyeingPrintingAreaMovementHistories",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "DyeingPrintingAreaMovementHistories");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "DyeingPrintingAreaMovementHistories");
        }
    }
}
