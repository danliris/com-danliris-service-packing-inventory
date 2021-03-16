using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class DestinationBuyerNameInputOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DestinationBuyerNameInputOrder",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationBuyerNameInputOrder",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
