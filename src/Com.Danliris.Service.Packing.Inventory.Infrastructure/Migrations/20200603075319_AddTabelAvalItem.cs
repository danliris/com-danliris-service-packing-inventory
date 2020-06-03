using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddTabelAvalItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaOutputAvalItems",
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
                    Type = table.Column<string>(nullable: true),
                    Length = table.Column<double>(nullable: false),
                    DyeingPrintingAreaOutputProductionOrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeingPrintingAreaOutputAvalItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DyeingPrintingAreaOutputAvalItems_DyeingPrintingAreaOutputProductionOrders_DyeingPrintingAreaOutputProductionOrderId",
                        column: x => x.DyeingPrintingAreaOutputProductionOrderId,
                        principalTable: "DyeingPrintingAreaOutputProductionOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaOutputAvalItems_DyeingPrintingAreaOutputProductionOrderId",
                table: "DyeingPrintingAreaOutputAvalItems",
                column: "DyeingPrintingAreaOutputProductionOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaOutputAvalItems");
        }
    }
}
