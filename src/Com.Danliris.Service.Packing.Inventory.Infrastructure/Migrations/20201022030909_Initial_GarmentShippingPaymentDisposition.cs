using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Initial_GarmentShippingPaymentDisposition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingPaymentDispositions",
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
                    DispositionNo = table.Column<string>(nullable: true),
                    PaymentType = table.Column<string>(maxLength: 50, nullable: true),
                    PaymentMethod = table.Column<string>(maxLength: 50, nullable: true),
                    PaidAt = table.Column<string>(maxLength: 50, nullable: true),
                    SendBy = table.Column<string>(nullable: true),
                    BuyerAgentId = table.Column<int>(nullable: false),
                    BuyerAgentCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerAgentName = table.Column<string>(maxLength: 255, nullable: true),
                    PaymentTerm = table.Column<string>(maxLength: 50, nullable: true),
                    ForwarderId = table.Column<int>(nullable: false),
                    ForwarderCode = table.Column<string>(maxLength: 50, nullable: true),
                    ForwarderName = table.Column<string>(maxLength: 255, nullable: true),
                    CourierId = table.Column<int>(nullable: false),
                    CourierCode = table.Column<string>(maxLength: 50, nullable: true),
                    CourierName = table.Column<string>(maxLength: 255, nullable: true),
                    EMKLId = table.Column<int>(nullable: false),
                    EMKLCode = table.Column<string>(maxLength: 50, nullable: true),
                    EMKLName = table.Column<string>(maxLength: 255, nullable: true),
                    Address = table.Column<string>(maxLength: 4000, nullable: true),
                    NPWP = table.Column<string>(maxLength: 100, nullable: true),
                    InvoiceNumber = table.Column<string>(maxLength: 100, nullable: true),
                    InvoiceDate = table.Column<DateTimeOffset>(nullable: false),
                    InvoiceTaxNumber = table.Column<string>(maxLength: 100, nullable: true),
                    BillValue = table.Column<decimal>(nullable: false),
                    VatValue = table.Column<decimal>(nullable: false),
                    IncomeTaxId = table.Column<int>(nullable: false),
                    IncomeTaxName = table.Column<string>(maxLength: 255, nullable: true),
                    IncomeTaxRate = table.Column<decimal>(nullable: false),
                    IncomeTaxValue = table.Column<decimal>(nullable: false),
                    TotalBill = table.Column<decimal>(nullable: false),
                    PaymentDate = table.Column<DateTimeOffset>(nullable: false),
                    Bank = table.Column<string>(maxLength: 255, nullable: true),
                    AccNo = table.Column<string>(maxLength: 100, nullable: true),
                    IsFreightCharged = table.Column<bool>(nullable: false),
                    FreightBy = table.Column<string>(maxLength: 50, nullable: true),
                    FreightNo = table.Column<string>(maxLength: 255, nullable: true),
                    FreightDate = table.Column<DateTimeOffset>(nullable: false),
                    Remark = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingPaymentDispositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingPaymentDispositionBillDetails",
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
                    PaymentDispositionId = table.Column<int>(nullable: false),
                    BillDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingPaymentDispositionBillDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingPaymentDispositionBillDetails_GarmentShippingPaymentDispositions_PaymentDispositionId",
                        column: x => x.PaymentDispositionId,
                        principalTable: "GarmentShippingPaymentDispositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingPaymentDispositionInvoiceDetails",
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
                    PaymentDispositionId = table.Column<int>(nullable: false),
                    InvoiceNo = table.Column<string>(maxLength: 50, nullable: true),
                    InvoiceId = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Volume = table.Column<decimal>(nullable: false),
                    GrossWeight = table.Column<decimal>(nullable: false),
                    ChargeableWeight = table.Column<decimal>(nullable: false),
                    TotalCarton = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingPaymentDispositionInvoiceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingPaymentDispositionInvoiceDetails_GarmentShippingPaymentDispositions_PaymentDispositionId",
                        column: x => x.PaymentDispositionId,
                        principalTable: "GarmentShippingPaymentDispositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingPaymentDispositionUnitCharges",
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
                    PaymentDispositionId = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    UnitCode = table.Column<string>(maxLength: 20, nullable: true),
                    AmountPercentage = table.Column<decimal>(nullable: false),
                    BillAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingPaymentDispositionUnitCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingPaymentDispositionUnitCharges_GarmentShippingPaymentDispositions_PaymentDispositionId",
                        column: x => x.PaymentDispositionId,
                        principalTable: "GarmentShippingPaymentDispositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingPaymentDispositionBillDetails_PaymentDispositionId",
                table: "GarmentShippingPaymentDispositionBillDetails",
                column: "PaymentDispositionId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingPaymentDispositionInvoiceDetails_PaymentDispositionId",
                table: "GarmentShippingPaymentDispositionInvoiceDetails",
                column: "PaymentDispositionId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingPaymentDispositionUnitCharges_PaymentDispositionId",
                table: "GarmentShippingPaymentDispositionUnitCharges",
                column: "PaymentDispositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingPaymentDispositionBillDetails");

            migrationBuilder.DropTable(
                name: "GarmentShippingPaymentDispositionInvoiceDetails");

            migrationBuilder.DropTable(
                name: "GarmentShippingPaymentDispositionUnitCharges");

            migrationBuilder.DropTable(
                name: "GarmentShippingPaymentDispositions");
        }
    }
}
