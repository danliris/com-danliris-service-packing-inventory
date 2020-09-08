using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Initial_VBPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingVBPayments",
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
                    VBNo = table.Column<string>(maxLength: 50, nullable: true),
                    VBDate = table.Column<DateTimeOffset>(nullable: false),
                    PaymentType = table.Column<string>(maxLength: 50, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 250, nullable: true),
                    EMKLId = table.Column<int>(nullable: false),
                    EMKLName = table.Column<string>(maxLength: 250, nullable: true),
                    EMKLCode = table.Column<string>(maxLength: 100, nullable: true),
                    ForwarderId = table.Column<int>(nullable: false),
                    ForwarderCode = table.Column<string>(nullable: true),
                    ForwarderName = table.Column<string>(nullable: true),
                    EMKLInvoiceNo = table.Column<string>(maxLength: 100, nullable: true),
                    ForwarderInvoiceNo = table.Column<string>(maxLength: 100, nullable: true),
                    BillValue = table.Column<double>(nullable: false),
                    VatValue = table.Column<double>(nullable: false),
                    PaymentDate = table.Column<DateTimeOffset>(nullable: false),
                    IncomeTaxId = table.Column<int>(nullable: false),
                    IncomeTaxName = table.Column<string>(maxLength: 255, nullable: true),
                    IncomeTaxRate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingVBPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingVBPaymentInvoices",
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
                    VBPaymentId = table.Column<int>(nullable: false),
                    InvoiceId = table.Column<int>(nullable: false),
                    InvoiceNo = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingVBPaymentInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingVBPaymentInvoices_GarmentShippingVBPayments_VBPaymentId",
                        column: x => x.VBPaymentId,
                        principalTable: "GarmentShippingVBPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingVBPaymentUnits",
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
                    VBPaymentId = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    UnitCode = table.Column<string>(maxLength: 128, nullable: true),
                    UnitName = table.Column<string>(maxLength: 128, nullable: true),
                    BillValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingVBPaymentUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingVBPaymentUnits_GarmentShippingVBPayments_VBPaymentId",
                        column: x => x.VBPaymentId,
                        principalTable: "GarmentShippingVBPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingVBPaymentInvoices_VBPaymentId",
                table: "GarmentShippingVBPaymentInvoices",
                column: "VBPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingVBPaymentUnits_VBPaymentId",
                table: "GarmentShippingVBPaymentUnits",
                column: "VBPaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingVBPaymentInvoices");

            migrationBuilder.DropTable(
                name: "GarmentShippingVBPaymentUnits");

            migrationBuilder.DropTable(
                name: "GarmentShippingVBPayments");
        }
    }
}
