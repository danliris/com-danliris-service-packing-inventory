using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddPropertiesOnDyeingPrintingArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityKg",
                table: "DyeingPrintingAreaInputProductionOrders",
                newName: "AvalQuantityKg");

            migrationBuilder.AddColumn<string>(
                name: "AvalCartNo",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AvalQuantityKg",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "AvalType",
                table: "DyeingPrintingAreaOutputProductionOrders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PackagingStock",
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
                    DyeingPrintingProductionOrderId = table.Column<int>(maxLength: 128, nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 128, nullable: true),
                    PackagingType = table.Column<string>(maxLength: 128, nullable: true),
                    PackagingUnit = table.Column<string>(maxLength: 128, nullable: true),
                    PackagingQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UomUnit = table.Column<string>(maxLength: 32, nullable: true),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HasNextArea = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackagingStock", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackagingStock");

            migrationBuilder.DropColumn(
                name: "AvalCartNo",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "AvalQuantityKg",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.DropColumn(
                name: "AvalType",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.RenameColumn(
                name: "AvalQuantityKg",
                table: "DyeingPrintingAreaInputProductionOrders",
                newName: "QuantityKg");
        }
    }
}
