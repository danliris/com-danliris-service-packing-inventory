using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class StockOpnameReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockOpnameReportHeaders",
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
                    Date = table.Column<DateTime>(nullable: false),
                    Zone = table.Column<string>(maxLength: 128, nullable: true),
                    Unit = table.Column<string>(maxLength: 128, nullable: true),
                    Material = table.Column<string>(maxLength: 1024, nullable: true),
                    Buyer = table.Column<string>(maxLength: 1024, nullable: true),
                    ProductionOrderId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOpnameReportHeaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockOpnameReportItems",
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
                    ProductionOrderNo = table.Column<string>(maxLength: 128, nullable: true),
                    Material = table.Column<string>(maxLength: 128, nullable: true),
                    Unit = table.Column<string>(maxLength: 128, nullable: true),
                    Motif = table.Column<string>(maxLength: 128, nullable: true),
                    Buyer = table.Column<string>(maxLength: 1024, nullable: true),
                    Color = table.Column<string>(maxLength: 128, nullable: true),
                    Grade = table.Column<string>(maxLength: 128, nullable: true),
                    Jenis = table.Column<string>(maxLength: 128, nullable: true),
                    StockOpnameQuantity = table.Column<double>(nullable: false),
                    WarehouseQuantity = table.Column<double>(nullable: false),
                    Difference = table.Column<double>(nullable: false),
                    HeaderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOpnameReportItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockOpnameReportHeaders");

            migrationBuilder.DropTable(
                name: "StockOpnameReportItems");
        }
    }
}
