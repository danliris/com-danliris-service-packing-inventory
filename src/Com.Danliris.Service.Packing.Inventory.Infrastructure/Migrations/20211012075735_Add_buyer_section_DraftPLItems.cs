using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_buyer_section_DraftPLItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerCode",
                table: "GarmentDraftPackingListItems",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "GarmentDraftPackingListItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SectionCode",
                table: "GarmentDraftPackingListItems",
                maxLength: 5,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerCode",
                table: "GarmentDraftPackingListItems");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "GarmentDraftPackingListItems");

            migrationBuilder.DropColumn(
                name: "SectionCode",
                table: "GarmentDraftPackingListItems");
        }
    }
}
