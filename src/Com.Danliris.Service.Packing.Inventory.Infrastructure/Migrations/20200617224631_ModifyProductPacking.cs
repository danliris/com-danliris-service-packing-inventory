using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class ModifyProductPacking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedMonth",
                table: "ProductPackings");

            migrationBuilder.DropColumn(
                name: "CreatedYear",
                table: "ProductPackings");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductPackings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductPackings");

            migrationBuilder.AddColumn<int>(
                name: "CreatedMonth",
                table: "ProductPackings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedYear",
                table: "ProductPackings",
                nullable: false,
                defaultValue: 0);
        }
    }
}
