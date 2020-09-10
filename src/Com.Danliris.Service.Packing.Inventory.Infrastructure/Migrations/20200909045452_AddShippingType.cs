using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddShippingType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductPackingCode",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 4096,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingType",
                table: "DyeingPrintingAreaInputs",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductPackingCode",
                table: "DyeingPrintingAreaInputProductionOrders",
                maxLength: 4096,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingType",
                table: "DyeingPrintingAreaInputs");

            migrationBuilder.AlterColumn<string>(
                name: "ProductPackingCode",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4096,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductPackingCode",
                table: "DyeingPrintingAreaInputProductionOrders",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4096,
                oldNullable: true);
        }
    }
}
