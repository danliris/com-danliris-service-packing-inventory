using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_column_productpacking_dpwarehousemovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductPackingCode",
                table: "DPWarehouseMovements",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductPackingId",
                table: "DPWarehouseMovements",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPackingCode",
                table: "DPWarehouseMovements");

            migrationBuilder.DropColumn(
                name: "ProductPackingId",
                table: "DPWarehouseMovements");
        }
    }
}
