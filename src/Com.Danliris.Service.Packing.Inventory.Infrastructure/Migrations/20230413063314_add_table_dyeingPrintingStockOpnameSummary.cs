using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_table_dyeingPrintingStockOpnameSummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DyeingPrintingStockOpnameSummaries",
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
                    PackagingLength = table.Column<double>(nullable: false),
                    PackagingType = table.Column<string>(nullable: true),
                    PackagingUnit = table.Column<string>(nullable: true),
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderNo = table.Column<string>(nullable: true),
                    ProductionOrderType = table.Column<string>(nullable: true),
                    ProductionOrderOrderQuantity = table.Column<double>(nullable: false),
                    CreatedUtcOrderNo = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
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
                    ProductSKUId = table.Column<int>(nullable: false),
                    FabricSKUId = table.Column<int>(nullable: false),
                    ProductSKUCode = table.Column<string>(nullable: true),
                    ProductPackingId = table.Column<int>(nullable: false),
                    FabricPackingId = table.Column<int>(nullable: false),
                    ProductPackingCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingStockOpnameSummaries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DyeingPrintingStockOpnameSummaries");
        }
    }
}
