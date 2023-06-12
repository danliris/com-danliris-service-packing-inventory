using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddColumn_BuyerCode_On_Creadit_Advice_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAccountNo",
                table: "GarmentShippingCreditAdvices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerCode",
                table: "GarmentShippingCreditAdvices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAccountNo",
                table: "GarmentShippingCreditAdvices");

            migrationBuilder.DropColumn(
                name: "BuyerCode",
                table: "GarmentShippingCreditAdvices");
        }
    }
}
