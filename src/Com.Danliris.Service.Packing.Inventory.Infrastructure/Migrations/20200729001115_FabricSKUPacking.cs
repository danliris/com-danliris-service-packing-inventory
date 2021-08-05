using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class FabricSKUPacking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductSKUID",
                table: "FabricProductSKUs",
                newName: "ProductSKUId");

            migrationBuilder.RenameColumn(
                name: "WeftThreadId",
                table: "FabricProductSKUs",
                newName: "YarnTypeId");

            migrationBuilder.RenameColumn(
                name: "WarpThreadId",
                table: "FabricProductSKUs",
                newName: "WeftId");

            migrationBuilder.RenameColumn(
                name: "ConstructionTypeId",
                table: "FabricProductSKUs",
                newName: "WarpId");

            migrationBuilder.RenameColumn(
                name: "ColorWayId",
                table: "FabricProductSKUs",
                newName: "ConstructionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductSKUId",
                table: "FabricProductSKUs",
                newName: "ProductSKUID");

            migrationBuilder.RenameColumn(
                name: "YarnTypeId",
                table: "FabricProductSKUs",
                newName: "WeftThreadId");

            migrationBuilder.RenameColumn(
                name: "WeftId",
                table: "FabricProductSKUs",
                newName: "WarpThreadId");

            migrationBuilder.RenameColumn(
                name: "WarpId",
                table: "FabricProductSKUs",
                newName: "ConstructionTypeId");

            migrationBuilder.RenameColumn(
                name: "ConstructionId",
                table: "FabricProductSKUs",
                newName: "ColorWayId");
        }
    }
}
