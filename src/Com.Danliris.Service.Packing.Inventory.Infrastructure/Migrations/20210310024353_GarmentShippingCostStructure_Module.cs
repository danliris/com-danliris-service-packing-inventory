using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentShippingCostStructure_Module : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingCostStructures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    InvoiceNo = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    ComodityId = table.Column<int>(nullable: false),
                    ComodityCode = table.Column<string>(maxLength: 50, nullable: true),
                    ComodityName = table.Column<string>(maxLength: 255, nullable: true),
                    HsCode = table.Column<string>(maxLength: 100, nullable: true),
                    Destination = table.Column<string>(maxLength: 50, nullable: true),
                    FabricTypeId = table.Column<int>(nullable: false),
                    FabricType = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingCostStructures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingCostStructureItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    CostStructureId = table.Column<int>(nullable: false),
                    SummaryValue = table.Column<double>(nullable: false),
                    SummaryPercentage = table.Column<double>(nullable: false),
                    CostStructureType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingCostStructureItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingCostStructureItems_GarmentShippingCostStructures_CostStructureId",
                        column: x => x.CostStructureId,
                        principalTable: "GarmentShippingCostStructures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingCostStructureDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    CostStructureItemId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    CountryFrom = table.Column<string>(maxLength: 50, nullable: true),
                    Percentage = table.Column<double>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingCostStructureDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingCostStructureDetails_GarmentShippingCostStructureItems_CostStructureItemId",
                        column: x => x.CostStructureItemId,
                        principalTable: "GarmentShippingCostStructureItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingCostStructureDetails_CostStructureItemId",
                table: "GarmentShippingCostStructureDetails",
                column: "CostStructureItemId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingCostStructureItems_CostStructureId",
                table: "GarmentShippingCostStructureItems",
                column: "CostStructureId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingCostStructures_InvoiceNo",
                table: "GarmentShippingCostStructures",
                column: "InvoiceNo",
                unique: true,
                filter: "[IsDeleted]=(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingCostStructureDetails");

            migrationBuilder.DropTable(
                name: "GarmentShippingCostStructureItems");

            migrationBuilder.DropTable(
                name: "GarmentShippingCostStructures");
        }
    }
}
