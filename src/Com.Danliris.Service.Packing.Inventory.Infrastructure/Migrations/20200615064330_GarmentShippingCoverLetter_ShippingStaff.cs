using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentShippingCoverLetter_ShippingStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingStaff",
                table: "GarmentShippingCoverLetters",
                newName: "ShippingStaffName");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "GarmentShippingCoverLetters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShippingStaffId",
                table: "GarmentShippingCoverLetters",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "GarmentShippingCoverLetters");

            migrationBuilder.DropColumn(
                name: "ShippingStaffId",
                table: "GarmentShippingCoverLetters");

            migrationBuilder.RenameColumn(
                name: "ShippingStaffName",
                table: "GarmentShippingCoverLetters",
                newName: "ShippingStaff");
        }
    }
}
