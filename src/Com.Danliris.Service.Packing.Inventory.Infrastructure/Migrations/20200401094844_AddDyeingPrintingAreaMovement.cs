using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddDyeingPrintingAreaMovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaMovements",
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
                    Area = table.Column<string>(maxLength: 64, nullable: true),
                    BonNo = table.Column<string>(maxLength: 64, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Shift = table.Column<string>(maxLength: 64, nullable: true),
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderCode = table.Column<string>(maxLength: 32, nullable: true),
                    ProductionOrderNo = table.Column<string>(maxLength: 128, nullable: true),
                    CartNo = table.Column<string>(maxLength: 128, nullable: true),
                    Construction = table.Column<string>(maxLength: 1024, nullable: true),
                    MaterialId = table.Column<int>(nullable: false),
                    MaterialCode = table.Column<string>(maxLength: 32, nullable: true),
                    MaterialName = table.Column<string>(maxLength: 4096, nullable: true),
                    MaterialConstructionId = table.Column<int>(nullable: false),
                    MaterialConstructionCode = table.Column<string>(maxLength: 32, nullable: true),
                    MaterialConstructionName = table.Column<string>(maxLength: 4096, nullable: true),
                    MaterialWidth = table.Column<string>(maxLength: 1024, nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    UnitCode = table.Column<string>(maxLength: 1024, nullable: true),
                    UnitName = table.Column<string>(maxLength: 4096, nullable: true),
                    Color = table.Column<string>(maxLength: 4096, nullable: true),
                    Mutation = table.Column<string>(maxLength: 64, nullable: true),
                    MeterLength = table.Column<double>(nullable: false),
                    YardsLength = table.Column<double>(nullable: false),
                    UOMUnit = table.Column<string>(maxLength: 32, nullable: true),
                    Balance = table.Column<decimal>(nullable: false),
                    Status = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingAreaMovements", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaMovements");
        }
    }
}
