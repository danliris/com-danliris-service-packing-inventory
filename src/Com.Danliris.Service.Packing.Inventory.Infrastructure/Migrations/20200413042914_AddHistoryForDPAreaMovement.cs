using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddHistoryForDPAreaMovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "DyeingPrintingAreaMovements",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DyeingPrintingAreaMovementHistories",
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
                    Index = table.Column<int>(nullable: false),
                    DyeingPrintingAreaMovementId = table.Column<int>(nullable: false)
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
                name: "IX_DyeingPrintingAreaMovementHistories_DyeingPrintingAreaMovementId",
                table: "DyeingPrintingAreaMovementHistories",
                column: "DyeingPrintingAreaMovementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DyeingPrintingAreaMovementHistories");

            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "DyeingPrintingAreaMovements");
        }
    }
}
