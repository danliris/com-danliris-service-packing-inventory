using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddField_InventoryType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InventoryType",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryType",
                table: "DyeingPrintingAreaMovements",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryType",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryType",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "InventoryType",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "InventoryType",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
