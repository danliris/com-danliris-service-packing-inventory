using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class update_includeVat_GarmentShippingLocalPriceCuttingNoteItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UseVat",
                table: "GarmentShippingLocalPriceCuttingNoteItems",
                newName: "IncludeVat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IncludeVat",
                table: "GarmentShippingLocalPriceCuttingNoteItems",
                newName: "UseVat");
        }
    }
}
