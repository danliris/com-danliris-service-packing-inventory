using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class EnhanceDPOutputNextAreaStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NextAreaInputStatus",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 16,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextAreaInputStatus",
                table: "DyeingPrintingAreaOutputProductionOrders");
        }
    }
}
