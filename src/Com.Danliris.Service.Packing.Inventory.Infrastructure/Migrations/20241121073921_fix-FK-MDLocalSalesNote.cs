using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class fixFKMDLocalSalesNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarmentMDLocalSalesNoteDetails_GarmentMDLocalSalesNoteItems_GarmentMDLocalSalesNoteItemModelId",
                table: "GarmentMDLocalSalesNoteDetails");

            migrationBuilder.DropIndex(
                name: "IX_GarmentMDLocalSalesNoteDetails_GarmentMDLocalSalesNoteItemModelId",
                table: "GarmentMDLocalSalesNoteDetails");

            migrationBuilder.DropColumn(
                name: "GarmentMDLocalSalesNoteItemModelId",
                table: "GarmentMDLocalSalesNoteDetails");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentMDLocalSalesNoteDetails_LocalSalesNoteItemId",
                table: "GarmentMDLocalSalesNoteDetails",
                column: "LocalSalesNoteItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentMDLocalSalesNoteDetails_GarmentMDLocalSalesNoteItems_LocalSalesNoteItemId",
                table: "GarmentMDLocalSalesNoteDetails",
                column: "LocalSalesNoteItemId",
                principalTable: "GarmentMDLocalSalesNoteItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarmentMDLocalSalesNoteDetails_GarmentMDLocalSalesNoteItems_LocalSalesNoteItemId",
                table: "GarmentMDLocalSalesNoteDetails");

            migrationBuilder.DropIndex(
                name: "IX_GarmentMDLocalSalesNoteDetails_LocalSalesNoteItemId",
                table: "GarmentMDLocalSalesNoteDetails");

            migrationBuilder.AddColumn<int>(
                name: "GarmentMDLocalSalesNoteItemModelId",
                table: "GarmentMDLocalSalesNoteDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GarmentMDLocalSalesNoteDetails_GarmentMDLocalSalesNoteItemModelId",
                table: "GarmentMDLocalSalesNoteDetails",
                column: "GarmentMDLocalSalesNoteItemModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentMDLocalSalesNoteDetails_GarmentMDLocalSalesNoteItems_GarmentMDLocalSalesNoteItemModelId",
                table: "GarmentMDLocalSalesNoteDetails",
                column: "GarmentMDLocalSalesNoteItemModelId",
                principalTable: "GarmentMDLocalSalesNoteItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
