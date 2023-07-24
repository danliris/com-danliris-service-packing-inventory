using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_table_MovementShippingDP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DPShippingMovements",
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
                    DestinationArea = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    DPShippingInputItemId = table.Column<int>(nullable: false),
                    DPShippingOutputItemId = table.Column<int>(nullable: false),
                    DPShippingDocumentId = table.Column<int>(nullable: false),
                    ProductionOrderId = table.Column<int>(nullable: false),
                    ProductionOrderNo = table.Column<string>(nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerName = table.Column<string>(nullable: true),
                    Construction = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Motif = table.Column<string>(nullable: true),
                    UomUnit = table.Column<string>(nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    Grade = table.Column<string>(nullable: true),
                    ProductionOrderType = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PackingType = table.Column<string>(nullable: true),
                    PackagingLength = table.Column<double>(nullable: false),
                    PackagingQty = table.Column<decimal>(nullable: false),
                    PackagingUnit = table.Column<string>(nullable: true),
                    MaterialOrigin = table.Column<string>(nullable: true),
                    ProductPackingCode = table.Column<string>(nullable: true),
                    ProductPackingId = table.Column<int>(nullable: false),
                    ProductTextileId = table.Column<int>(nullable: false),
                    ProductTextileCode = table.Column<string>(nullable: true),
                    ProductTextileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DPShippingMovements", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DPShippingMovements");
        }
    }
}
