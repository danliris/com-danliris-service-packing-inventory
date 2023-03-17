using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Alter_table_ProductPacking_add_column_PackingType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PackingType",
                table: "ProductPackings",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackingType",
                table: "ProductPackings");
        }
    }
}
