using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_column_afterStockOpname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AfterStockOpname",
                table: "ProductSKUs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AfterStockOpname",
                table: "ProductPackings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AfterStockOpname",
                table: "FabricProductSKUs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AfterStockOpname",
                table: "FabricProductPackings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfterStockOpname",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "AfterStockOpname",
                table: "ProductPackings");

            migrationBuilder.DropColumn(
                name: "AfterStockOpname",
                table: "FabricProductSKUs");

            migrationBuilder.DropColumn(
                name: "AfterStockOpname",
                table: "FabricProductPackings");
        }
    }
}
