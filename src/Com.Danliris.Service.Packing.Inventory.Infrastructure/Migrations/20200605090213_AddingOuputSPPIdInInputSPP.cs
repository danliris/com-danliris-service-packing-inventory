using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddingOuputSPPIdInInputSPP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DyeingPrintingAreaOutputProductionOrderId",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DyeingPrintingAreaOutputProductionOrderId",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
