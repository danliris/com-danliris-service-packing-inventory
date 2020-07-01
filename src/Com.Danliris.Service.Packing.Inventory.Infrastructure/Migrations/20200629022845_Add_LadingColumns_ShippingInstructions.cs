using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_LadingColumns_ShippingInstructions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Freight",
                table: "GarmentShippingInstructions",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LadingBill",
                table: "GarmentShippingInstructions",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LadingDate",
                table: "GarmentShippingInstructions",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Freight",
                table: "GarmentShippingInstructions");

            migrationBuilder.DropColumn(
                name: "LadingBill",
                table: "GarmentShippingInstructions");

            migrationBuilder.DropColumn(
                name: "LadingDate",
                table: "GarmentShippingInstructions");
        }
    }
}
