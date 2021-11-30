using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class update_LocalSalesNotes_approval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApproveFinanceBy",
                table: "GarmentShippingLocalSalesNotes",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApproveFinanceDate",
                table: "GarmentShippingLocalSalesNotes",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "ApproveShippingBy",
                table: "GarmentShippingLocalSalesNotes",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApproveShippingDate",
                table: "GarmentShippingLocalSalesNotes",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsApproveFinance",
                table: "GarmentShippingLocalSalesNotes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproveShipping",
                table: "GarmentShippingLocalSalesNotes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproveFinanceBy",
                table: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropColumn(
                name: "ApproveFinanceDate",
                table: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropColumn(
                name: "ApproveShippingBy",
                table: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropColumn(
                name: "ApproveShippingDate",
                table: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropColumn(
                name: "IsApproveFinance",
                table: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropColumn(
                name: "IsApproveShipping",
                table: "GarmentShippingLocalSalesNotes");
        }
    }
}
