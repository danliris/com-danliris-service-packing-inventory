using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_column_Product_textile_DyeingPrintingMovement_all : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductTextileCode",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTextileId",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductTextileName",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductTextileCode",
                table: "DyeingPrintingAreaMovements",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTextileId",
                table: "DyeingPrintingAreaMovements",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductTextileName",
                table: "DyeingPrintingAreaMovements",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductTextileCode",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTextileId",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductTextileName",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductTextileCode",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductTextileId",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductTextileName",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductTextileCode",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "ProductTextileId",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "ProductTextileName",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "ProductTextileCode",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductTextileId",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ProductTextileName",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
