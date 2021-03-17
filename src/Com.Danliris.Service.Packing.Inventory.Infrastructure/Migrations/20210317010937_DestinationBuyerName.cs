using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class DestinationBuyerName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DestinationBuyerName",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationBuyerName",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationBuyerName",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "DestinationBuyerName",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
