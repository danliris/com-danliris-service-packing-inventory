using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddFabricQualityControl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "DyeingPrintingAreaMovements",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedAgent",
                table: "DyeingPrintingAreaMovements",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "DyeingPrintingAreaMovements",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAgent",
                table: "DyeingPrintingAreaMovements",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "DyeingPrintingAreaMovements",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAgent",
                table: "DyeingPrintingAreaMovements",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Buyer",
                table: "DyeingPrintingAreaMovements",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackingInstruction",
                table: "DyeingPrintingAreaMovements",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderType",
                table: "DyeingPrintingAreaMovements",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NewFabricQualityControls",
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
                    UId = table.Column<string>(maxLength: 256, nullable: true),
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    DateIm = table.Column<DateTimeOffset>(nullable: false),
                    Group = table.Column<string>(maxLength: 4096, nullable: true),
                    IsUsed = table.Column<bool>(nullable: false),
                    DyeingPrintingAreaMovementId = table.Column<int>(nullable: false),
                    DyeingPrintingAreaMovementBonNo = table.Column<string>(maxLength: 64, nullable: true),
                    ProductionOrderNo = table.Column<string>(maxLength: 128, nullable: true),
                    MachineNoIm = table.Column<string>(maxLength: 4096, nullable: true),
                    OperatorIm = table.Column<string>(maxLength: 4096, nullable: true),
                    PointLimit = table.Column<double>(nullable: false),
                    PointSystem = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewFabricQualityControls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewFabricGradeTests",
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
                    AvalLength = table.Column<double>(nullable: false),
                    FabricGradeTest = table.Column<double>(nullable: false),
                    FinalArea = table.Column<double>(nullable: false),
                    FinalGradeTest = table.Column<double>(nullable: false),
                    FinalLength = table.Column<double>(nullable: false),
                    FinalScore = table.Column<double>(nullable: false),
                    Grade = table.Column<string>(maxLength: 512, nullable: true),
                    InitLength = table.Column<double>(nullable: false),
                    PcsNo = table.Column<string>(maxLength: 4096, nullable: true),
                    PointLimit = table.Column<double>(nullable: false),
                    PointSystem = table.Column<double>(nullable: false),
                    SampleLength = table.Column<double>(nullable: false),
                    Score = table.Column<double>(nullable: false),
                    Type = table.Column<string>(maxLength: 1024, nullable: true),
                    Width = table.Column<double>(nullable: false),
                    ItemIndex = table.Column<int>(nullable: false),
                    FabricQualityControlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewFabricGradeTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewFabricGradeTests_NewFabricQualityControls_FabricQualityControlId",
                        column: x => x.FabricQualityControlId,
                        principalTable: "NewFabricQualityControls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewCriterias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    Group = table.Column<string>(maxLength: 4096, nullable: true),
                    Index = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 4096, nullable: true),
                    ScoreA = table.Column<double>(nullable: false),
                    ScoreB = table.Column<double>(nullable: false),
                    ScoreC = table.Column<double>(nullable: false),
                    ScoreD = table.Column<double>(nullable: false),
                    FabricGradeTestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewCriterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewCriterias_NewFabricGradeTests_FabricGradeTestId",
                        column: x => x.FabricGradeTestId,
                        principalTable: "NewFabricGradeTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewCriterias_FabricGradeTestId",
                table: "NewCriterias",
                column: "FabricGradeTestId");

            migrationBuilder.CreateIndex(
                name: "IX_NewFabricGradeTests_FabricQualityControlId",
                table: "NewFabricGradeTests",
                column: "FabricQualityControlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewCriterias");

            migrationBuilder.DropTable(
                name: "NewFabricGradeTests");

            migrationBuilder.DropTable(
                name: "NewFabricQualityControls");

            migrationBuilder.DropColumn(
                name: "Buyer",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "PackingInstruction",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.DropColumn(
                name: "ProductionOrderType",
                table: "DyeingPrintingAreaMovements");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "DyeingPrintingAreaMovements",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedAgent",
                table: "DyeingPrintingAreaMovements",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "DyeingPrintingAreaMovements",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAgent",
                table: "DyeingPrintingAreaMovements",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "DyeingPrintingAreaMovements",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAgent",
                table: "DyeingPrintingAreaMovements",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);
        }
    }
}
