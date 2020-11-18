using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentShippingPaymentDispositionRecaps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingPaymentDispositionRecaps",
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
                    RecapNo = table.Column<string>(maxLength: 100, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    EmklId = table.Column<int>(nullable: false),
                    EMKLCode = table.Column<string>(maxLength: 50, nullable: true),
                    EMKLName = table.Column<string>(maxLength: 255, nullable: true),
                    EMKLAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    EMKLNPWP = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingPaymentDispositionRecaps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingPaymentDispositionRecapItems",
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
                    RecapId = table.Column<int>(nullable: false),
                    PaymentDispositionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingPaymentDispositionRecapItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingPaymentDispositionRecapItems_GarmentShippingPaymentDispositionRecaps_RecapId",
                        column: x => x.RecapId,
                        principalTable: "GarmentShippingPaymentDispositionRecaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingPaymentDispositionRecapItems_RecapId",
                table: "GarmentShippingPaymentDispositionRecapItems",
                column: "RecapId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingPaymentDispositionRecaps_RecapNo",
                table: "GarmentShippingPaymentDispositionRecaps",
                column: "RecapNo",
                unique: true,
                filter: "[IsDeleted]=(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingPaymentDispositionRecapItems");

            migrationBuilder.DropTable(
                name: "GarmentShippingPaymentDispositionRecaps");
        }
    }
}
