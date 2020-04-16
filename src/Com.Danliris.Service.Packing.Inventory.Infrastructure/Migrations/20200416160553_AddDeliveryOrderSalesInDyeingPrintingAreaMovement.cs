using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddDeliveryOrderSalesInDyeingPrintingAreaMovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UOMUnit",
                table: "DyeingPrintingAreaMovementHistories");

            migrationBuilder.AddColumn<long>(
                name: "DeliveryOrderSalesId",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryOrderSalesNo",
                table: "DyeingPrintingAreaMovements",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryOrderSalesId",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "DeliveryOrderSalesNo",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.AddColumn<string>(
                name: "UOMUnit",
                table: "DyeingPrintingAreaMovementHistories",
                nullable: true);
        }
    }
}
