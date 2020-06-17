using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentShippingLocalSalesNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingLocalSalesNotes",
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
                    NoteNo = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    TransactionTypeId = table.Column<int>(nullable: false),
                    TransactionTypeCode = table.Column<string>(maxLength: 100, nullable: true),
                    TransactionTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerNPWP = table.Column<string>(maxLength: 50, nullable: true),
                    Tempo = table.Column<int>(nullable: false),
                    DispositionNo = table.Column<string>(maxLength: 100, nullable: true),
                    UseVat = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingLocalSalesNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingLocalSalesNoteItems",
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
                    LocalSalesNoteId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ProductCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductName = table.Column<string>(maxLength: 250, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 250, nullable: true),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingLocalSalesNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingLocalSalesNoteItems_GarmentShippingLocalSalesNotes_LocalSalesNoteId",
                        column: x => x.LocalSalesNoteId,
                        principalTable: "GarmentShippingLocalSalesNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalSalesNoteItems_LocalSalesNoteId",
                table: "GarmentShippingLocalSalesNoteItems",
                column: "LocalSalesNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalSalesNotes_NoteNo",
                table: "GarmentShippingLocalSalesNotes",
                column: "NoteNo",
                unique: true,
                filter: "[IsDeleted]=(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingLocalSalesNoteItems");

            migrationBuilder.DropTable(
                name: "GarmentShippingLocalSalesNotes");
        }
    }
}
