using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddingPackingAreaOutputRequirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PackagingQty",
                table: "DyeingPrintingAreaOutputProductionOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PackagingType",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackagingUnit",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackagingQty",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "PackagingType",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "PackagingUnit",
                table: "DyeingPrintingAreaOutputProductionOrders");
        }
    }
}
