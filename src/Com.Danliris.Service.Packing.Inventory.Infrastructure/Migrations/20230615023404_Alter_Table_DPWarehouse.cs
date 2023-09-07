using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Alter_Table_DPWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DyeingPrintingStockOpnameId",
                table: "DPWarehouseSummaries");

            migrationBuilder.RenameColumn(
                name: "DPWarehouseOuputId",
                table: "DPWarehouseOutputItems",
                newName: "DPWarehouseOutputId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DPWarehouseSummaries",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DPDocumentBonNo",
                table: "DPWarehouseMovements",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "DPWarehousePreInputs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAgent = table.Column<string>(nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedAgent = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedAgent = table.Column<string>(nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    BalanceRemains = table.Column<double>(nullable: false),
                    BalanceReceipt = table.Column<double>(nullable: false),
                    BalanceReject = table.Column<double>(nullable: false),
                    BuyerId = table.Column<int>(nullable: false),
                    Buyer = table.Column<string>(nullable: true),
                    CartNo = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Construction = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    MaterialConstructionId = table.Column<int>(nullable: false),
                    MaterialConstructionName = table.Column<string>(nullable: true),
                    MaterialId = table.Column<int>(nullable: false),
                    MaterialName = table.Column<string>(nullable: true),
                    MaterialWidth = table.Column<string>(nullable: true),
                    Motif = table.Column<string>(nullable: true),
                    PackingInstruction = table.Column<string>(nullable: true),
                    PackagingQty = table.Column<decimal>(nullable: false),
                    PackagingQtyRemains = table.Column<decimal>(nullable: false),
                    PackagingQtyReceipt = table.Column<decimal>(nullable: false),
                    PackagingQtyReject = table.Column<decimal>(nullable: false),
                    PackagingLength = table.Column<double>(nullable: false),
                    PackagingType = table.Column<string>(nullable: true),
                    PackagingUnit = table.Column<string>(nullable: true),
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderNo = table.Column<string>(nullable: true),
                    ProductionOrderType = table.Column<string>(nullable: true),
                    ProductionOrderOrderQuantity = table.Column<double>(nullable: false),
                    CreatedUtcOrderNo = table.Column<DateTime>(nullable: false),
                    ProcessTypeId = table.Column<int>(nullable: false),
                    ProcessTypeName = table.Column<string>(nullable: true),
                    YarnMaterialId = table.Column<int>(nullable: false),
                    YarnMaterialName = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    UomUnit = table.Column<string>(nullable: true),
                    TrackId = table.Column<int>(nullable: false),
                    TrackType = table.Column<string>(nullable: true),
                    TrackName = table.Column<string>(nullable: true),
                    TrackBox = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MaterialOrigin = table.Column<string>(nullable: true),
                    FinishWidth = table.Column<string>(nullable: true),
                    ProductSKUId = table.Column<int>(nullable: false),
                    FabricSKUId = table.Column<int>(nullable: false),
                    ProductSKUCode = table.Column<string>(nullable: true),
                    ProductPackingId = table.Column<int>(nullable: false),
                    FabricPackingId = table.Column<int>(nullable: false),
                    ProductPackingCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPWarehousePreInputs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DPWarehouseOutputItems_DPWarehouseOutputId",
                table: "DPWarehouseOutputItems",
                column: "DPWarehouseOutputId");

            migrationBuilder.CreateIndex(
                name: "IX_DPWarehouseInputItems_DPWarehouseInputId",
                table: "DPWarehouseInputItems",
                column: "DPWarehouseInputId");

            migrationBuilder.AddForeignKey(
                name: "FK_DPWarehouseInputItems_DPWarehouseInputs_DPWarehouseInputId",
                table: "DPWarehouseInputItems",
                column: "DPWarehouseInputId",
                principalTable: "DPWarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DPWarehouseOutputItems_DPWarehouseOutputs_DPWarehouseOutputId",
                table: "DPWarehouseOutputItems",
                column: "DPWarehouseOutputId",
                principalTable: "DPWarehouseOutputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DPWarehouseInputItems_DPWarehouseInputs_DPWarehouseInputId",
                table: "DPWarehouseInputItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DPWarehouseOutputItems_DPWarehouseOutputs_DPWarehouseOutputId",
                table: "DPWarehouseOutputItems");

            migrationBuilder.DropTable(
                name: "DPWarehousePreInputs");

            migrationBuilder.DropIndex(
                name: "IX_DPWarehouseOutputItems_DPWarehouseOutputId",
                table: "DPWarehouseOutputItems");

            migrationBuilder.DropIndex(
                name: "IX_DPWarehouseInputItems_DPWarehouseInputId",
                table: "DPWarehouseInputItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DPWarehouseSummaries");

            migrationBuilder.RenameColumn(
                name: "DPWarehouseOutputId",
                table: "DPWarehouseOutputItems",
                newName: "DPWarehouseOuputId");

            migrationBuilder.AddColumn<int>(
                name: "DyeingPrintingStockOpnameId",
                table: "DPWarehouseSummaries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DPDocumentBonNo",
                table: "DPWarehouseMovements",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
