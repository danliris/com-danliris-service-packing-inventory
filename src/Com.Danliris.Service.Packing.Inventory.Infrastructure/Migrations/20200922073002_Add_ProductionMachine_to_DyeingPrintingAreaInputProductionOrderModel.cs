using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_ProductionMachine_to_DyeingPrintingAreaInputProductionOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductionMachine",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductionMachine",
                table: "DyeingPrintingAreaInputProductionOrders",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionMachine",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.AlterColumn<string>(
                name: "ProductionMachine",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);
        }
    }
}
