using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_column_DPwarehouseOutput_in_dyeingPrinitngOutput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DPWarehouseOutputId",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DPWarehouseOutputItemId",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DPWarehouseOutputId",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "DPWarehouseOutputItemId",
                table: "DyeingPrintingAreaOutputProductionOrders");
        }
    }
}
