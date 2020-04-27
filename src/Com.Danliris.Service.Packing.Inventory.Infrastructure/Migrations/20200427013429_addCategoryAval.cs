using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class addCategoryAval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvalLength",
                table: "NewFabricGradeTests",
                newName: "AvalConnectionLength");

            migrationBuilder.RenameColumn(
                name: "AvalLength",
                table: "DyeingPrintingAreaInputProductionOrders",
                newName: "AvalConnectionLength");

            migrationBuilder.AddColumn<double>(
                name: "AvalALength",
                table: "NewFabricGradeTests",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AvalBLength",
                table: "NewFabricGradeTests",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AvalALength",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AvalBLength",
                table: "DyeingPrintingAreaInputProductionOrders",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvalALength",
                table: "NewFabricGradeTests");

            migrationBuilder.DropColumn(
                name: "AvalBLength",
                table: "NewFabricGradeTests");

            migrationBuilder.DropColumn(
                name: "AvalALength",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropColumn(
                name: "AvalBLength",
                table: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.RenameColumn(
                name: "AvalConnectionLength",
                table: "NewFabricGradeTests",
                newName: "AvalLength");

            migrationBuilder.RenameColumn(
                name: "AvalConnectionLength",
                table: "DyeingPrintingAreaInputProductionOrders",
                newName: "AvalLength");
        }
    }
}
