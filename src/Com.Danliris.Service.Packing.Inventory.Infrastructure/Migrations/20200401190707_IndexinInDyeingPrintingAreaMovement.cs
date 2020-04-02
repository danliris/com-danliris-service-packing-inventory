using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class IndexinInDyeingPrintingAreaMovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaMovements_BonNo",
                table: "DyeingPrintingAreaMovements",
                column: "BonNo",
                unique: true,
                filter: "[IsDeleted]=(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DyeingPrintingAreaMovements_BonNo",
                table: "DyeingPrintingAreaMovements");
        }
    }
}
