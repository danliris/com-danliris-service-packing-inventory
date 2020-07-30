using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class EnhanceDPAreaAddSKUPacking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPrintingProductPacking",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasPrintingProductSKU",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductPackingCode",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductPackingId",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductSKUCode",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductSKUId",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasPrintingProductPacking",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasPrintingProductSKU",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductPackingCode",
                table: "DyeingPrintingAreaInputProductionOrders",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductPackingId",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductSKUCode",
                table: "DyeingPrintingAreaInputProductionOrders",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductSKUId",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPrintingProductPacking",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "HasPrintingProductSKU",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductPackingCode",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductPackingId",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductSKUCode",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductSKUId",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "HasPrintingProductPacking",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "HasPrintingProductSKU",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductPackingCode",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductPackingId",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductSKUCode",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductSKUId",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
