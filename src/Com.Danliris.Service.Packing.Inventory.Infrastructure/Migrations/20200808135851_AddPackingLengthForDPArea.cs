using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddPackingLengthForDPArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PackagingLength",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PackagingLength",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackagingLength",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "PackagingLength",
                table: "DyeingPrintingAreaInputProductionOrders");
        }
    }
}
