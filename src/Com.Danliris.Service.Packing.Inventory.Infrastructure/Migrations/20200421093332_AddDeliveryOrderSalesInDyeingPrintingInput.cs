using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddDeliveryOrderSalesInDyeingPrintingInput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DeliveryOrderSalesId",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryOrderSalesNo",
                table: "DyeingPrintingAreaInputProductionOrders",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryOrderSalesId",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "DeliveryOrderSalesNo",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
