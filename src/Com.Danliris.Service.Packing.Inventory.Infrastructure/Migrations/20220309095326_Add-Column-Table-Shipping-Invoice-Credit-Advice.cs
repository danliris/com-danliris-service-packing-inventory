//using Microsoft.EntityFrameworkCore.Migrations;

//namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
//{
//    public partial class AddColumnTableShippingInvoiceCreditAdvice : Migration
//    {
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.AddColumn<decimal>(
//                name: "AmountCA",
//                table: "GarmentShippingInvoices",
//                nullable: false,
//                defaultValue: 0m);

//            migrationBuilder.AddColumn<double>(
//                name: "AmountPaid",
//                table: "GarmentShippingCreditAdvices",
//                nullable: false,
//                defaultValue: 0.0);

//            migrationBuilder.AddColumn<double>(
//                name: "BalanceAmount",
//                table: "GarmentShippingCreditAdvices",
//                nullable: false,
//                defaultValue: 0.0);
//        }

//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropColumn(
//                name: "AmountCA",
//                table: "GarmentShippingInvoices");

//            migrationBuilder.DropColumn(
//                name: "AmountPaid",
//                table: "GarmentShippingCreditAdvices");

//            migrationBuilder.DropColumn(
//                name: "BalanceAmount",
//                table: "GarmentShippingCreditAdvices");
//        }
//    }
//}
