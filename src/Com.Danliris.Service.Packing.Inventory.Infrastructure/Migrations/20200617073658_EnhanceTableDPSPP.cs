using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class EnhanceTableDPSPP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaterialConstructionId",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialConstructionName",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialWidth",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialConstructionId",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialConstructionName",
                table: "DyeingPrintingAreaInputProductionOrders",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "DyeingPrintingAreaInputProductionOrders",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialWidth",
                table: "DyeingPrintingAreaInputProductionOrders",
                maxLength: 1024,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialConstructionId",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionName",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "MaterialWidth",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionId",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionName",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "MaterialWidth",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
