using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class addtableGarmentLocalSalesNoteReceiptSubcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentReceiptSubconLocalSalesNotes",
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
                    SalesContractId = table.Column<int>(nullable: false),
                    NoteNo = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerNPWP = table.Column<string>(maxLength: 50, nullable: true),
                    KaberType = table.Column<string>(maxLength: 20, nullable: true),
                    PaymentType = table.Column<string>(maxLength: 20, nullable: true),
                    Tempo = table.Column<int>(nullable: false),
                    UseVat = table.Column<bool>(nullable: false),
                    VatId = table.Column<int>(nullable: false),
                    VatRate = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    IsUsed = table.Column<bool>(nullable: false),
                    IsCL = table.Column<bool>(nullable: false),
                    IsDetail = table.Column<bool>(nullable: false),
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
                    table.PrimaryKey("PK_GarmentReceiptSubconLocalSalesNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentReceiptSubconLocalSalesNoteItems",
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
                    LocalSalesNoteId = table.Column<int>(nullable: false),
                    PackingListId = table.Column<int>(nullable: false),
                    InvoiceNo = table.Column<string>(maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_GarmentReceiptSubconLocalSalesNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentReceiptSubconLocalSalesNoteItems_GarmentReceiptSubconLocalSalesNotes_LocalSalesNoteId",
                        column: x => x.LocalSalesNoteId,
                        principalTable: "GarmentReceiptSubconLocalSalesNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentReceiptSubconLocalSalesNoteItems_LocalSalesNoteId",
                table: "GarmentReceiptSubconLocalSalesNoteItems",
                column: "LocalSalesNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentReceiptSubconLocalSalesNotes_NoteNo",
                table: "GarmentReceiptSubconLocalSalesNotes",
                column: "NoteNo",
                unique: true,
                filter: "[IsDeleted]=(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentReceiptSubconLocalSalesNoteItems");

            migrationBuilder.DropTable(
                name: "GarmentReceiptSubconLocalSalesNotes");
        }
    }
}
