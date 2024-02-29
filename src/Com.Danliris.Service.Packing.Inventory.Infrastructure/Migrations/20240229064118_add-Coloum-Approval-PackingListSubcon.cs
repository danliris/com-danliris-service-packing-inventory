using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class addColoumApprovalPackingListSubcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValidatedMD",
                table: "GarmentReceiptSubconPackingLists",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValidatedShipping",
                table: "GarmentReceiptSubconPackingLists",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Kurs",
                table: "GarmentReceiptSubconPackingLists",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RejectReason",
                table: "GarmentReceiptSubconPackingLists",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValidatedMDBy",
                table: "GarmentReceiptSubconPackingLists",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ValidatedMDDate",
                table: "GarmentReceiptSubconPackingLists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValidatedMDRemark",
                table: "GarmentReceiptSubconPackingLists",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValidatedShippingBy",
                table: "GarmentReceiptSubconPackingLists",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ValidatedShippingDate",
                table: "GarmentReceiptSubconPackingLists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValidatedMD",
                table: "GarmentReceiptSubconPackingLists");

            migrationBuilder.DropColumn(
                name: "IsValidatedShipping",
                table: "GarmentReceiptSubconPackingLists");

            migrationBuilder.DropColumn(
                name: "Kurs",
                table: "GarmentReceiptSubconPackingLists");

            migrationBuilder.DropColumn(
                name: "RejectReason",
                table: "GarmentReceiptSubconPackingLists");

            migrationBuilder.DropColumn(
                name: "ValidatedMDBy",
                table: "GarmentReceiptSubconPackingLists");

            migrationBuilder.DropColumn(
                name: "ValidatedMDDate",
                table: "GarmentReceiptSubconPackingLists");

            migrationBuilder.DropColumn(
                name: "ValidatedMDRemark",
                table: "GarmentReceiptSubconPackingLists");

            migrationBuilder.DropColumn(
                name: "ValidatedShippingBy",
                table: "GarmentReceiptSubconPackingLists");

            migrationBuilder.DropColumn(
                name: "ValidatedShippingDate",
                table: "GarmentReceiptSubconPackingLists");
        }
    }
}
