using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddColumnProductPackingCodeRemainsOnDyeingPrintingAreaOutputProductionOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductPackingCodeRemains",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPackingCodeRemains",
                table: "DyeingPrintingAreaOutputProductionOrders");
        }
    }
}
