using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_Table_StockOpname_Mutation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DyeingPrintingStockOpnameMutations",
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
                    Area = table.Column<string>(nullable: true),
                    BonNo = table.Column<string>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingStockOpnameMutations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DyeingPrintingStockOpnameMutationItems",
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
                    Color = table.Column<string>(nullable: true),
                    Construction = table.Column<string>(nullable: true),
                    DyeingPrintingStockOpnameMutationId = table.Column<int>(nullable: false),
                    Grade = table.Column<string>(nullable: true),
                    Motif = table.Column<string>(nullable: true),
                    PackagingQty = table.Column<decimal>(nullable: false),
                    PackagingLength = table.Column<double>(nullable: false),
                    PackagingType = table.Column<string>(nullable: true),
                    PackagingUnit = table.Column<string>(nullable: true),
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderNo = table.Column<string>(nullable: true),
                    ProductionOrderType = table.Column<string>(nullable: true),
                    ProductionOrderOrderQuantity = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    ProcessTypeId = table.Column<int>(nullable: false),
                    ProcessTypeName = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    UomUnit = table.Column<string>(nullable: true),
                    TrackId = table.Column<int>(nullable: false),
                    TrackType = table.Column<string>(nullable: true),
                    TrackName = table.Column<string>(nullable: true),
                    ProductSKUId = table.Column<int>(nullable: false),
                    FabricSKUId = table.Column<int>(nullable: false),
                    ProductSKUCode = table.Column<string>(nullable: true),
                    ProductPackingId = table.Column<int>(nullable: false),
                    FabricPackingId = table.Column<int>(nullable: false),
                    ProductPackingCode = table.Column<string>(nullable: true),
                    TypeOut = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingStockOpnameMutationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DyeingPrintingStockOpnameMutationItems_DyeingPrintingStockOpnameMutations_DyeingPrintingStockOpnameMutationId",
                        column: x => x.DyeingPrintingStockOpnameMutationId,
                        principalTable: "DyeingPrintingStockOpnameMutations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingStockOpnameMutationItems_DyeingPrintingStockOpnameMutationId",
                table: "DyeingPrintingStockOpnameMutationItems",
                column: "DyeingPrintingStockOpnameMutationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DyeingPrintingStockOpnameMutationItems");

            migrationBuilder.DropTable(
                name: "DyeingPrintingStockOpnameMutations");
        }
    }
}
