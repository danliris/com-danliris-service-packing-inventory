using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Column_AreaOrigin_DPPreInputWarehouseId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DPPreInputWarehouseId",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AreaOrigin",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DPPreInputWarehouseId",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DPPreInputWarehouseId",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "AreaOrigin",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "DPPreInputWarehouseId",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
