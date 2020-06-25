using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class update_GShipping_LocalReturnNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarmentShippingLocalReturnNoteItems_GarmentShippingLocalReturnNotes_GarmentShippingLocalReturnNoteModelId",
                table: "GarmentShippingLocalReturnNoteItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GarmentShippingLocalReturnNoteItems_GarmentShippingLocalSalesNoteItems_SalesNoteItemId",
                table: "GarmentShippingLocalReturnNoteItems");

            migrationBuilder.DropIndex(
                name: "IX_GarmentShippingLocalReturnNoteItems_GarmentShippingLocalReturnNoteModelId",
                table: "GarmentShippingLocalReturnNoteItems");

            migrationBuilder.DropColumn(
                name: "GarmentShippingLocalReturnNoteModelId",
                table: "GarmentShippingLocalReturnNoteItems");

            migrationBuilder.AlterColumn<string>(
                name: "ReturnNoteNo",
                table: "GarmentShippingLocalReturnNotes",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "GarmentShippingLocalReturnNotes",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedAgent",
                table: "GarmentShippingLocalReturnNotes",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "GarmentShippingLocalReturnNotes",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "GarmentShippingLocalReturnNotes",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAgent",
                table: "GarmentShippingLocalReturnNotes",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "GarmentShippingLocalReturnNotes",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAgent",
                table: "GarmentShippingLocalReturnNotes",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "GarmentShippingLocalReturnNoteItems",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedAgent",
                table: "GarmentShippingLocalReturnNoteItems",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "GarmentShippingLocalReturnNoteItems",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAgent",
                table: "GarmentShippingLocalReturnNoteItems",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "GarmentShippingLocalReturnNoteItems",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAgent",
                table: "GarmentShippingLocalReturnNoteItems",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalReturnNotes_ReturnNoteNo",
                table: "GarmentShippingLocalReturnNotes",
                column: "ReturnNoteNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalReturnNoteItems_ReturnNoteId",
                table: "GarmentShippingLocalReturnNoteItems",
                column: "ReturnNoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentShippingLocalReturnNoteItems_GarmentShippingLocalReturnNotes_ReturnNoteId",
                table: "GarmentShippingLocalReturnNoteItems",
                column: "ReturnNoteId",
                principalTable: "GarmentShippingLocalReturnNotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_GarmentShippingLocalReturnNoteItems_GarmentShippingLocalReturnNotes_ReturnNoteId",
                table: "GarmentShippingLocalReturnNoteItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GarmentShippingLocalReturnNoteItems_GarmentShippingLocalSalesNoteItems_SalesNoteItemId",
                table: "GarmentShippingLocalReturnNoteItems");

            migrationBuilder.DropIndex(
                name: "IX_GarmentShippingLocalReturnNotes_ReturnNoteNo",
                table: "GarmentShippingLocalReturnNotes");

            migrationBuilder.DropIndex(
                name: "IX_GarmentShippingLocalReturnNoteItems_ReturnNoteId",
                table: "GarmentShippingLocalReturnNoteItems");

            migrationBuilder.AlterColumn<string>(
                name: "ReturnNoteNo",
                table: "GarmentShippingLocalReturnNotes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "GarmentShippingLocalReturnNotes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedAgent",
                table: "GarmentShippingLocalReturnNotes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "GarmentShippingLocalReturnNotes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "GarmentShippingLocalReturnNotes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAgent",
                table: "GarmentShippingLocalReturnNotes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "GarmentShippingLocalReturnNotes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAgent",
                table: "GarmentShippingLocalReturnNotes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "GarmentShippingLocalReturnNoteItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedAgent",
                table: "GarmentShippingLocalReturnNoteItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "GarmentShippingLocalReturnNoteItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAgent",
                table: "GarmentShippingLocalReturnNoteItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "GarmentShippingLocalReturnNoteItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAgent",
                table: "GarmentShippingLocalReturnNoteItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GarmentShippingLocalReturnNoteModelId",
                table: "GarmentShippingLocalReturnNoteItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalReturnNoteItems_GarmentShippingLocalReturnNoteModelId",
                table: "GarmentShippingLocalReturnNoteItems",
                column: "GarmentShippingLocalReturnNoteModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentShippingLocalReturnNoteItems_GarmentShippingLocalReturnNotes_GarmentShippingLocalReturnNoteModelId",
                table: "GarmentShippingLocalReturnNoteItems",
                column: "GarmentShippingLocalReturnNoteModelId",
                principalTable: "GarmentShippingLocalReturnNotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
