using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class EnhanceDPAreaWithSKUPackingIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FabricPackingId",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FabricSKUId",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FabricPackingId",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "FabricSKUId",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
