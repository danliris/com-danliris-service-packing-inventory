using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class EnhanceDPAreaInput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessTypeId",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProcessTypeName",
                table: "DyeingPrintingAreaInputProductionOrders",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YarnMaterialId",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "YarnMaterialName",
                table: "DyeingPrintingAreaInputProductionOrders",
                maxLength: 1024,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessTypeId",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProcessTypeName",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "YarnMaterialId",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "YarnMaterialName",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
