using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class FixDyeingPrintingAreaMK3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaMovementHistories");

            migrationBuilder.DropIndex(
                name: "IX_DyeingPrintingAreaMovements_BonNo",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "BonNo",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "DeliveryOrderSalesId",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "DeliveryOrderSalesNo",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "MaterialCode",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionCode",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionId",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionName",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "MaterialWidth",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "MeterLength",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "Mutation",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "PackingInstruction",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "ProductionOrderCode",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "ProductionOrderQuantity",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "ProductionOrderType",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "QtyKg",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "Shift",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "UnitCode",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "YardsLength",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.RenameColumn(
                name: "UOMUnit",
                table: "DyeingPrintingAreaMovements",
                newName: "UomUnit");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "DyeingPrintingAreaMovements",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "SourceArea",
                table: "DyeingPrintingAreaMovements",
                newName: "DyeingPrintingAreaDocumentBonNo");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "DyeingPrintingAreaMovements",
                newName: "DyeingPrintingAreaDocumentId");

            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Area",
                table: "DyeingPrintingAreaMovements",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaInputs",
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
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Area = table.Column<string>(maxLength: 64, nullable: true),
                    Shift = table.Column<string>(maxLength: 64, nullable: true),
                    BonNo = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingAreaInputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaOutputs",
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
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Area = table.Column<string>(maxLength: 64, nullable: true),
                    Shift = table.Column<string>(maxLength: 64, nullable: true),
                    BonNo = table.Column<string>(maxLength: 64, nullable: true),
                    HasNextAreaDocument = table.Column<bool>(nullable: false),
                    DestinationArea = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingAreaOutputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaSummaries",
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
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Area = table.Column<string>(maxLength: 128, nullable: true),
                    Type = table.Column<string>(maxLength: 32, nullable: true),
                    DyeingPrintingAreaDocumentId = table.Column<int>(nullable: false),
                    DyeingPrintingAreaDocumentBonNo = table.Column<string>(maxLength: 64, nullable: true),
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 128, nullable: true),
                    CartNo = table.Column<string>(maxLength: 128, nullable: true),
                    Buyer = table.Column<string>(maxLength: 4096, nullable: true),
                    Construction = table.Column<string>(maxLength: 1024, nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    UnitName = table.Column<string>(maxLength: 4096, nullable: true),
                    Color = table.Column<string>(maxLength: 4096, nullable: true),
                    Motif = table.Column<string>(maxLength: 4096, nullable: true),
                    UomUnit = table.Column<string>(maxLength: 32, nullable: true),
                    Balance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingAreaSummaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaInputProductionOrders",
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
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 128, nullable: true),
                    CartNo = table.Column<string>(maxLength: 128, nullable: true),
                    Buyer = table.Column<string>(maxLength: 4096, nullable: true),
                    Construction = table.Column<string>(maxLength: 1024, nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    UnitName = table.Column<string>(maxLength: 4096, nullable: true),
                    Color = table.Column<string>(maxLength: 4096, nullable: true),
                    Motif = table.Column<string>(maxLength: 4096, nullable: true),
                    UomUnit = table.Column<string>(maxLength: 32, nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    HasOutputDocument = table.Column<bool>(nullable: false),
                    IsChecked = table.Column<bool>(nullable: false),
                    PackingInstruction = table.Column<string>(maxLength: 4096, nullable: true),
                    ProductionOrderType = table.Column<string>(maxLength: 512, nullable: true),
                    Remark = table.Column<string>(maxLength: 128, nullable: true),
                    Grade = table.Column<string>(maxLength: 128, nullable: true),
                    Status = table.Column<string>(maxLength: 128, nullable: true),
                    DyeingPrintingAreaInputId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingAreaInputProductionOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DyeingPrintingAreaInputProductionOrders_DyeingPrintingAreaInputs_DyeingPrintingAreaInputId",
                        column: x => x.DyeingPrintingAreaInputId,
                        principalTable: "DyeingPrintingAreaInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaOutputProductionOrders",
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
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 128, nullable: true),
                    CartNo = table.Column<string>(maxLength: 128, nullable: true),
                    Buyer = table.Column<string>(maxLength: 4096, nullable: true),
                    Construction = table.Column<string>(maxLength: 1024, nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    UnitName = table.Column<string>(maxLength: 4096, nullable: true),
                    Color = table.Column<string>(maxLength: 4096, nullable: true),
                    Motif = table.Column<string>(maxLength: 4096, nullable: true),
                    UomUnit = table.Column<string>(maxLength: 32, nullable: true),
                    Remark = table.Column<string>(maxLength: 128, nullable: true),
                    Grade = table.Column<string>(maxLength: 128, nullable: true),
                    Status = table.Column<string>(maxLength: 128, nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    PackingInstruction = table.Column<string>(maxLength: 4096, nullable: true),
                    ProductionOrderType = table.Column<string>(maxLength: 512, nullable: true),
                    DyeingPrintingAreaOutputId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingAreaOutputProductionOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DyeingPrintingAreaOutputProductionOrders_DyeingPrintingAreaOutputs_DyeingPrintingAreaOutputId",
                        column: x => x.DyeingPrintingAreaOutputId,
                        principalTable: "DyeingPrintingAreaOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaInputProductionOrders_DyeingPrintingAreaInputId",
                table: "DyeingPrintingAreaInputProductionOrders",
                column: "DyeingPrintingAreaInputId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaInputs_BonNo",
                table: "DyeingPrintingAreaInputs",
                column: "BonNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaOutputProductionOrders_DyeingPrintingAreaOutputId",
                table: "DyeingPrintingAreaOutputProductionOrders",
                column: "DyeingPrintingAreaOutputId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaOutputs_BonNo",
                table: "DyeingPrintingAreaOutputs",
                column: "BonNo",
                unique: true,
                filter: "[IsDeleted]=(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaInputProductionOrders");

            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaSummaries");

            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaInputs");

            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaOutputs");

            migrationBuilder.RenameColumn(
                name: "UomUnit",
                table: "DyeingPrintingAreaMovements",
                newName: "UOMUnit");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "DyeingPrintingAreaMovements",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "DyeingPrintingAreaDocumentId",
                table: "DyeingPrintingAreaMovements",
                newName: "MaterialId");

            migrationBuilder.RenameColumn(
                name: "DyeingPrintingAreaDocumentBonNo",
                table: "DyeingPrintingAreaMovements",
                newName: "SourceArea");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "DyeingPrintingAreaMovements",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "Area",
                table: "DyeingPrintingAreaMovements",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BonNo",
                table: "DyeingPrintingAreaMovements",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeliveryOrderSalesId",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryOrderSalesNo",
                table: "DyeingPrintingAreaMovements",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "DyeingPrintingAreaMovements",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MaterialCode",
                table: "DyeingPrintingAreaMovements",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialConstructionCode",
                table: "DyeingPrintingAreaMovements",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialConstructionId",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialConstructionName",
                table: "DyeingPrintingAreaMovements",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "DyeingPrintingAreaMovements",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialWidth",
                table: "DyeingPrintingAreaMovements",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MeterLength",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Mutation",
                table: "DyeingPrintingAreaMovements",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackingInstruction",
                table: "DyeingPrintingAreaMovements",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderCode",
                table: "DyeingPrintingAreaMovements",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProductionOrderQuantity",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderType",
                table: "DyeingPrintingAreaMovements",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "QtyKg",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "DyeingPrintingAreaMovements",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shift",
                table: "DyeingPrintingAreaMovements",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitCode",
                table: "DyeingPrintingAreaMovements",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "YardsLength",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaMovementHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Area = table.Column<string>(maxLength: 64, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DyeingPrintingAreaMovementId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    ProductionOrderQuantity = table.Column<double>(nullable: false),
                    QtyKg = table.Column<double>(nullable: false),
                    Shift = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingAreaMovementHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DyeingPrintingAreaMovementHistories_DyeingPrintingAreaMovements_DyeingPrintingAreaMovementId",
                        column: x => x.DyeingPrintingAreaMovementId,
                        principalTable: "DyeingPrintingAreaMovements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaMovements_BonNo",
                table: "DyeingPrintingAreaMovements",
                column: "BonNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaMovementHistories_DyeingPrintingAreaMovementId",
                table: "DyeingPrintingAreaMovementHistories",
                column: "DyeingPrintingAreaMovementId");
        }
    }
}
