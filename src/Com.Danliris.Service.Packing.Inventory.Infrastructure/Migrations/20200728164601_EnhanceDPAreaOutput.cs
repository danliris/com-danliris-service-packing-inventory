using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class EnhanceDPAreaOutput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessTypeId",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProcessTypeName",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YarnMaterialId",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "YarnMaterialName",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 1024,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessTypeId",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProcessTypeName",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "YarnMaterialId",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "YarnMaterialName",
                table: "DyeingPrintingAreaOutputProductionOrders");
        }
    }
}
