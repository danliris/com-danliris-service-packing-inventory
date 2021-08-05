using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class EnhanceOutputAval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DeliveryOrderAvalId",
                table: "DyeingPrintingAreaOutputs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryOrderAvalNo",
                table: "DyeingPrintingAreaOutputs",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryOrderAvalId",
                table: "DyeingPrintingAreaOutputs");

            migrationBuilder.DropColumn(
                name: "DeliveryOrderAvalNo",
                table: "DyeingPrintingAreaOutputs");
        }
    }
}
