using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Update_MaterialDeliveryNoteWeaving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitLength",
                table: "MaterialDeliveryNoteWeaving");

            migrationBuilder.DropColumn(
                name: "UnitPacking",
                table: "MaterialDeliveryNoteWeaving");

            migrationBuilder.RenameColumn(
                name: "ItemType",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "itemType");

            migrationBuilder.RenameColumn(
                name: "ItemMaterialName",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "itemMaterialName");

            migrationBuilder.RenameColumn(
                name: "ItemCode",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "itemCode");

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "inputPiece");

            migrationBuilder.RenameColumn(
                name: "ItemNoSPP",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "itemNoSOP");

            migrationBuilder.RenameColumn(
                name: "ItemDesign",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "itemGrade");

            migrationBuilder.RenameColumn(
                name: "InputPacking",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "inputMeter");

            migrationBuilder.RenameColumn(
                name: "InputConversion",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "inputKg");

            migrationBuilder.AddColumn<decimal>(
                name: "inputBale",
                table: "ItemsMaterialDeliveryNoteWeaving",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "inputBale",
                table: "ItemsMaterialDeliveryNoteWeaving");

            migrationBuilder.RenameColumn(
                name: "itemType",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "ItemType");

            migrationBuilder.RenameColumn(
                name: "itemMaterialName",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "ItemMaterialName");

            migrationBuilder.RenameColumn(
                name: "itemCode",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "ItemCode");

            migrationBuilder.RenameColumn(
                name: "itemNoSOP",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "ItemNoSPP");

            migrationBuilder.RenameColumn(
                name: "itemGrade",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "ItemDesign");

            migrationBuilder.RenameColumn(
                name: "inputPiece",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "Length");

            migrationBuilder.RenameColumn(
                name: "inputMeter",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "InputPacking");

            migrationBuilder.RenameColumn(
                name: "inputKg",
                table: "ItemsMaterialDeliveryNoteWeaving",
                newName: "InputConversion");

            migrationBuilder.AddColumn<string>(
                name: "UnitLength",
                table: "MaterialDeliveryNoteWeaving",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitPacking",
                table: "MaterialDeliveryNoteWeaving",
                maxLength: 128,
                nullable: true);
        }
    }
}
