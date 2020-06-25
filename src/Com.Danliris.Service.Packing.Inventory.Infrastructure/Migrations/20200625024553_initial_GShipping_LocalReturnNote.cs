using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class initial_GShipping_LocalReturnNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingLocalReturnNotes",
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
                    ReturnNoteNo = table.Column<string>(nullable: true),
                    SalesNoteId = table.Column<int>(nullable: false),
                    ReturnDate = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingLocalReturnNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingLocalReturnNotes_GarmentShippingLocalSalesNotes_SalesNoteId",
                        column: x => x.SalesNoteId,
                        principalTable: "GarmentShippingLocalSalesNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentShippingLocalReturnNoteItems",
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
                    ReturnNoteId = table.Column<int>(nullable: false),
                    SalesNoteItemId = table.Column<int>(nullable: false),
                    ReturnQuantity = table.Column<double>(nullable: false),
                    GarmentShippingLocalReturnNoteModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingLocalReturnNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingLocalReturnNoteItems_GarmentShippingLocalReturnNotes_GarmentShippingLocalReturnNoteModelId",
                        column: x => x.GarmentShippingLocalReturnNoteModelId,
                        principalTable: "GarmentShippingLocalReturnNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GarmentShippingLocalReturnNoteItems_GarmentShippingLocalSalesNoteItems_SalesNoteItemId",
                        column: x => x.SalesNoteItemId,
                        principalTable: "GarmentShippingLocalSalesNoteItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalReturnNoteItems_GarmentShippingLocalReturnNoteModelId",
                table: "GarmentShippingLocalReturnNoteItems",
                column: "GarmentShippingLocalReturnNoteModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalReturnNoteItems_SalesNoteItemId",
                table: "GarmentShippingLocalReturnNoteItems",
                column: "SalesNoteItemId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLocalReturnNotes_SalesNoteId",
                table: "GarmentShippingLocalReturnNotes",
                column: "SalesNoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingLocalReturnNoteItems");

            migrationBuilder.DropTable(
                name: "GarmentShippingLocalReturnNotes");
        }
    }
}
