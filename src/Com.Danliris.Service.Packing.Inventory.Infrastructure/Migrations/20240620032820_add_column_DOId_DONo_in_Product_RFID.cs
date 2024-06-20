using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_column_DOId_DONo_in_Product_RFID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DOId",
                table: "ProductRFIDs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DONo",
                table: "ProductRFIDs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOId",
                table: "ProductRFIDs");

            migrationBuilder.DropColumn(
                name: "DONo",
                table: "ProductRFIDs");
        }
    }
}
