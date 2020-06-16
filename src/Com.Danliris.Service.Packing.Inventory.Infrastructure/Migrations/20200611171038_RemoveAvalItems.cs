using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class RemoveAvalItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaOutputAvalItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaOutputAvalItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DyeingPrintingAreaOutputProductionOrderId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    Length = table.Column<double>(nullable: false),
                    Type = table.Column<string>(maxLength: 128, nullable: true)
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
    }
}
