using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AlterTableGarmentLocalSalesNoteAndCoverLetter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExpenditureNo",
                table: "GarmentShippingLocalSalesNotes",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KaberType",
                table: "GarmentShippingLocalSalesNotes",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BCNo",
                table: "GarmentShippingLocalCoverLetters",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenditureNo",
                table: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropColumn(
                name: "KaberType",
                table: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropColumn(
                name: "BCNo",
                table: "GarmentShippingLocalCoverLetters");
        }
    }
}
