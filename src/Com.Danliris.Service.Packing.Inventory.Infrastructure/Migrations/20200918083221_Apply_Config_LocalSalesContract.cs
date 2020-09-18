using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Apply_Config_LocalSalesContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarmentShippingLocalSalesContractItems_GarmentShippingLocalSalesContracts_GarmentShippingLocalSalesContractModelId",
                table: "GarmentShippingLocalSalesContractItems");

            migrationBuilder.DropIndex(
                name: "IX_GarmentShippingLocalSalesContractItems_GarmentShippingLocalSalesContractModelId",
                table: "GarmentShippingLocalSalesContractItems");

            migrationBuilder.DropColumn(
                name: "GarmentShippingLocalSalesContractModelId",
                table: "GarmentShippingLocalSalesContractItems");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionTypeName",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TransactionTypeCode",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SellerPosition",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SellerName",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SellerNPWP",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SellerAddress",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SalesContractNo",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedAgent",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAgent",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAgent",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerName",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerNPWP",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerCode",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerAddress",
                table: "GarmentShippingLocalSalesContracts",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UomUnit",
                table: "GarmentShippingLocalSalesContractItems",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "GarmentShippingLocalSalesContractItems",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "GarmentShippingLocalSalesContractItems",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "GarmentShippingLocalSalesContractItems",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedAgent",
                table: "GarmentShippingLocalSalesContractItems",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "GarmentShippingLocalSalesContractItems",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAgent",
                table: "GarmentShippingLocalSalesContractItems",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "GarmentShippingLocalSalesContractItems",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAgent",
                table: "GarmentShippingLocalSalesContractItems",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalSalesContracts_SalesContractNo",
                table: "GarmentShippingLocalSalesContracts",
                column: "SalesContractNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalSalesContractItems_LocalSalesContractId",
                table: "GarmentShippingLocalSalesContractItems",
                column: "LocalSalesContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentShippingLocalSalesContractItems_GarmentShippingLocalSalesContracts_LocalSalesContractId",
                table: "GarmentShippingLocalSalesContractItems",
                column: "LocalSalesContractId",
                principalTable: "GarmentShippingLocalSalesContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarmentShippingLocalSalesContractItems_GarmentShippingLocalSalesContracts_LocalSalesContractId",
                table: "GarmentShippingLocalSalesContractItems");

            migrationBuilder.DropIndex(
                name: "IX_GarmentShippingLocalSalesContracts_SalesContractNo",
                table: "GarmentShippingLocalSalesContracts");

            migrationBuilder.DropIndex(
                name: "IX_GarmentShippingLocalSalesContractItems_LocalSalesContractId",
                table: "GarmentShippingLocalSalesContractItems");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionTypeName",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TransactionTypeCode",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SellerPosition",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SellerName",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SellerNPWP",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SellerAddress",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SalesContractNo",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedAgent",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAgent",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAgent",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerName",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerNPWP",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerCode",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerAddress",
                table: "GarmentShippingLocalSalesContracts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UomUnit",
                table: "GarmentShippingLocalSalesContractItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "GarmentShippingLocalSalesContractItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "GarmentShippingLocalSalesContractItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "GarmentShippingLocalSalesContractItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedAgent",
                table: "GarmentShippingLocalSalesContractItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "GarmentShippingLocalSalesContractItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAgent",
                table: "GarmentShippingLocalSalesContractItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "GarmentShippingLocalSalesContractItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAgent",
                table: "GarmentShippingLocalSalesContractItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GarmentShippingLocalSalesContractModelId",
                table: "GarmentShippingLocalSalesContractItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalSalesContractItems_GarmentShippingLocalSalesContractModelId",
                table: "GarmentShippingLocalSalesContractItems",
                column: "GarmentShippingLocalSalesContractModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentShippingLocalSalesContractItems_GarmentShippingLocalSalesContracts_GarmentShippingLocalSalesContractModelId",
                table: "GarmentShippingLocalSalesContractItems",
                column: "GarmentShippingLocalSalesContractModelId",
                principalTable: "GarmentShippingLocalSalesContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
