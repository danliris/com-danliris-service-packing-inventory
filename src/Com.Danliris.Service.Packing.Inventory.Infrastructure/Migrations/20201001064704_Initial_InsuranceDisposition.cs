using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Initial_InsuranceDisposition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingInsuranceDispositions",
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
                    DispositionNo = table.Column<string>(maxLength: 50, nullable: true),
                    PolicyType = table.Column<string>(maxLength: 25, nullable: true),
                    PaymentDate = table.Column<DateTimeOffset>(nullable: false),
                    BankName = table.Column<string>(maxLength: 255, nullable: true),
                    InsuranceId = table.Column<int>(nullable: false),
                    InsuranceName = table.Column<string>(maxLength: 255, nullable: true),
                    InsuranceCode = table.Column<string>(maxLength: 50, nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    Remark = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingInsuranceDispositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingInsuranceDispositionItems",
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
                    InsuranceDispositionId = table.Column<int>(nullable: false),
                    PolicyDate = table.Column<DateTimeOffset>(nullable: false),
                    PolicyNo = table.Column<string>(maxLength: 255, nullable: true),
                    InvoiceNo = table.Column<string>(maxLength: 50, nullable: true),
                    InvoiceId = table.Column<int>(nullable: false),
                    BuyerAgentId = table.Column<int>(nullable: false),
                    BuyerAgentCode = table.Column<string>(maxLength: 10, nullable: true),
                    BuyerAgentName = table.Column<string>(maxLength: 255, nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    CurrencyRate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingInsuranceDispositionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingInsuranceDispositionItems_GarmentShippingInsuranceDispositions_InsuranceDispositionId",
                        column: x => x.InsuranceDispositionId,
                        principalTable: "GarmentShippingInsuranceDispositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingInsuranceDispositionUnitCharges",
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
                    InsuranceDispositionId = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    UnitCode = table.Column<string>(maxLength: 10, nullable: true),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingInsuranceDispositionUnitCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingInsuranceDispositionUnitCharges_GarmentShippingInsuranceDispositions_InsuranceDispositionId",
                        column: x => x.InsuranceDispositionId,
                        principalTable: "GarmentShippingInsuranceDispositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingInsuranceDispositionItems_InsuranceDispositionId",
                table: "GarmentShippingInsuranceDispositionItems",
                column: "InsuranceDispositionId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingInsuranceDispositions_DispositionNo",
                table: "GarmentShippingInsuranceDispositions",
                column: "DispositionNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingInsuranceDispositionUnitCharges_InsuranceDispositionId",
                table: "GarmentShippingInsuranceDispositionUnitCharges",
                column: "InsuranceDispositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingInsuranceDispositionItems");

            migrationBuilder.DropTable(
                name: "GarmentShippingInsuranceDispositionUnitCharges");

            migrationBuilder.DropTable(
                name: "GarmentShippingInsuranceDispositions");
        }
    }
}
