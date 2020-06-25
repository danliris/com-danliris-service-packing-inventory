using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class ChangeAvalMachineToMachine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvalMachine",
                table: "DyeingPrintingAreaOutputProductionOrders",
                newName: "Machine");

            migrationBuilder.RenameColumn(
                name: "AvalMachine",
                table: "DyeingPrintingAreaInputProductionOrders",
                newName: "Machine");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Machine",
                table: "DyeingPrintingAreaOutputProductionOrders",
                newName: "AvalMachine");

            migrationBuilder.RenameColumn(
                name: "Machine",
                table: "DyeingPrintingAreaInputProductionOrders",
                newName: "AvalMachine");
        }
    }
}
