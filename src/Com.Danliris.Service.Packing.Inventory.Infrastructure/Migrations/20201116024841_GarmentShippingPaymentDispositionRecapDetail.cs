using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentShippingPaymentDispositionRecapDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingPaymentDispositionRecapDetails",
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
                    RecapItemId = table.Column<int>(nullable: false),
                    PaymentDispositionInvoiceDetailId = table.Column<int>(nullable: false),
                    InvoiceId = table.Column<int>(nullable: false),
                    Service = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingPaymentDispositionRecapDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingPaymentDispositionRecapDetails_GarmentShippingPaymentDispositionRecapItems_RecapItemId",
                        column: x => x.RecapItemId,
                        principalTable: "GarmentShippingPaymentDispositionRecapItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingPaymentDispositionRecapDetails_RecapItemId",
                table: "GarmentShippingPaymentDispositionRecapDetails",
                column: "RecapItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingPaymentDispositionRecapDetails");
        }
    }
}
