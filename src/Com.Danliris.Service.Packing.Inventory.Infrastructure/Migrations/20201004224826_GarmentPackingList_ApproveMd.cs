using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentPackingList_ApproveMd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FabricComposition",
                table: "GarmentPackingLists",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FabricCountryOrigin",
                table: "GarmentPackingLists",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemarkMd",
                table: "GarmentPackingLists",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipmentMode",
                table: "GarmentPackingLists",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "TruckingEstimationDate",
                table: "GarmentPackingLists",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "DescriptionMd",
                table: "GarmentPackingListItems",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriceCMT",
                table: "GarmentPackingListItems",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PriceFOB",
                table: "GarmentPackingListItems",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FabricComposition",
                table: "GarmentPackingLists");

            migrationBuilder.DropColumn(
                name: "FabricCountryOrigin",
                table: "GarmentPackingLists");

            migrationBuilder.DropColumn(
                name: "RemarkMd",
                table: "GarmentPackingLists");

            migrationBuilder.DropColumn(
                name: "ShipmentMode",
                table: "GarmentPackingLists");

            migrationBuilder.DropColumn(
                name: "TruckingEstimationDate",
                table: "GarmentPackingLists");

            migrationBuilder.DropColumn(
                name: "DescriptionMd",
                table: "GarmentPackingListItems");

            migrationBuilder.DropColumn(
                name: "PriceCMT",
                table: "GarmentPackingListItems");

            migrationBuilder.DropColumn(
                name: "PriceFOB",
                table: "GarmentPackingListItems");
        }
    }
}
