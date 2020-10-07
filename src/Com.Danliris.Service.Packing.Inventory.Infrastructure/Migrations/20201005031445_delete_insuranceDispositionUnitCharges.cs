using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class delete_insuranceDispositionUnitCharges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingInsuranceDispositionUnitCharges");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount1A",
                table: "GarmentShippingInsuranceDispositionItems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount1B",
                table: "GarmentShippingInsuranceDispositionItems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount2A",
                table: "GarmentShippingInsuranceDispositionItems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount2B",
                table: "GarmentShippingInsuranceDispositionItems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount2C",
                table: "GarmentShippingInsuranceDispositionItems",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount1A",
                table: "GarmentShippingInsuranceDispositionItems");

            migrationBuilder.DropColumn(
                name: "Amount1B",
                table: "GarmentShippingInsuranceDispositionItems");

            migrationBuilder.DropColumn(
                name: "Amount2A",
                table: "GarmentShippingInsuranceDispositionItems");

            migrationBuilder.DropColumn(
                name: "Amount2B",
                table: "GarmentShippingInsuranceDispositionItems");

            migrationBuilder.DropColumn(
                name: "Amount2C",
                table: "GarmentShippingInsuranceDispositionItems");

            migrationBuilder.CreateTable(
                name: "GarmentShippingInsuranceDispositionUnitCharges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    InsuranceDispositionId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    UnitCode = table.Column<string>(maxLength: 10, nullable: true),
                    UnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingInsuranceDispositionUnitCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentShippingInsuranceDispositionUnitCharges_GarmentShippingInsuranceDispositions_InsuranceDispositionId",
                        column: x => x.InsuranceDispositionId,
                        principalTable: "GarmentShippingInsuranceDispositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingInsuranceDispositionUnitCharges_InsuranceDispositionId",
                table: "GarmentShippingInsuranceDispositionUnitCharges",
                column: "InsuranceDispositionId");
        }
    }
}
