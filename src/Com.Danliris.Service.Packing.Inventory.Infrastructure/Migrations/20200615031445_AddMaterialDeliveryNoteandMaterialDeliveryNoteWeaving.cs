using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddMaterialDeliveryNoteandMaterialDeliveryNoteWeaving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialDeliveryNote",
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
                    Code = table.Column<string>(nullable: true),
                    DateSJ = table.Column<DateTimeOffset>(nullable: true),
                    BonCode = table.Column<string>(nullable: true),
                    DateFrom = table.Column<DateTimeOffset>(nullable: true),
                    DateTo = table.Column<DateTimeOffset>(nullable: true),
                    DONumber = table.Column<string>(nullable: true),
                    FONumber = table.Column<string>(nullable: true),
                    Receiver = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    SCNumber = table.Column<string>(nullable: true),
                    Sender = table.Column<string>(nullable: true),
                    StorageNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialDeliveryNote", x => x.Id);
                });

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
                    DateSJ = table.Column<DateTimeOffset>(maxLength: 128, nullable: false),
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
                    Remark = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialDeliveryNoteWeaving", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
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
                    NoSPP = table.Column<string>(nullable: true),
                    MaterialName = table.Column<string>(nullable: true),
                    InputLot = table.Column<string>(nullable: true),
                    WeightBruto = table.Column<double>(nullable: true),
                    WeightDOS = table.Column<double>(nullable: true),
                    WeightCone = table.Column<double>(nullable: true),
                    WeightBale = table.Column<double>(nullable: true),
                    GetTotal = table.Column<double>(nullable: true),
                    MaterialDeliveryNoteModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_MaterialDeliveryNote_MaterialDeliveryNoteModelId",
                        column: x => x.MaterialDeliveryNoteModelId,
                        principalTable: "MaterialDeliveryNote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ItemNoSOP = table.Column<string>(maxLength: 128, nullable: true),
                    ItemMaterialName = table.Column<string>(maxLength: 128, nullable: true),
                    ItemGrade = table.Column<string>(maxLength: 128, nullable: true),
                    ItemType = table.Column<string>(maxLength: 128, nullable: true),
                    ItemCode = table.Column<string>(maxLength: 128, nullable: true),
                    InputBale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InputPiece = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InputMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InputKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                name: "IX_Items_MaterialDeliveryNoteModelId",
                table: "Items",
                column: "MaterialDeliveryNoteModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsMaterialDeliveryNoteWeaving_MaterialDeliveryNoteWeavingId",
                table: "ItemsMaterialDeliveryNoteWeaving",
                column: "MaterialDeliveryNoteWeavingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ItemsMaterialDeliveryNoteWeaving");

            migrationBuilder.DropTable(
                name: "MaterialDeliveryNote");

            migrationBuilder.DropTable(
                name: "MaterialDeliveryNoteWeaving");
        }
    }
}
