using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddingPackagingQtyUnitAndTypeForWarehouses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PackagingQty",
                table: "DyeingPrintingAreaInputProductionOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PackagingType",
                table: "DyeingPrintingAreaInputProductionOrders",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackagingUnit",
                table: "DyeingPrintingAreaInputProductionOrders",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackagingQty",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "PackagingType",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "PackagingUnit",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
