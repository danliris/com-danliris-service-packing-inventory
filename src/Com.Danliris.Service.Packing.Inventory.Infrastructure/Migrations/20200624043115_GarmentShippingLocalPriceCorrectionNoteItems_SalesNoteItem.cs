using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentShippingLocalPriceCorrectionNoteItems_SalesNoteItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalPriceCorrectionNoteItems_SalesNoteItemId",
                table: "GarmentShippingLocalPriceCorrectionNoteItems",
                column: "SalesNoteItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentShippingLocalPriceCorrectionNoteItems_GarmentShippingLocalSalesNoteItems_SalesNoteItemId",
                table: "GarmentShippingLocalPriceCorrectionNoteItems",
                column: "SalesNoteItemId",
                principalTable: "GarmentShippingLocalSalesNoteItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarmentShippingLocalPriceCorrectionNoteItems_GarmentShippingLocalSalesNoteItems_SalesNoteItemId",
                table: "GarmentShippingLocalPriceCorrectionNoteItems");

            migrationBuilder.DropIndex(
                name: "IX_GarmentShippingLocalPriceCorrectionNoteItems_SalesNoteItemId",
                table: "GarmentShippingLocalPriceCorrectionNoteItems");
        }
    }
}
