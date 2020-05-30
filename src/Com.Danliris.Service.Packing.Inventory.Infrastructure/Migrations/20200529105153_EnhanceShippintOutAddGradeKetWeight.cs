using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class EnhanceShippintOutAddGradeKetWeight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShippingGrade",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingRemark",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingGrade",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "ShippingRemark",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "DyeingPrintingAreaOutputProductionOrders");
        }
    }
}
