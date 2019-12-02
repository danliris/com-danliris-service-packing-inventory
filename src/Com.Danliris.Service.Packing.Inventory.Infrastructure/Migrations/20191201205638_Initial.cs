using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PackagingInventoryDocuments",
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
                    DocumentNo = table.Column<string>(maxLength: 32, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    ReferenceNo = table.Column<string>(maxLength: 128, nullable: true),
                    ReferenceType = table.Column<string>(maxLength: 128, nullable: true),
                    Remark = table.Column<string>(maxLength: 1024, nullable: true),
                    Status = table.Column<string>(maxLength: 32, nullable: true),
                    Storage = table.Column<string>(maxLength: 1024, nullable: true),
                    StorageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackagingInventoryDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPackagings",
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
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    Composition = table.Column<string>(maxLength: 128, nullable: true),
                    Construction = table.Column<string>(maxLength: 128, nullable: true),
                    Currency = table.Column<string>(maxLength: 1024, nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: true),
                    Design = table.Column<string>(maxLength: 128, nullable: true),
                    Grade = table.Column<string>(maxLength: 32, nullable: true),
                    Lot = table.Column<string>(maxLength: 128, nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    PackagingCode = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ProductType = table.Column<string>(maxLength: 32, nullable: true),
                    SKU = table.Column<string>(maxLength: 1000, nullable: true),
                    SKUId = table.Column<string>(maxLength: 128, nullable: true),
                    Tags = table.Column<string>(maxLength: 512, nullable: true),
                    UOM = table.Column<string>(maxLength: 1024, nullable: true),
                    UOMId = table.Column<int>(nullable: false),
                    Width = table.Column<string>(maxLength: 128, nullable: true),
                    WovenType = table.Column<string>(maxLength: 128, nullable: true),
                    YarnType1 = table.Column<string>(maxLength: 128, nullable: true),
                    YarnType2 = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPackagings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSKUs",
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
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    Composition = table.Column<string>(maxLength: 128, nullable: true),
                    Construction = table.Column<string>(maxLength: 128, nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    Currency = table.Column<string>(maxLength: 1024, nullable: true),
                    Description = table.Column<string>(maxLength: 1024, nullable: true),
                    Design = table.Column<string>(maxLength: 128, nullable: true),
                    Grade = table.Column<string>(maxLength: 32, nullable: true),
                    Lot = table.Column<string>(maxLength: 128, nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ProductType = table.Column<string>(maxLength: 32, nullable: true),
                    SKUId = table.Column<string>(maxLength: 128, nullable: true),
                    SKUCode = table.Column<string>(maxLength: 32, nullable: true),
                    Tags = table.Column<string>(maxLength: 512, nullable: true),
                    UOM = table.Column<string>(maxLength: 1024, nullable: true),
                    UOMId = table.Column<int>(nullable: false),
                    Width = table.Column<string>(maxLength: 128, nullable: true),
                    WovenType = table.Column<string>(maxLength: 128, nullable: true),
                    YarnType1 = table.Column<string>(maxLength: 128, nullable: true),
                    YarnType2 = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSKUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SKUInventoryDocuments",
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
                    DocumentNo = table.Column<string>(maxLength: 32, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    ReferenceNo = table.Column<string>(maxLength: 128, nullable: true),
                    ReferenceType = table.Column<string>(maxLength: 128, nullable: true),
                    Status = table.Column<string>(maxLength: 32, nullable: true),
                    Storage = table.Column<string>(maxLength: 1024, nullable: true),
                    StorageId = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKUInventoryDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackagingInventoryDocumentItems",
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
                    Packaging = table.Column<string>(maxLength: 1024, nullable: true),
                    PackagingId = table.Column<int>(nullable: false),
                    PackagingInventoryDocumentId = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Remark = table.Column<string>(maxLength: 1024, nullable: true),
                    SKU = table.Column<string>(maxLength: 1024, nullable: true),
                    SKUId = table.Column<int>(nullable: false),
                    PackagingIventoryDocumentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackagingInventoryDocumentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackagingInventoryDocumentItems_PackagingInventoryDocuments_PackagingIventoryDocumentId",
                        column: x => x.PackagingIventoryDocumentId,
                        principalTable: "PackagingInventoryDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SKUInventoryDocumentItems",
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
                    Quantity = table.Column<decimal>(nullable: false),
                    Remark = table.Column<string>(maxLength: 1024, nullable: true),
                    SKU = table.Column<string>(maxLength: 1024, nullable: true),
                    SKUId = table.Column<int>(nullable: false),
                    SkuInventoryDocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKUInventoryDocumentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SKUInventoryDocumentItems_SKUInventoryDocuments_SkuInventoryDocumentId",
                        column: x => x.SkuInventoryDocumentId,
                        principalTable: "SKUInventoryDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackagingInventoryDocumentItems_PackagingIventoryDocumentId",
                table: "PackagingInventoryDocumentItems",
                column: "PackagingIventoryDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SKUInventoryDocumentItems_SkuInventoryDocumentId",
                table: "SKUInventoryDocumentItems",
                column: "SkuInventoryDocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackagingInventoryDocumentItems");

            migrationBuilder.DropTable(
                name: "ProductPackagings");

            migrationBuilder.DropTable(
                name: "ProductSKUs");

            migrationBuilder.DropTable(
                name: "SKUInventoryDocumentItems");

            migrationBuilder.DropTable(
                name: "PackagingInventoryDocuments");

            migrationBuilder.DropTable(
                name: "SKUInventoryDocuments");
        }
    }
}
