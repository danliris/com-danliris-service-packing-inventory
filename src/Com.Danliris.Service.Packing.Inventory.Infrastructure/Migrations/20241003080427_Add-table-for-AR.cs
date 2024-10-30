using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddtableforAR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AR_Balances",
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
                    TruckingDate = table.Column<DateTime>(nullable: false),
                    BuyerAgentCode = table.Column<string>(maxLength: 10, nullable: true),
                    Destination = table.Column<string>(maxLength: 50, nullable: true),
                    InvoiceNo = table.Column<string>(maxLength: 30, nullable: true),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    PEBNo = table.Column<string>(maxLength: 10, nullable: true),
                    PEBDate = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    UOMUnit = table.Column<string>(maxLength: 8, nullable: true),
                    CurrencyCode = table.Column<string>(maxLength: 10, nullable: true),
                    Rate = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    AmountIDR = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AR_Balances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AR_CashInBank",
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
                    ReceiptNo = table.Column<string>(maxLength: 30, nullable: true),
                    ReceiptDate = table.Column<DateTime>(nullable: true),
                    BuyerCode = table.Column<string>(maxLength: 10, nullable: true),
                    ReceiptAmount = table.Column<double>(nullable: false),
                    ReceiptKurs = table.Column<double>(nullable: false),
                    ReceiptTotalAmount = table.Column<double>(nullable: false),
                    InvoiceNo = table.Column<string>(maxLength: 64, nullable: true),
                    LiquidAmount = table.Column<double>(nullable: false),
                    LiquidTotalAmount = table.Column<double>(nullable: false),
                    BookBalanceKurs = table.Column<double>(nullable: false),
                    BookBalanceTotalAmount = table.Column<double>(nullable: false),
                    DifferenceKurs = table.Column<double>(nullable: false),
                    COA = table.Column<string>(maxLength: 10, nullable: true),
                    UnitCode = table.Column<string>(maxLength: 10, nullable: true),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    SupportingDocument = table.Column<string>(maxLength: 64, nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    TotalAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AR_CashInBank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AR_CMTs",
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
                    InvoiceNo = table.Column<string>(maxLength: 64, nullable: true),
                    TruckingDate = table.Column<DateTime>(nullable: false),
                    PEBDate = table.Column<DateTime>(nullable: false),
                    ExpenditureGoodNo = table.Column<string>(maxLength: 64, nullable: true),
                    RONo = table.Column<string>(maxLength: 20, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Kurs = table.Column<double>(nullable: false),
                    TotalAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AR_CMTs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AR_DownPayments",
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
                    MemoNo = table.Column<string>(maxLength: 30, nullable: true),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    ReceiptNo = table.Column<string>(maxLength: 30, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    OffsetDate = table.Column<DateTime>(nullable: false),
                    InvoiceNo = table.Column<string>(maxLength: 64, nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    Kurs = table.Column<double>(nullable: false),
                    TotalAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AR_DownPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AR_OmzetCorrections",
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
                    MemoNo = table.Column<string>(maxLength: 30, nullable: true),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    BuyerCode = table.Column<string>(maxLength: 10, nullable: true),
                    InvoiceNo = table.Column<string>(maxLength: 64, nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    Kurs = table.Column<double>(nullable: false),
                    TotalAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AR_OmzetCorrections", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AR_Balances");

            migrationBuilder.DropTable(
                name: "AR_CashInBank");

            migrationBuilder.DropTable(
                name: "AR_CMTs");

            migrationBuilder.DropTable(
                name: "AR_DownPayments");

            migrationBuilder.DropTable(
                name: "AR_OmzetCorrections");
        }
    }
}
