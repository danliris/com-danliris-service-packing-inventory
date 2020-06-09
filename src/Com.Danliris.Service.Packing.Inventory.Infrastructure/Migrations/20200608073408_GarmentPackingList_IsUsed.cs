using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentPackingList_IsUsed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "GarmentPackingLists",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_GarmentPackingLists_InvoiceNo",
                table: "GarmentPackingLists",
                column: "InvoiceNo",
                unique: true,
                filter: "[IsDeleted]=(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GarmentPackingLists_InvoiceNo",
                table: "GarmentPackingLists");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "GarmentPackingLists");
        }
    }
}
