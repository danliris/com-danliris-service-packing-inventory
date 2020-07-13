using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class GarmentCreditAdvice_Policy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AccountsReceivablePolicyDate",
                table: "GarmentShippingCreditAdvices",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "AccountsReceivablePolicyNo",
                table: "GarmentShippingCreditAdvices",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CargoPolicyDate",
                table: "GarmentShippingCreditAdvices",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CargoPolicyNo",
                table: "GarmentShippingCreditAdvices",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DocumentSendDate",
                table: "GarmentShippingCreditAdvices",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountsReceivablePolicyDate",
                table: "GarmentShippingCreditAdvices");

            migrationBuilder.DropColumn(
                name: "AccountsReceivablePolicyNo",
                table: "GarmentShippingCreditAdvices");

            migrationBuilder.DropColumn(
                name: "CargoPolicyDate",
                table: "GarmentShippingCreditAdvices");

            migrationBuilder.DropColumn(
                name: "CargoPolicyNo",
                table: "GarmentShippingCreditAdvices");

            migrationBuilder.DropColumn(
                name: "DocumentSendDate",
                table: "GarmentShippingCreditAdvices");
        }
    }
}
