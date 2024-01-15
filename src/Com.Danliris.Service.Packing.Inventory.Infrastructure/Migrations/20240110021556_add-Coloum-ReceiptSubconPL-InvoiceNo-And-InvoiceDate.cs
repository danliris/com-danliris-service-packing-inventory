using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class addColoumReceiptSubconPLInvoiceNoAndInvoiceDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InvoiceDate",
                table: "GarmentReceiptSubconPackingLists",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNo",
                table: "GarmentReceiptSubconPackingLists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "GarmentReceiptSubconPackingLists");

            migrationBuilder.DropColumn(
                name: "InvoiceNo",
                table: "GarmentReceiptSubconPackingLists");
        }
    }
}
