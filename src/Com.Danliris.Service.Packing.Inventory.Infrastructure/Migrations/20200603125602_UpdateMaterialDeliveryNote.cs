using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class UpdateMaterialDeliveryNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DONumber",
                table: "MaterialDeliveryNote",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialDeliveryNoteModelId",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_MaterialDeliveryNoteModelId",
                table: "Items",
                column: "MaterialDeliveryNoteModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_MaterialDeliveryNote_MaterialDeliveryNoteModelId",
                table: "Items",
                column: "MaterialDeliveryNoteModelId",
                principalTable: "MaterialDeliveryNote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_MaterialDeliveryNote_MaterialDeliveryNoteModelId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_MaterialDeliveryNoteModelId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DONumber",
                table: "MaterialDeliveryNote");

            migrationBuilder.DropColumn(
                name: "MaterialDeliveryNoteModelId",
                table: "Items");
        }
    }
}
