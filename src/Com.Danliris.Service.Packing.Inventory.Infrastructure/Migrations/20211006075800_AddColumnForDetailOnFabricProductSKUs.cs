using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddColumnForDetailOnFabricProductSKUs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "FabricProductSKUs",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "FabricProductSKUs",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialConstructionId",
                table: "FabricProductSKUs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialConstructionName",
                table: "FabricProductSKUs",
                maxLength: 225,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "FabricProductSKUs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "FabricProductSKUs",
                maxLength: 225,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Motif",
                table: "FabricProductSKUs",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderNo",
                table: "FabricProductSKUs",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UomUnit",
                table: "FabricProductSKUs",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YarnMaterialId",
                table: "FabricProductSKUs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "YarnMaterialName",
                table: "FabricProductSKUs",
                maxLength: 225,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "FabricProductSKUs");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "FabricProductSKUs");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionId",
                table: "FabricProductSKUs");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionName",
                table: "FabricProductSKUs");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "FabricProductSKUs");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "FabricProductSKUs");

            migrationBuilder.DropColumn(
                name: "Motif",
                table: "FabricProductSKUs");

            migrationBuilder.DropColumn(
                name: "ProductionOrderNo",
                table: "FabricProductSKUs");

            migrationBuilder.DropColumn(
                name: "UomUnit",
                table: "FabricProductSKUs");

            migrationBuilder.DropColumn(
                name: "YarnMaterialId",
                table: "FabricProductSKUs");

            migrationBuilder.DropColumn(
                name: "YarnMaterialName",
                table: "FabricProductSKUs");
        }
    }
}
