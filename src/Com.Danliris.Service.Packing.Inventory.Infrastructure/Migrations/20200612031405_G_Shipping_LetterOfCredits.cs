using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class G_Shipping_LetterOfCredits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingLetterOfCredits",
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
                    DocumentCreditNo = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    IssuedBank = table.Column<string>(maxLength: 200, nullable: true),
                    ApplicantId = table.Column<int>(nullable: false),
                    ApplicantCode = table.Column<string>(maxLength: 100, nullable: true),
                    ApplicantName = table.Column<string>(maxLength: 255, nullable: true),
                    ExpireDate = table.Column<DateTimeOffset>(nullable: false),
                    ExpirePlace = table.Column<string>(maxLength: 255, nullable: true),
                    LatestShipment = table.Column<DateTimeOffset>(nullable: false),
                    LCCondition = table.Column<string>(maxLength: 20, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 50, nullable: true),
                    TotalAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingLetterOfCredits", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingLetterOfCredits_DocumentCreditNo",
                table: "GarmentShippingLetterOfCredits",
                column: "DocumentCreditNo",
                unique: true,
                filter: "[IsDeleted]=(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingLetterOfCredits");
        }
    }
}
