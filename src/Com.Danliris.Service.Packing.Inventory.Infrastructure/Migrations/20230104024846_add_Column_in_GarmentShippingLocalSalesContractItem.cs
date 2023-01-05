using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_Column_in_GarmentShippingLocalSalesContractItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComodityCode",
                table: "GarmentShippingLocalSalesContractItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComodityId",
                table: "GarmentShippingLocalSalesContractItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ComodityName",
                table: "GarmentShippingLocalSalesContractItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "GarmentShippingLocalSalesContractItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComodityCode",
                table: "GarmentShippingLocalSalesContractItems");

            migrationBuilder.DropColumn(
                name: "ComodityId",
                table: "GarmentShippingLocalSalesContractItems");

            migrationBuilder.DropColumn(
                name: "ComodityName",
                table: "GarmentShippingLocalSalesContractItems");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "GarmentShippingLocalSalesContractItems");
        }
    }
}
