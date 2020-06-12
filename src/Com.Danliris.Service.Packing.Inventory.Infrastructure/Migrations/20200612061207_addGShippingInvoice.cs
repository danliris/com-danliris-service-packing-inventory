using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class addGShippingInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingInvoices",
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
                    InvoiceNo = table.Column<string>(maxLength: 50, nullable: true),
                    InvoiceDate = table.Column<DateTimeOffset>(nullable: false),
                    From = table.Column<string>(maxLength: 255, nullable: true),
                    To = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerAgentId = table.Column<int>(nullable: false),
                    BuyerAgentCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerAgentName = table.Column<string>(maxLength: 255, nullable: true),
                    Consignee = table.Column<string>(maxLength: 255, nullable: true),
                    LCNo = table.Column<string>(maxLength: 100, nullable: true),
                    IssuedBy = table.Column<string>(maxLength: 100, nullable: true),
                    SectionId = table.Column<int>(nullable: false),
                    SectionCode = table.Column<string>(maxLength: 100, nullable: true),
                    ShippingPer = table.Column<string>(maxLength: 256, nullable: true),
                    SailingDate = table.Column<DateTimeOffset>(nullable: false),
                    ConfirmationOfOrderNo = table.Column<string>(maxLength: 255, nullable: true),
                    ShippingStaffId = table.Column<int>(nullable: false),
                    ShippingStaff = table.Column<string>(maxLength: 255, nullable: true),
                    FabricTypeId = table.Column<int>(nullable: false),
                    FabricType = table.Column<string>(maxLength: 100, nullable: true),
                    BankAccountId = table.Column<int>(nullable: false),
                    BankAccount = table.Column<string>(maxLength: 50, nullable: true),
                    PaymentDue = table.Column<int>(maxLength: 5, nullable: false),
                    PEBNo = table.Column<string>(maxLength: 50, nullable: true),
                    PEBDate = table.Column<DateTimeOffset>(nullable: false),
                    NPENo = table.Column<string>(maxLength: 50, nullable: true),
                    NPEDate = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    AmountToBePaid = table.Column<decimal>(nullable: false),
                    CPrice = table.Column<decimal>(nullable: false),
                    Say = table.Column<string>(nullable: true),
                    Memo = table.Column<string>(nullable: true),
                    IsUsed = table.Column<bool>(nullable: false),
                    BL = table.Column<string>(maxLength: 50, nullable: true),
                    BLDate = table.Column<DateTimeOffset>(nullable: false),
                    CO = table.Column<string>(maxLength: 50, nullable: true),
                    CODate = table.Column<DateTimeOffset>(nullable: false),
                    COTP = table.Column<string>(maxLength: 50, nullable: true),
                    COTPDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingInvoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingInvoiceAdjustments",
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
                    GarmentShippingInvoiceId = table.Column<int>(nullable: false),
                    AdjustmentDescription = table.Column<string>(nullable: true),
                    AdjustmentValue = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingInvoiceAdjustments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingInvoiceAdjustments_GarmentShippingInvoices_GarmentShippingInvoiceId",
                        column: x => x.GarmentShippingInvoiceId,
                        principalTable: "GarmentShippingInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingInvoiceItems",
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
                    RONo = table.Column<string>(nullable: true),
                    SCNo = table.Column<string>(nullable: true),
                    BuyerBrandId = table.Column<int>(nullable: false),
                    BuyerBrandName = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    ComodityId = table.Column<int>(nullable: false),
                    ComodityCode = table.Column<string>(nullable: true),
                    ComodityName = table.Column<string>(nullable: true),
                    ComodityDesc = table.Column<string>(nullable: true),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    PriceRO = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CurrencyCode = table.Column<string>(nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    UnitCode = table.Column<string>(nullable: true),
                    CMTPrice = table.Column<decimal>(nullable: false),
                    GarmentShippingInvoiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingInvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingInvoiceItems_GarmentShippingInvoices_GarmentShippingInvoiceId",
                        column: x => x.GarmentShippingInvoiceId,
                        principalTable: "GarmentShippingInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingInvoiceAdjustments_GarmentShippingInvoiceId",
                table: "GarmentShippingInvoiceAdjustments",
                column: "GarmentShippingInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingInvoiceItems_GarmentShippingInvoiceId",
                table: "GarmentShippingInvoiceItems",
                column: "GarmentShippingInvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingInvoiceAdjustments");

            migrationBuilder.DropTable(
                name: "GarmentShippingInvoiceItems");

            migrationBuilder.DropTable(
                name: "GarmentShippingInvoices");
        }
    }
}
