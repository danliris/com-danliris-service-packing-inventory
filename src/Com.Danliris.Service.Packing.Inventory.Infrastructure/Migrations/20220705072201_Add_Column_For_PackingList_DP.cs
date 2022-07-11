using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_Column_For_PackingList_DP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PackingListAuthorized",
                table: "DyeingPrintingAreaOutputs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackingListNo",
                table: "DyeingPrintingAreaOutputs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackingListRemark",
                table: "DyeingPrintingAreaOutputs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackingType",
                table: "DyeingPrintingAreaOutputs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackingListBaleNo",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PackingListGross",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PackingListNet",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackingListAuthorized",
                table: "DyeingPrintingAreaOutputs");

            migrationBuilder.DropColumn(
                name: "PackingListNo",
                table: "DyeingPrintingAreaOutputs");

            migrationBuilder.DropColumn(
                name: "PackingListRemark",
                table: "DyeingPrintingAreaOutputs");

            migrationBuilder.DropColumn(
                name: "PackingType",
                table: "DyeingPrintingAreaOutputs");

            migrationBuilder.DropColumn(
                name: "PackingListBaleNo",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "PackingListGross",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "PackingListNet",
                table: "DyeingPrintingAreaOutputProductionOrders");
        }
    }
}
