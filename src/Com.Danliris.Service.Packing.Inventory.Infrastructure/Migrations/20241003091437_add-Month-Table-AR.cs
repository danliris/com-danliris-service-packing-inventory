using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class addMonthTableAR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "AR_OmzetCorrections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "AR_DownPayments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "AR_CMTs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "AR_CashInBank",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "AR_Balances",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "AR_OmzetCorrections");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "AR_DownPayments");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "AR_CMTs");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "AR_CashInBank");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "AR_Balances");
        }
    }
}
