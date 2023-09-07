using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_column_MaterialOrigin_Remark_DPWarehouseSummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaterialOrigin",
                table: "DPWarehouseSummaries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "DPWarehouseSummaries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialOrigin",
                table: "DPWarehouseSummaries");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "DPWarehouseSummaries");
        }
    }
}
