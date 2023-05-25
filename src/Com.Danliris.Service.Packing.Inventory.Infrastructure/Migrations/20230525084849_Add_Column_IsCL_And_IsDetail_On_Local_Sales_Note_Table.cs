using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_Column_IsCL_And_IsDetail_On_Local_Sales_Note_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCL",
                table: "GarmentShippingLocalSalesNotes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDetail",
                table: "GarmentShippingLocalSalesNotes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCL",
                table: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropColumn(
                name: "IsDetail",
                table: "GarmentShippingLocalSalesNotes");
        }
    }
}
