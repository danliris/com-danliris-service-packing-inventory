using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_Table_OuputShipping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DPShippingOutputs",
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
                    DestinationArea = table.Column<string>(nullable: true),
                    BonNo = table.Column<string>(nullable: true),
                    HasOuputArea = table.Column<bool>(nullable: false),
                    DeliveryOrderSalesId = table.Column<int>(nullable: false),
                    DeliveryOrderSalesNo = table.Column<string>(nullable: true),
                    DestinationBuyerName = table.Column<string>(nullable: true),
                    ShippingCode = table.Column<string>(nullable: true),
                    PackingListAuthorized = table.Column<string>(nullable: true),
                    PackingListNo = table.Column<string>(nullable: true),
                    PackingListRemark = table.Column<string>(nullable: true),
                    PackingType = table.Column<string>(nullable: true),
                    PackingListDescription = table.Column<string>(nullable: true),
                    PackingListIssuedBy = table.Column<string>(nullable: true),
                    PackingListLCNumber = table.Column<string>(nullable: true),
                    UpdateBySales = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPShippingOutputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DPShippingOutputItems",
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
                    Remark = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    PackingInstruction = table.Column<string>(nullable: true),
                    ProductionOrderType = table.Column<string>(nullable: true),
                    ProductionOrderOrderQuantity = table.Column<string>(nullable: true),
                    PackagingType = table.Column<string>(nullable: true),
                    PackagingQty = table.Column<decimal>(nullable: false),
                    PackagingUnit = table.Column<string>(nullable: true),
                    DeliveryOrderSalesId = table.Column<int>(nullable: false),
                    DeliveryOrderSalesNo = table.Column<string>(nullable: true),
                    HasNextAreaDocument = table.Column<bool>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    DestinationArea = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ShippingGrade = table.Column<string>(nullable: true),
                    ShippingRemark = table.Column<string>(nullable: true),
                    DPShippingOutputId = table.Column<int>(nullable: false),
                    ProductPackingCode = table.Column<string>(nullable: true),
                    ProductPackingId = table.Column<int>(nullable: false),
                    ProductSKUCode = table.Column<string>(nullable: true),
                    ProductSKUId = table.Column<int>(nullable: false),
                    ProcessTypeId = table.Column<int>(nullable: false),
                    ProcessTypeName = table.Column<string>(nullable: true),
                    YarnMaterialId = table.Column<int>(nullable: false),
                    YarnMaterialName = table.Column<string>(nullable: true),
                    FabricPackingId = table.Column<int>(nullable: false),
                    FabricSKUId = table.Column<int>(nullable: false),
                    PackagingLength = table.Column<double>(nullable: false),
                    FinishWidth = table.Column<string>(nullable: true),
                    DestinationBuyerName = table.Column<string>(nullable: true),
                    MaterialOrigin = table.Column<string>(nullable: true),
                    PackingListBaleNo = table.Column<string>(nullable: true),
                    PackingListGross = table.Column<decimal>(nullable: false),
                    PackingListNet = table.Column<decimal>(nullable: false),
                    DeliveryOrderSalesType = table.Column<string>(nullable: true),
                    CreatedUtcOrderNo = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPShippingOutputItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DPShippingOutputItems_DPShippingOutputs_DPShippingOutputId",
                        column: x => x.DPShippingOutputId,
                        principalTable: "DPShippingOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DPShippingOutputItems_DPShippingOutputId",
                table: "DPShippingOutputItems",
                column: "DPShippingOutputId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DPShippingOutputItems");

            migrationBuilder.DropTable(
                name: "DPShippingOutputs");
        }
    }
}
