using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_table_DP_Warehouse_All : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DPWarehouseInputItems",
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
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderNo = table.Column<string>(nullable: true),
                    MaterialId = table.Column<int>(nullable: false),
                    MaterialName = table.Column<string>(nullable: true),
                    MaterialConstructionId = table.Column<int>(nullable: false),
                    MaterialConstructionName = table.Column<string>(nullable: true),
                    MaterialWidth = table.Column<string>(nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    Buyer = table.Column<string>(nullable: true),
                    Construction = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Motif = table.Column<string>(nullable: true),
                    UomUnit = table.Column<string>(nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    PackingInstruction = table.Column<string>(nullable: true),
                    ProductionOrderType = table.Column<string>(nullable: true),
                    ProductionOrderOrderQuantity = table.Column<double>(nullable: false),
                    CreatedUtcOrderNo = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DeliveryOrderSalesId = table.Column<long>(nullable: false),
                    DeliveryOrderSalesNo = table.Column<string>(nullable: true),
                    PackagingUnit = table.Column<string>(nullable: true),
                    PackagingType = table.Column<string>(nullable: true),
                    PackagingQty = table.Column<decimal>(nullable: false),
                    PackagingLength = table.Column<double>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    DPWarehouseInputId = table.Column<int>(nullable: false),
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
                    InputPackagingQty = table.Column<decimal>(nullable: false),
                    InputQuantity = table.Column<double>(nullable: false),
                    DeliveryOrderReturId = table.Column<long>(nullable: false),
                    DeliveryOrderReturNo = table.Column<string>(nullable: true),
                    FinishWidth = table.Column<string>(nullable: true),
                    DateIn = table.Column<DateTimeOffset>(nullable: false),
                    DestinationBuyerName = table.Column<string>(nullable: true),
                    MaterialOrigin = table.Column<string>(nullable: true),
                    DeliveryOrderSalesType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPWarehouseInputItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DPWarehouseInputs",
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
                    Area = table.Column<string>(nullable: true),
                    Shift = table.Column<string>(nullable: true),
                    BonNo = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    ShippingType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPWarehouseInputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DPWarehouseMovements",
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
                    Area = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    DPDocumentId = table.Column<int>(nullable: false),
                    DPDocumentItemId = table.Column<int>(nullable: false),
                    DPDocumentBonNo = table.Column<int>(nullable: false),
                    DPSummaryId = table.Column<int>(nullable: false),
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderNo = table.Column<string>(nullable: true),
                    Buyer = table.Column<string>(nullable: true),
                    Construction = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Motif = table.Column<string>(nullable: true),
                    UomUnit = table.Column<string>(nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    Grade = table.Column<string>(nullable: true),
                    ProductionOrderType = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    PackingType = table.Column<string>(nullable: true),
                    PackagingQty = table.Column<decimal>(nullable: false),
                    PackagingUnit = table.Column<string>(nullable: true),
                    PackagingLength = table.Column<double>(nullable: false),
                    MaterialOrigin = table.Column<string>(nullable: true),
                    ProductTextileId = table.Column<int>(nullable: true),
                    ProductTextileCode = table.Column<string>(nullable: true),
                    ProductTextileName = table.Column<string>(nullable: true),
                    TrackFromId = table.Column<int>(nullable: false),
                    TrackFromType = table.Column<string>(nullable: true),
                    TrackFromName = table.Column<string>(nullable: true),
                    TrackFromBox = table.Column<string>(nullable: true),
                    TrackToId = table.Column<int>(nullable: false),
                    TrackToType = table.Column<string>(nullable: true),
                    TrackToName = table.Column<string>(nullable: true),
                    TrackToBox = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPWarehouseMovements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DPWarehouseOutputItems",
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
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderNo = table.Column<string>(nullable: true),
                    MaterialId = table.Column<int>(nullable: false),
                    MaterialName = table.Column<string>(nullable: true),
                    MaterialConstructionId = table.Column<int>(nullable: false),
                    MaterialConstructionName = table.Column<string>(nullable: true),
                    MaterialWidth = table.Column<string>(nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    Buyer = table.Column<string>(nullable: true),
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
                    ProductionOrderOrderQuantity = table.Column<double>(nullable: false),
                    PackagingType = table.Column<string>(nullable: true),
                    PackagingQty = table.Column<decimal>(nullable: false),
                    PackagingLength = table.Column<double>(nullable: false),
                    PackagingUnit = table.Column<string>(nullable: true),
                    DeliveryOrderSalesId = table.Column<long>(nullable: false),
                    DeliveryOrderSalesNo = table.Column<string>(nullable: true),
                    ProductionMachine = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    DestinationArea = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ShippingGrade = table.Column<string>(nullable: true),
                    ShippingRemark = table.Column<string>(nullable: true),
                    DPWarehouseOuputId = table.Column<int>(nullable: false),
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
                    NextAreaInputStatus = table.Column<string>(nullable: true),
                    HasNextAreaDocument = table.Column<bool>(nullable: false),
                    FinishWidth = table.Column<string>(nullable: true),
                    DateOut = table.Column<DateTimeOffset>(nullable: false),
                    DestinationBuyerName = table.Column<string>(nullable: true),
                    MaterialOrigin = table.Column<string>(nullable: true),
                    DeliveryOrderSalesType = table.Column<string>(nullable: true),
                    PackingListBaleNo = table.Column<string>(nullable: true),
                    PackingListNet = table.Column<decimal>(nullable: false),
                    PackingListGross = table.Column<decimal>(nullable: false),
                    ProductTextileId = table.Column<int>(nullable: true),
                    ProductTextileCode = table.Column<string>(nullable: true),
                    ProductTextileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPWarehouseOutputItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DPWarehouseOutputs",
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
                    Area = table.Column<string>(nullable: true),
                    Shift = table.Column<string>(nullable: true),
                    BonNo = table.Column<string>(nullable: true),
                    DestinationArea = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    DeliveryOrderSalesId = table.Column<long>(nullable: false),
                    DeliveryOrderSalesNo = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    ShippingCode = table.Column<string>(nullable: true),
                    PackingListNo = table.Column<string>(nullable: true),
                    PackingType = table.Column<string>(nullable: true),
                    PackingListRemark = table.Column<string>(nullable: true),
                    PackingListAuthorized = table.Column<string>(nullable: true),
                    PackingListLCNumber = table.Column<string>(nullable: true),
                    PackingListIssuedBy = table.Column<string>(nullable: true),
                    PackingListDescription = table.Column<string>(nullable: true),
                    UpdateBySales = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPWarehouseOutputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DPWarehouseSummaries",
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
                    BalanceOut = table.Column<double>(nullable: false),
                    BuyerId = table.Column<int>(nullable: false),
                    Buyer = table.Column<string>(nullable: true),
                    CartNo = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Construction = table.Column<string>(nullable: true),
                    DyeingPrintingStockOpnameId = table.Column<int>(nullable: false),
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
                    PackagingQtyOut = table.Column<decimal>(nullable: false),
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
                    SplitQuantity = table.Column<double>(nullable: false),
                    ProductSKUId = table.Column<int>(nullable: false),
                    FabricSKUId = table.Column<int>(nullable: false),
                    ProductSKUCode = table.Column<string>(nullable: true),
                    ProductPackingId = table.Column<int>(nullable: false),
                    FabricPackingId = table.Column<int>(nullable: false),
                    ProductPackingCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPWarehouseSummaries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DPWarehouseInputItems");

            migrationBuilder.DropTable(
                name: "DPWarehouseInputs");

            migrationBuilder.DropTable(
                name: "DPWarehouseMovements");

            migrationBuilder.DropTable(
                name: "DPWarehouseOutputItems");

            migrationBuilder.DropTable(
                name: "DPWarehouseOutputs");

            migrationBuilder.DropTable(
                name: "DPWarehouseSummaries");
        }
    }
}
