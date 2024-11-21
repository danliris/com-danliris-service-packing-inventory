using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddTableLocalMDSalesNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentMDLocalSalesNotes",
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
                    LocalSalesContractId = table.Column<int>(nullable: false),
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
                    AccountNumber = table.Column<string>(nullable: true),
                    IsSubconPackingList = table.Column<bool>(nullable: false),
                    BCNo = table.Column<string>(nullable: true),
                    BCType = table.Column<string>(nullable: true),
                    BCDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentMDLocalSalesNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentMDLocalSalesNoteItems",
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
                    LocalSalesContractId = table.Column<int>(nullable: false),
                    ComodityName = table.Column<string>(maxLength: 250, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 250, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentMDLocalSalesNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentMDLocalSalesNoteItems_GarmentMDLocalSalesNotes_LocalSalesNoteId",
                        column: x => x.LocalSalesNoteId,
                        principalTable: "GarmentMDLocalSalesNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentMDLocalSalesNoteDetails",
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
                    LocalSalesNoteItemId = table.Column<int>(nullable: false),
                    BonNo = table.Column<string>(maxLength: 250, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 250, nullable: true),
                    BonFrom = table.Column<string>(maxLength: 250, nullable: true),
                    GarmentMDLocalSalesNoteItemModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentMDLocalSalesNoteDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentMDLocalSalesNoteDetails_GarmentMDLocalSalesNoteItems_GarmentMDLocalSalesNoteItemModelId",
                        column: x => x.GarmentMDLocalSalesNoteItemModelId,
                        principalTable: "GarmentMDLocalSalesNoteItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentMDLocalSalesNoteDetails_GarmentMDLocalSalesNoteItemModelId",
                table: "GarmentMDLocalSalesNoteDetails",
                column: "GarmentMDLocalSalesNoteItemModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentMDLocalSalesNoteItems_LocalSalesNoteId",
                table: "GarmentMDLocalSalesNoteItems",
                column: "LocalSalesNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentMDLocalSalesNotes_NoteNo",
                table: "GarmentMDLocalSalesNotes",
                column: "NoteNo",
                unique: true,
                filter: "[IsDeleted]=(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentMDLocalSalesNoteDetails");

            migrationBuilder.DropTable(
                name: "GarmentMDLocalSalesNoteItems");

            migrationBuilder.DropTable(
                name: "GarmentMDLocalSalesNotes");
        }
    }
}
