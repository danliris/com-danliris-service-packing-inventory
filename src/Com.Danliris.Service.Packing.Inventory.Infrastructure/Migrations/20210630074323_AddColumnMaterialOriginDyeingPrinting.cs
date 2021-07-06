using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddColumnMaterialOriginDyeingPrinting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaterialOrigin",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialOrigin",
                table: "DyeingPrintingAreaMovements",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialOrigin",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialOrigin",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "MaterialOrigin",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "MaterialOrigin",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
