using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class add_column_track_DP_WarehouseInputItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrackBox",
                table: "DPWarehouseInputItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrackId",
                table: "DPWarehouseInputItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TrackName",
                table: "DPWarehouseInputItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackType",
                table: "DPWarehouseInputItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackBox",
                table: "DPWarehouseInputItems");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "DPWarehouseInputItems");

            migrationBuilder.DropColumn(
                name: "TrackName",
                table: "DPWarehouseInputItems");

            migrationBuilder.DropColumn(
                name: "TrackType",
                table: "DPWarehouseInputItems");
        }
    }
}
