using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddColoumComodityRONoLocalSalesNoteNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComodityCode",
                table: "GarmentMDLocalSalesNoteDetails",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComodityId",
                table: "GarmentMDLocalSalesNoteDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ComodityName",
                table: "GarmentMDLocalSalesNoteDetails",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RONo",
                table: "GarmentMDLocalSalesNoteDetails",
                maxLength: 15,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComodityCode",
                table: "GarmentMDLocalSalesNoteDetails");

            migrationBuilder.DropColumn(
                name: "ComodityId",
                table: "GarmentMDLocalSalesNoteDetails");

            migrationBuilder.DropColumn(
                name: "ComodityName",
                table: "GarmentMDLocalSalesNoteDetails");

            migrationBuilder.DropColumn(
                name: "RONo",
                table: "GarmentMDLocalSalesNoteDetails");
        }
    }
}
