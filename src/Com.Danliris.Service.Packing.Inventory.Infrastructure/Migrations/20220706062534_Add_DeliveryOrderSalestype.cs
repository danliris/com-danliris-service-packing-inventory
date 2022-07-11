using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_DeliveryOrderSalestype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryOrderSalesType",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryOrderSalesType",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryOrderSalesType",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "DeliveryOrderSalesType",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
