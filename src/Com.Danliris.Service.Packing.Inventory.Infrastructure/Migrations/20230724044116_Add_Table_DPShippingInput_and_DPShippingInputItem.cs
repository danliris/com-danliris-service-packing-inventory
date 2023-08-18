using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_Table_DPShippingInput_and_DPShippingInputItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DPShippingInputs",
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
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    ShippingType = table.Column<string>(nullable: true),
                    Shift = table.Column<string>(nullable: true),
                    BonNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPShippingInputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DPShippingInputItems",
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
                    ProductionOrderId = table.Column<int>(nullable: false),
                    ProductionOrderNo = table.Column<string>(nullable: true),
                    MaterialId = table.Column<int>(nullable: false),
                    MaterialName = table.Column<string>(nullable: true),
                    MaterialConstructionId = table.Column<int>(nullable: false),
                    MaterialConstructionName = table.Column<string>(nullable: true),
                    MaterialWidth = table.Column<string>(nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerName = table.Column<string>(nullable: true),
                    Construction = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Motif = table.Column<string>(nullable: true),
                    UomUnit = table.Column<string>(nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    BalanceRemains = table.Column<double>(nullable: false),
                    PackingInstruction = table.Column<string>(nullable: true),
                    ProductionOrderType = table.Column<string>(nullable: true),
                    ProductionOrderOrderQuantity = table.Column<string>(nullable: true),
                    CreatedUtcOrderNo = table.Column<DateTimeOffset>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DeliveryOrderSalesId = table.Column<int>(nullable: false),
                    DeliveryOrderSalesNo = table.Column<string>(nullable: true),
                    PackagingUnit = table.Column<string>(nullable: true),
                    PackagingType = table.Column<string>(nullable: true),
                    PackagingQty = table.Column<decimal>(nullable: false),
                    PackagingLength = table.Column<double>(nullable: false),
                    AreaOrigin = table.Column<string>(nullable: true),
                    DPShippingInputId = table.Column<int>(nullable: false),
                    ProductSKUId = table.Column<int>(nullable: false),
                    FabricSKUId = table.Column<int>(nullable: false),
                    ProductSKUCode = table.Column<string>(nullable: true),
                    ProductPackingId = table.Column<int>(nullable: false),
                    FabricPackingId = table.Column<int>(nullable: false),
                    ProductPackingCode = table.Column<string>(nullable: true),
                    ProcessTypeId = table.Column<int>(nullable: false),
                    ProcessTypeName = table.Column<string>(nullable: true),
                    YarnMaterialId = table.Column<int>(nullable: false),
                    YarnMaterialName = table.Column<string>(nullable: true),
                    InputPackagingQty = table.Column<double>(nullable: false),
                    InputQuantity = table.Column<double>(nullable: false),
                    DeliveryOrderReturId = table.Column<int>(nullable: false),
                    DeliveryOrderReturNo = table.Column<string>(nullable: true),
                    FinishWidth = table.Column<string>(nullable: true),
                    DestinationBuyerName = table.Column<string>(nullable: true),
                    MaterialOrigin = table.Column<string>(nullable: true),
                    DeliveryOrderSalesType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPShippingInputItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DPShippingInputItems_DPShippingInputs_DPShippingInputId",
                        column: x => x.DPShippingInputId,
                        principalTable: "DPShippingInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DPShippingInputItems_DPShippingInputId",
                table: "DPShippingInputItems",
                column: "DPShippingInputId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DPShippingInputItems");

            migrationBuilder.DropTable(
                name: "DPShippingInputs");
        }
    }
}
