using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class addtableLeftOverSalesExport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingExportCoverLetters",
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
                    ExportSalesNoteId = table.Column<int>(nullable: false),
                    NoteNo = table.Column<string>(maxLength: 50, nullable: true),
                    ExportCoverLetterNo = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerAdddress = table.Column<string>(maxLength: 1000, nullable: true),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    BCNo = table.Column<string>(maxLength: 50, nullable: true),
                    BCDate = table.Column<DateTimeOffset>(nullable: false),
                    Truck = table.Column<string>(maxLength: 250, nullable: true),
                    PlateNumber = table.Column<string>(maxLength: 250, nullable: true),
                    Driver = table.Column<string>(maxLength: 250, nullable: true),
                    ShippingStaffId = table.Column<int>(nullable: false),
                    ShippingStaffName = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingExportCoverLetters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingExportSalesContracts",
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
                    SalesContractNo = table.Column<string>(maxLength: 50, nullable: true),
                    SalesContractDate = table.Column<DateTimeOffset>(nullable: false),
                    TransactionTypeId = table.Column<int>(nullable: false),
                    TransactionTypeCode = table.Column<string>(maxLength: 100, nullable: true),
                    TransactionTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    SellerName = table.Column<string>(maxLength: 100, nullable: true),
                    SellerPosition = table.Column<string>(maxLength: 100, nullable: true),
                    SellerAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    SellerNPWP = table.Column<string>(maxLength: 50, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    BuyerNPWP = table.Column<string>(maxLength: 50, nullable: true),
                    IsUseVat = table.Column<bool>(nullable: false),
                    VatId = table.Column<int>(nullable: false),
                    VatRate = table.Column<int>(nullable: false),
                    SubTotal = table.Column<decimal>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingExportSalesContracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingExportSalesNotes",
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
                    SalesContractNo = table.Column<string>(maxLength: 50, nullable: true),
                    ExportSalesContractId = table.Column<int>(nullable: false),
                    NoteNo = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    TransactionTypeId = table.Column<int>(nullable: false),
                    TransactionTypeCode = table.Column<string>(maxLength: 100, nullable: true),
                    TransactionTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerNPWP = table.Column<string>(maxLength: 50, nullable: true),
                    KaberType = table.Column<string>(maxLength: 20, nullable: true),
                    ExpenditureNo = table.Column<string>(maxLength: 50, nullable: true),
                    PaymentType = table.Column<string>(maxLength: 20, nullable: true),
                    Tempo = table.Column<int>(nullable: false),
                    DispositionNo = table.Column<string>(maxLength: 100, nullable: true),
                    UseVat = table.Column<bool>(nullable: false),
                    VatId = table.Column<int>(nullable: false),
                    VatRate = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    IsUsed = table.Column<bool>(nullable: false),
                    IsApproveShipping = table.Column<bool>(nullable: false),
                    IsApproveFinance = table.Column<bool>(nullable: false),
                    ApproveShippingBy = table.Column<string>(nullable: true),
                    ApproveFinanceBy = table.Column<string>(nullable: true),
                    ApproveShippingDate = table.Column<DateTimeOffset>(nullable: false),
                    ApproveFinanceDate = table.Column<DateTimeOffset>(nullable: false),
                    IsRejectedFinance = table.Column<bool>(nullable: false),
                    IsRejectedShipping = table.Column<bool>(nullable: false),
                    RejectedReason = table.Column<string>(nullable: true),
                    BankId = table.Column<int>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingExportSalesNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingLeftOverExportSalesDOs",
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
                    ExportSalesDONo = table.Column<string>(maxLength: 50, nullable: true),
                    ExportSalesNoteNo = table.Column<string>(maxLength: 50, nullable: true),
                    ExportSalesNoteId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 255, nullable: true),
                    To = table.Column<string>(maxLength: 255, nullable: true),
                    StorageDivision = table.Column<string>(maxLength: 255, nullable: true),
                    Remark = table.Column<string>(maxLength: 3000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingLeftOverExportSalesDOs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingExportSalesContractItems",
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
                    ExportSalesContractId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ProductCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductName = table.Column<string>(maxLength: 250, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    RemainingQuantity = table.Column<double>(nullable: false),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 250, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    ComodityId = table.Column<int>(nullable: false),
                    ComodityCode = table.Column<string>(nullable: true),
                    ComodityName = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingExportSalesContractItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingExportSalesContractItems_GarmentShippingExportSalesContracts_ExportSalesContractId",
                        column: x => x.ExportSalesContractId,
                        principalTable: "GarmentShippingExportSalesContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingExportSalesNoteItems",
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
                    ExportSalesNoteId = table.Column<int>(nullable: false),
                    ExportSalesContractItemId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ProductCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductName = table.Column<string>(maxLength: 250, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 250, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    PackageQuantity = table.Column<double>(nullable: false),
                    PackageUomId = table.Column<int>(nullable: false),
                    PackageUomUnit = table.Column<string>(maxLength: 250, nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingExportSalesNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingExportSalesNoteItems_GarmentShippingExportSalesNotes_ExportSalesNoteId",
                        column: x => x.ExportSalesNoteId,
                        principalTable: "GarmentShippingExportSalesNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingLeftOverExportSalesDOItems",
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
                    ExportSalesDOId = table.Column<int>(nullable: false),
                    ExportSalesNoteItemId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ProductCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductName = table.Column<string>(maxLength: 500, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 100, nullable: true),
                    PackQuantity = table.Column<double>(nullable: false),
                    PackUomId = table.Column<int>(nullable: false),
                    PackUomUnit = table.Column<string>(maxLength: 100, nullable: true),
                    GrossWeight = table.Column<double>(nullable: false),
                    NettWeight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingLeftOverExportSalesDOItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingLeftOverExportSalesDOItems_GarmentShippingLeftOverExportSalesDOs_ExportSalesDOId",
                        column: x => x.ExportSalesDOId,
                        principalTable: "GarmentShippingLeftOverExportSalesDOs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingExportSalesContractItems_ExportSalesContractId",
                table: "GarmentShippingExportSalesContractItems",
                column: "ExportSalesContractId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingExportSalesContracts_SalesContractNo",
                table: "GarmentShippingExportSalesContracts",
                column: "SalesContractNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingExportSalesNoteItems_ExportSalesNoteId",
                table: "GarmentShippingExportSalesNoteItems",
                column: "ExportSalesNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingExportSalesNotes_NoteNo",
                table: "GarmentShippingExportSalesNotes",
                column: "NoteNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLeftOverExportSalesDOItems_ExportSalesDOId",
                table: "GarmentShippingLeftOverExportSalesDOItems",
                column: "ExportSalesDOId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingExportCoverLetters");

            migrationBuilder.DropTable(
                name: "GarmentShippingExportSalesContractItems");

            migrationBuilder.DropTable(
                name: "GarmentShippingExportSalesNoteItems");

            migrationBuilder.DropTable(
                name: "GarmentShippingLeftOverExportSalesDOItems");

            migrationBuilder.DropTable(
                name: "GarmentShippingExportSalesContracts");

            migrationBuilder.DropTable(
                name: "GarmentShippingExportSalesNotes");

            migrationBuilder.DropTable(
                name: "GarmentShippingLeftOverExportSalesDOs");
        }
    }
}
