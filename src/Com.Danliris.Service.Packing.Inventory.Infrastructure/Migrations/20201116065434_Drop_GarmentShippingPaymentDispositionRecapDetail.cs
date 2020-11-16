using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Drop_GarmentShippingPaymentDispositionRecapDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingPaymentDispositionRecapDetails");

            migrationBuilder.AddColumn<double>(
                name: "Service",
                table: "GarmentShippingPaymentDispositionRecapItems",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Service",
                table: "GarmentShippingPaymentDispositionRecapItems");

            migrationBuilder.CreateTable(
                name: "GarmentShippingPaymentDispositionRecapDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAgent = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DeletedAgent = table.Column<string>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    InvoiceId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    PaymentDispositionInvoiceDetailId = table.Column<int>(nullable: false),
                    RecapItemId = table.Column<int>(nullable: false),
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
    }
}
