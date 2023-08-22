using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_column_construction_productpacking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FinishWidth",
                table: "ProductPackings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialConstructionId",
                table: "ProductPackings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialConstructionName",
                table: "ProductPackings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "ProductPackings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "ProductPackings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YarnMaterialId",
                table: "ProductPackings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "YarnMaterialName",
                table: "ProductPackings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishWidth",
                table: "ProductPackings");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionId",
                table: "ProductPackings");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionName",
                table: "ProductPackings");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "ProductPackings");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "ProductPackings");

            migrationBuilder.DropColumn(
                name: "YarnMaterialId",
                table: "ProductPackings");

            migrationBuilder.DropColumn(
                name: "YarnMaterialName",
                table: "ProductPackings");
        }
    }
}
