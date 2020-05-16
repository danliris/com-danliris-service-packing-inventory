using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class EnhanceDPOutputAddDO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DeliveryOrderSalesId",
                table: "DyeingPrintingAreaOutputs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryOrderSalesNo",
                table: "DyeingPrintingAreaOutputs",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryOrderSalesId",
                table: "DyeingPrintingAreaOutputs");

            migrationBuilder.DropColumn(
                name: "DeliveryOrderSalesNo",
                table: "DyeingPrintingAreaOutputs");
        }
    }
}
