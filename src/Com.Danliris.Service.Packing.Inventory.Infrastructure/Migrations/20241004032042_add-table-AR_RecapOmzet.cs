using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class addtableAR_RecapOmzet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "AR_Balances");

            migrationBuilder.DropColumn(
                name: "Destination",
                table: "AR_Balances");

            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "AR_Balances");

            migrationBuilder.DropColumn(
                name: "PEBNo",
                table: "AR_Balances");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "AR_Balances");

            migrationBuilder.DropColumn(
                name: "UOMUnit",
                table: "AR_Balances");

            migrationBuilder.CreateTable(
                name: "AR_RecapOmzet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAgent = table.Column<string>(nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedAgent = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedAgent = table.Column<string>(nullable: true),
                    TruckingDate = table.Column<DateTime>(nullable: false),
                    BuyerAgentCode = table.Column<string>(maxLength: 10, nullable: true),
                    Destination = table.Column<string>(maxLength: 50, nullable: true),
                    InvoiceNo = table.Column<string>(maxLength: 30, nullable: true),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    PEBNo = table.Column<string>(maxLength: 10, nullable: true),
                    PEBDate = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    UOMUnit = table.Column<string>(maxLength: 8, nullable: true),
                    CurrencyCode = table.Column<string>(maxLength: 10, nullable: true),
                    Rate = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    AmountIDR = table.Column<double>(nullable: false),
                    Month = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AR_RecapOmzet", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AR_RecapOmzet");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "AR_Balances",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "AR_Balances",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "AR_Balances",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PEBNo",
                table: "AR_Balances",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "AR_Balances",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "UOMUnit",
                table: "AR_Balances",
                maxLength: 8,
                nullable: true);
        }
    }
}
