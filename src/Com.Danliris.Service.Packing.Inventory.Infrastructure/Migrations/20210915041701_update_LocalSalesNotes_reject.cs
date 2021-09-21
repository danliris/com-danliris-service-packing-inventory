using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class update_LocalSalesNotes_reject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRejectedFinance",
                table: "GarmentShippingLocalSalesNotes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejectedShipping",
                table: "GarmentShippingLocalSalesNotes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRejectedFinance",
                table: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropColumn(
                name: "IsRejectedShipping",
                table: "GarmentShippingLocalSalesNotes");
        }
    }
}
