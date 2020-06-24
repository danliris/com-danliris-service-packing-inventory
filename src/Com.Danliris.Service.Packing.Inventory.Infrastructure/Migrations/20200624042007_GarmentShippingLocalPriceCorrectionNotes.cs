using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentShippingLocalPriceCorrectionNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingLocalPriceCorrectionNotes",
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
                    CorrectionNoteNo = table.Column<string>(maxLength: 50, nullable: true),
                    CorrectionDate = table.Column<DateTimeOffset>(nullable: false),
                    SalesNoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingLocalPriceCorrectionNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingLocalPriceCorrectionNotes_GarmentShippingLocalSalesNotes_SalesNoteId",
                        column: x => x.SalesNoteId,
                        principalTable: "GarmentShippingLocalSalesNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingLocalPriceCorrectionNoteItems",
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
                    PriceCorrectionNoteId = table.Column<int>(nullable: false),
                    SalesNoteItemId = table.Column<int>(nullable: false),
                    PriceCorrection = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingLocalPriceCorrectionNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingLocalPriceCorrectionNoteItems_GarmentShippingLocalPriceCorrectionNotes_PriceCorrectionNoteId",
                        column: x => x.PriceCorrectionNoteId,
                        principalTable: "GarmentShippingLocalPriceCorrectionNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalPriceCorrectionNoteItems_PriceCorrectionNoteId",
                table: "GarmentShippingLocalPriceCorrectionNoteItems",
                column: "PriceCorrectionNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalPriceCorrectionNotes_CorrectionNoteNo",
                table: "GarmentShippingLocalPriceCorrectionNotes",
                column: "CorrectionNoteNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalPriceCorrectionNotes_SalesNoteId",
                table: "GarmentShippingLocalPriceCorrectionNotes",
                column: "SalesNoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingLocalPriceCorrectionNoteItems");

            migrationBuilder.DropTable(
                name: "GarmentShippingLocalPriceCorrectionNotes");
        }
    }
}
