using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_GarmentShippingCreditAdvices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingCreditAdvices",
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
                    PackingListId = table.Column<int>(nullable: false),
                    InvoiceId = table.Column<int>(nullable: false),
                    InvoiceNo = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    AmountToBePaid = table.Column<double>(nullable: false),
                    Valas = table.Column<bool>(nullable: false),
                    LCType = table.Column<string>(maxLength: 25, nullable: true),
                    Inkaso = table.Column<double>(nullable: false),
                    Disconto = table.Column<double>(nullable: false),
                    SRNo = table.Column<string>(maxLength: 250, nullable: true),
                    NegoDate = table.Column<DateTimeOffset>(nullable: false),
                    Condition = table.Column<string>(maxLength: 25, nullable: true),
                    BankComission = table.Column<double>(nullable: false),
                    DiscrepancyFee = table.Column<double>(nullable: false),
                    NettNego = table.Column<double>(nullable: false),
                    BTBCADate = table.Column<DateTimeOffset>(nullable: false),
                    BTBAmount = table.Column<double>(nullable: false),
                    BTBRatio = table.Column<double>(nullable: false),
                    BTBRate = table.Column<double>(nullable: false),
                    BTBTransfer = table.Column<double>(nullable: false),
                    BTBMaterial = table.Column<double>(nullable: false),
                    BillDays = table.Column<double>(nullable: false),
                    BillAmount = table.Column<double>(nullable: false),
                    BillCA = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerName = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    BankAccountId = table.Column<int>(nullable: false),
                    BankAccountName = table.Column<string>(maxLength: 255, nullable: true),
                    BankAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    CreditInterest = table.Column<double>(nullable: false),
                    BankCharges = table.Column<double>(nullable: false),
                    DocumentPresente = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingCreditAdvices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingCreditAdvices");
        }
    }
}
