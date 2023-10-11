using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class CreateGarmentShippingNoteCreditAdvice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AmountCA",
                table: "GarmentShippingNotes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "GarmentShippingNoteCreditAdvices",
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
                    ShippingNoteId = table.Column<int>(nullable: false),
                    NoteType = table.Column<string>(maxLength: 50, nullable: true),
                    NoteNo = table.Column<string>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    ReceiptNo = table.Column<string>(nullable: true),
                    PaymentTerm = table.Column<string>(maxLength: 25, nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    PaidAmount = table.Column<double>(nullable: false),
                    BalanceAmount = table.Column<double>(nullable: false),
                    PaymentDate = table.Column<DateTimeOffset>(nullable: false),
                    NettNego = table.Column<double>(nullable: false),
                    BankComission = table.Column<double>(nullable: false),
                    CreditInterest = table.Column<double>(nullable: false),
                    BankCharges = table.Column<double>(nullable: false),
                    InsuranceCharge = table.Column<double>(nullable: false),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(nullable: true),
                    BuyerName = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    BankAccountId = table.Column<int>(nullable: false),
                    BankAccountName = table.Column<string>(maxLength: 255, nullable: true),
                    BankAccountNo = table.Column<string>(nullable: true),
                    BankAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    DocumentSendDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingNoteCreditAdvices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingNoteCreditAdvices");

            migrationBuilder.DropColumn(
                name: "AmountCA",
                table: "GarmentShippingNotes");
        }
    }
}
