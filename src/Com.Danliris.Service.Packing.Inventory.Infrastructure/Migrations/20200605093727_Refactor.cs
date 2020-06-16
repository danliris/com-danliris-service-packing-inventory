using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryDocumentPackingItems");

            migrationBuilder.DropTable(
                name: "InventoryDocumentSKUItems");

            migrationBuilder.DropTable(
                name: "IPPackings");

            migrationBuilder.DropTable(
                name: "IPProducts");

            migrationBuilder.DropTable(
                name: "PackagingStock");

            migrationBuilder.DropTable(
                name: "InventoryDocumentPackings");

            migrationBuilder.DropTable(
                name: "InventoryDocumentSKUs");

            migrationBuilder.DropColumn(
                name: "Composition",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "Construction",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "Design",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "LotNo",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "UOMUnit",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "WovenType",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "YarnType1",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "YarnType2",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "PackingType",
                table: "ProductPackings");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductPackings");

            migrationBuilder.RenameColumn(
                name: "SKUId",
                table: "ProductPackings",
                newName: "UOMId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductSKUs",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "ProductSKUs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedAgent",
                table: "ProductSKUs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "ProductSKUs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAgent",
                table: "ProductSKUs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ProductSKUs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAgent",
                table: "ProductSKUs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ProductSKUs",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ProductSKUs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductSKUs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UOMId",
                table: "ProductSKUs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "ProductPackings",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedAgent",
                table: "ProductPackings",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "ProductPackings",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAgent",
                table: "ProductPackings",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ProductPackings",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAgent",
                table: "ProductPackings",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ProductPackings",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductPackings",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PackingSize",
                table: "ProductPackings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ProductSKUId",
                table: "ProductPackings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "IPUnitOfMeasurements",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "IPCategories",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "IPCategories",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FabricProductPackings",
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
                    Code = table.Column<string>(maxLength: 64, nullable: true),
                    FabricProductSKUId = table.Column<int>(nullable: false),
                    ProductSKUId = table.Column<int>(nullable: false),
                    ProductPackingId = table.Column<int>(nullable: false),
                    UOMUnit = table.Column<string>(maxLength: 64, nullable: true),
                    PackingSize = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FabricProductPackings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FabricProductSKUs",
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
                    Code = table.Column<string>(maxLength: 64, nullable: true),
                    Composition = table.Column<string>(maxLength: 128, nullable: true),
                    Construction = table.Column<string>(maxLength: 128, nullable: true),
                    Width = table.Column<string>(maxLength: 64, nullable: true),
                    Design = table.Column<string>(maxLength: 64, nullable: true),
                    Grade = table.Column<string>(maxLength: 32, nullable: true),
                    UOMUnit = table.Column<string>(maxLength: 64, nullable: true),
                    ProductSKUId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FabricProductSKUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GreigeProductPackings",
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
                    Code = table.Column<string>(maxLength: 64, nullable: true),
                    GreigeProductSKUId = table.Column<int>(nullable: false),
                    ProductSKUId = table.Column<int>(nullable: false),
                    ProductPackingId = table.Column<int>(nullable: false),
                    UOMUnit = table.Column<string>(maxLength: 64, nullable: true),
                    PackingSize = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreigeProductPackings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GreigeProductSKUs",
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
                    Code = table.Column<string>(maxLength: 64, nullable: true),
                    WovenType = table.Column<string>(maxLength: 128, nullable: true),
                    Construction = table.Column<string>(maxLength: 128, nullable: true),
                    Width = table.Column<string>(maxLength: 64, nullable: true),
                    Warp = table.Column<string>(maxLength: 64, nullable: true),
                    Weft = table.Column<string>(maxLength: 64, nullable: true),
                    Grade = table.Column<string>(maxLength: 32, nullable: true),
                    UOMUnit = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreigeProductSKUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPackingInventoryDocuments",
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
                    DocumentNo = table.Column<string>(maxLength: 64, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    ReferenceNo = table.Column<string>(maxLength: 64, nullable: true),
                    ReferenceType = table.Column<string>(maxLength: 256, nullable: true),
                    StorageId = table.Column<int>(nullable: false),
                    StorageName = table.Column<string>(maxLength: 512, nullable: true),
                    StorageCode = table.Column<string>(maxLength: 64, nullable: true),
                    InventoryType = table.Column<string>(maxLength: 32, nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPackingInventoryDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPackingInventoryMovements",
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
                    InventoryDocumentId = table.Column<int>(nullable: false),
                    ProductSKUId = table.Column<int>(nullable: false),
                    UOMId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPackingInventoryMovements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPackingInventorySummaries",
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
                    ProductPackingId = table.Column<int>(nullable: false),
                    StorageId = table.Column<int>(nullable: false),
                    StorageCode = table.Column<string>(maxLength: 64, nullable: true),
                    StorageName = table.Column<string>(maxLength: 512, nullable: true),
                    UOMId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPackingInventorySummaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSKUInventoryDocuments",
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
                    DocumentNo = table.Column<string>(maxLength: 64, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    ReferenceNo = table.Column<string>(maxLength: 64, nullable: true),
                    ReferenceType = table.Column<string>(maxLength: 256, nullable: true),
                    StorageId = table.Column<int>(nullable: false),
                    StorageName = table.Column<string>(maxLength: 512, nullable: true),
                    StorageCode = table.Column<string>(maxLength: 64, nullable: true),
                    InventoryType = table.Column<string>(maxLength: 32, nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSKUInventoryDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSKUInventoryMovements",
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
                    InventoryDocumentId = table.Column<int>(nullable: false),
                    ProductSKUId = table.Column<int>(nullable: false),
                    UOMId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSKUInventoryMovements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSKUInventorySummaries",
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
                    ProductSKUId = table.Column<int>(nullable: false),
                    StorageId = table.Column<int>(nullable: false),
                    StorageCode = table.Column<string>(maxLength: 64, nullable: true),
                    StorageName = table.Column<string>(maxLength: 256, nullable: true),
                    UOMId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSKUInventorySummaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YarnProductPackings",
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
                    Code = table.Column<string>(maxLength: 64, nullable: true),
                    YarnProductSKUId = table.Column<int>(nullable: false),
                    ProductSKUId = table.Column<int>(nullable: false),
                    ProductPackingId = table.Column<int>(nullable: false),
                    UOMUnit = table.Column<string>(maxLength: 64, nullable: true),
                    PackingSize = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YarnProductPackings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YarnProductSKUs",
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
                    Code = table.Column<string>(maxLength: 64, nullable: true),
                    YarnType = table.Column<string>(maxLength: 128, nullable: true),
                    LotNo = table.Column<string>(maxLength: 128, nullable: true),
                    UOMUnit = table.Column<string>(maxLength: 64, nullable: true),
                    ProductSKUId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YarnProductSKUs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FabricProductPackings");

            migrationBuilder.DropTable(
                name: "FabricProductSKUs");

            migrationBuilder.DropTable(
                name: "GreigeProductPackings");

            migrationBuilder.DropTable(
                name: "GreigeProductSKUs");

            migrationBuilder.DropTable(
                name: "ProductPackingInventoryDocuments");

            migrationBuilder.DropTable(
                name: "ProductPackingInventoryMovements");

            migrationBuilder.DropTable(
                name: "ProductPackingInventorySummaries");

            migrationBuilder.DropTable(
                name: "ProductSKUInventoryDocuments");

            migrationBuilder.DropTable(
                name: "ProductSKUInventoryMovements");

            migrationBuilder.DropTable(
                name: "ProductSKUInventorySummaries");

            migrationBuilder.DropTable(
                name: "YarnProductPackings");

            migrationBuilder.DropTable(
                name: "YarnProductSKUs");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "UOMId",
                table: "ProductSKUs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductPackings");

            migrationBuilder.DropColumn(
                name: "PackingSize",
                table: "ProductPackings");

            migrationBuilder.DropColumn(
                name: "ProductSKUId",
                table: "ProductPackings");

            migrationBuilder.RenameColumn(
                name: "UOMId",
                table: "ProductPackings",
                newName: "SKUId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductSKUs",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "ProductSKUs",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedAgent",
                table: "ProductSKUs",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "ProductSKUs",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAgent",
                table: "ProductSKUs",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ProductSKUs",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAgent",
                table: "ProductSKUs",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ProductSKUs",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Composition",
                table: "ProductSKUs",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Construction",
                table: "ProductSKUs",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Design",
                table: "ProductSKUs",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "ProductSKUs",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LotNo",
                table: "ProductSKUs",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "ProductSKUs",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UOMUnit",
                table: "ProductSKUs",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Width",
                table: "ProductSKUs",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WovenType",
                table: "ProductSKUs",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YarnType1",
                table: "ProductSKUs",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YarnType2",
                table: "ProductSKUs",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "ProductPackings",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedAgent",
                table: "ProductPackings",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "ProductPackings",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAgent",
                table: "ProductPackings",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ProductPackings",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAgent",
                table: "ProductPackings",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ProductPackings",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackingType",
                table: "ProductPackings",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "ProductPackings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "IPUnitOfMeasurements",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "IPCategories",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "IPCategories",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "InventoryDocumentPackings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    ReferenceNo = table.Column<string>(maxLength: 256, nullable: true),
                    ReferenceType = table.Column<string>(maxLength: 256, nullable: true),
                    Remark = table.Column<string>(maxLength: 1024, nullable: true),
                    Storage = table.Column<string>(nullable: true),
                    StorageId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDocumentPackings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryDocumentSKUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    ReferenceNo = table.Column<string>(maxLength: 256, nullable: true),
                    ReferenceType = table.Column<string>(maxLength: 256, nullable: true),
                    Remark = table.Column<string>(maxLength: 1024, nullable: true),
                    Storage = table.Column<string>(nullable: true),
                    StorageId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDocumentSKUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IPPackings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAgent = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DeletedAgent = table.Column<string>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    Size = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPPackings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IPProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    CreatedAgent = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DeletedAgent = table.Column<string>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    PackingId = table.Column<int>(nullable: false),
                    UOMId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackagingStock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DyeingPrintingProductionOrderId = table.Column<int>(maxLength: 128, nullable: false),
                    HasNextArea = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PackagingQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PackagingType = table.Column<string>(maxLength: 128, nullable: true),
                    PackagingUnit = table.Column<string>(maxLength: 128, nullable: true),
                    ProductionOrderNo = table.Column<string>(maxLength: 128, nullable: true),
                    UomUnit = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackagingStock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryDocumentPackingItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    InventoryDocumentPackingId = table.Column<int>(nullable: true),
                    InventoryDocumentSKUId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    PackingId = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    SKUId = table.Column<int>(nullable: false),
                    UOMUnit = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDocumentPackingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryDocumentPackingItems_InventoryDocumentPackings_InventoryDocumentPackingId",
                        column: x => x.InventoryDocumentPackingId,
                        principalTable: "InventoryDocumentPackings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryDocumentSKUItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    BeforeQuantity = table.Column<decimal>(nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CurrentQuantity = table.Column<decimal>(nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    InventoryDocumentSKUId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    SKUId = table.Column<int>(nullable: false),
                    UOMUnit = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDocumentSKUItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryDocumentSKUItems_InventoryDocumentSKUs_InventoryDocumentSKUId",
                        column: x => x.InventoryDocumentSKUId,
                        principalTable: "InventoryDocumentSKUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDocumentPackingItems_InventoryDocumentPackingId",
                table: "InventoryDocumentPackingItems",
                column: "InventoryDocumentPackingId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDocumentSKUItems_InventoryDocumentSKUId",
                table: "InventoryDocumentSKUItems",
                column: "InventoryDocumentSKUId");
        }
    }
}
