using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_track_column_DPWarehouseOutItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrackFromBox",
                table: "DPWarehouseOutputItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrackFromId",
                table: "DPWarehouseOutputItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TrackFromName",
                table: "DPWarehouseOutputItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackFromType",
                table: "DPWarehouseOutputItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackFromBox",
                table: "DPWarehouseOutputItems");

            migrationBuilder.DropColumn(
                name: "TrackFromId",
                table: "DPWarehouseOutputItems");

            migrationBuilder.DropColumn(
                name: "TrackFromName",
                table: "DPWarehouseOutputItems");

            migrationBuilder.DropColumn(
                name: "TrackFromType",
                table: "DPWarehouseOutputItems");
        }
    }
}
