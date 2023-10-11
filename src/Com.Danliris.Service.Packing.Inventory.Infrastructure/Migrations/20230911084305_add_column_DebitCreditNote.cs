using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_column_DebitCreditNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DebitCreditNoteId",
                table: "GarmentShippingNoteItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemTypeDebitCreditNote",
                table: "GarmentShippingNoteItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeDebitCreditNote",
                table: "GarmentShippingNoteItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebitCreditNoteId",
                table: "GarmentShippingNoteItems");

            migrationBuilder.DropColumn(
                name: "ItemTypeDebitCreditNote",
                table: "GarmentShippingNoteItems");

            migrationBuilder.DropColumn(
                name: "TypeDebitCreditNote",
                table: "GarmentShippingNoteItems");
        }
    }
}
