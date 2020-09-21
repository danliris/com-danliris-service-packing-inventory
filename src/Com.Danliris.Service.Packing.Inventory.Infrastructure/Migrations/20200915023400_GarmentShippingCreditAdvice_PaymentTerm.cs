using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentShippingCreditAdvice_PaymentTerm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LCNo",
                table: "GarmentShippingCreditAdvices",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OtherCharge",
                table: "GarmentShippingCreditAdvices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PaymentDate",
                table: "GarmentShippingCreditAdvices",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "PaymentTerm",
                table: "GarmentShippingCreditAdvices",
                maxLength: 25,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LCNo",
                table: "GarmentShippingCreditAdvices");

            migrationBuilder.DropColumn(
                name: "OtherCharge",
                table: "GarmentShippingCreditAdvices");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "GarmentShippingCreditAdvices");

            migrationBuilder.DropColumn(
                name: "PaymentTerm",
                table: "GarmentShippingCreditAdvices");
        }
    }
}
