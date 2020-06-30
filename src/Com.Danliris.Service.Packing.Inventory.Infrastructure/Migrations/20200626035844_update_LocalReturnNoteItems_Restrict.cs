using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class update_LocalReturnNoteItems_Restrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarmentShippingLocalReturnNoteItems_GarmentShippingLocalSalesNoteItems_SalesNoteItemId",
                table: "GarmentShippingLocalReturnNoteItems");

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentShippingLocalReturnNoteItems_GarmentShippingLocalSalesNoteItems_SalesNoteItemId",
                table: "GarmentShippingLocalReturnNoteItems",
                column: "SalesNoteItemId",
                principalTable: "GarmentShippingLocalSalesNoteItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarmentShippingLocalReturnNoteItems_GarmentShippingLocalSalesNoteItems_SalesNoteItemId",
                table: "GarmentShippingLocalReturnNoteItems");

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentShippingLocalReturnNoteItems_GarmentShippingLocalSalesNoteItems_SalesNoteItemId",
                table: "GarmentShippingLocalReturnNoteItems",
                column: "SalesNoteItemId",
                principalTable: "GarmentShippingLocalSalesNoteItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
