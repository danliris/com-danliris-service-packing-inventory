using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class MaterialDeliveryNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialDeliveryNoteWeaving",
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
                    Code = table.Column<string>(maxLength: 128, nullable: true),
                    DateSJ = table.Column<DateTimeOffset>(maxLength: 128, nullable: true),
                    DoSalesNumberId = table.Column<long>(maxLength: 128, nullable: false),
                    DoSalesNumber = table.Column<string>(maxLength: 128, nullable: true),
                    SendTo = table.Column<string>(maxLength: 128, nullable: true),
                    UnitId = table.Column<int>(maxLength: 128, nullable: false),
                    UnitName = table.Column<string>(maxLength: 128, nullable: true),
                    BuyerId = table.Column<int>(maxLength: 128, nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 128, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 128, nullable: true),
                    NumberOut = table.Column<string>(maxLength: 128, nullable: true),
                    StorageId = table.Column<int>(maxLength: 128, nullable: true),
                    StorageCode = table.Column<string>(maxLength: 128, nullable: true),
                    StorageName = table.Column<string>(maxLength: 128, nullable: true),
                    UnitLength = table.Column<string>(maxLength: 128, nullable: true),
                    UnitPacking = table.Column<string>(maxLength: 128, nullable: true),
                    Remark = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialDeliveryNoteWeaving", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemsMaterialDeliveryNoteWeaving",
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
                    ItemNoSPP = table.Column<string>(maxLength: 128, nullable: true),
                    ItemMaterialName = table.Column<string>(maxLength: 128, nullable: true),
                    ItemDesign = table.Column<string>(maxLength: 128, nullable: true),
                    ItemType = table.Column<string>(maxLength: 128, nullable: true),
                    ItemCode = table.Column<string>(maxLength: 128, nullable: true),
                    InputPacking = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InputConversion = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaterialDeliveryNoteWeavingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsMaterialDeliveryNoteWeaving", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsMaterialDeliveryNoteWeaving_MaterialDeliveryNoteWeaving_MaterialDeliveryNoteWeavingId",
                        column: x => x.MaterialDeliveryNoteWeavingId,
                        principalTable: "MaterialDeliveryNoteWeaving",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemsMaterialDeliveryNoteWeaving_MaterialDeliveryNoteWeavingId",
                table: "ItemsMaterialDeliveryNoteWeaving",
                column: "MaterialDeliveryNoteWeavingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsMaterialDeliveryNoteWeaving");

            migrationBuilder.DropTable(
                name: "MaterialDeliveryNoteWeaving");
        }
    }
}
