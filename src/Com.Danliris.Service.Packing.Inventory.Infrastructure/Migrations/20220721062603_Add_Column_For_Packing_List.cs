using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_Column_For_Packing_List : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PackingListDescription",
                table: "DyeingPrintingAreaOutputs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackingListIssuedBy",
                table: "DyeingPrintingAreaOutputs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackingListLCNumber",
                table: "DyeingPrintingAreaOutputs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UpdateBySales",
                table: "DyeingPrintingAreaOutputs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackingListDescription",
                table: "DyeingPrintingAreaOutputs");

            migrationBuilder.DropColumn(
                name: "PackingListIssuedBy",
                table: "DyeingPrintingAreaOutputs");

            migrationBuilder.DropColumn(
                name: "PackingListLCNumber",
                table: "DyeingPrintingAreaOutputs");

            migrationBuilder.DropColumn(
                name: "UpdateBySales",
                table: "DyeingPrintingAreaOutputs");
        }
    }
}
