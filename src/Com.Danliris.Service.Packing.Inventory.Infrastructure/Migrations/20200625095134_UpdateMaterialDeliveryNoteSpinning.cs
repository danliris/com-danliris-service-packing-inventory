using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class UpdateMaterialDeliveryNoteSpinning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StorageNumber",
                table: "MaterialDeliveryNote",
                newName: "StorageName");

            migrationBuilder.RenameColumn(
                name: "Sender",
                table: "MaterialDeliveryNote",
                newName: "StorageCode");

            migrationBuilder.RenameColumn(
                name: "Receiver",
                table: "MaterialDeliveryNote",
                newName: "SenderName");

            migrationBuilder.RenameColumn(
                name: "NoSPP",
                table: "Items",
                newName: "NoSOP");

            migrationBuilder.AddColumn<long>(
                name: "DoNumberId",
                table: "MaterialDeliveryNote",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverCode",
                table: "MaterialDeliveryNote",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "MaterialDeliveryNote",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "MaterialDeliveryNote",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SCNumberId",
                table: "MaterialDeliveryNote",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderCode",
                table: "MaterialDeliveryNote",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "MaterialDeliveryNote",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StorageId",
                table: "MaterialDeliveryNote",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WeightDOS",
                table: "Items",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WeightCone",
                table: "Items",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdSOP",
                table: "Items",
                maxLength: 128,
                nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "PrevSppInJson",
            //    table: "DyeingPrintingAreaOutputProductionOrders",
            //    type: "varchar(MAX)",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoNumberId",
                table: "MaterialDeliveryNote");

            migrationBuilder.DropColumn(
                name: "ReceiverCode",
                table: "MaterialDeliveryNote");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "MaterialDeliveryNote");

            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "MaterialDeliveryNote");

            migrationBuilder.DropColumn(
                name: "SCNumberId",
                table: "MaterialDeliveryNote");

            migrationBuilder.DropColumn(
                name: "SenderCode",
                table: "MaterialDeliveryNote");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "MaterialDeliveryNote");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "MaterialDeliveryNote");

            migrationBuilder.DropColumn(
                name: "IdSOP",
                table: "Items");

            //migrationBuilder.DropColumn(
            //    name: "PrevSppInJson",
            //    table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.RenameColumn(
                name: "StorageName",
                table: "MaterialDeliveryNote",
                newName: "StorageNumber");

            migrationBuilder.RenameColumn(
                name: "StorageCode",
                table: "MaterialDeliveryNote",
                newName: "Sender");

            migrationBuilder.RenameColumn(
                name: "SenderName",
                table: "MaterialDeliveryNote",
                newName: "Receiver");

            migrationBuilder.RenameColumn(
                name: "NoSOP",
                table: "Items",
                newName: "NoSPP");

            migrationBuilder.AlterColumn<decimal>(
                name: "WeightDOS",
                table: "Items",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "WeightCone",
                table: "Items",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);
        }
    }
}
