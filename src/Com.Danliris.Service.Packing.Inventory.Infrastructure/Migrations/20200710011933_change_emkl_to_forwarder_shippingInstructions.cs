using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class change_emkl_to_forwarder_shippingInstructions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EMKLName",
                table: "GarmentShippingInstructions",
                newName: "ForwarderPhone");

            migrationBuilder.RenameColumn(
                name: "EMKLId",
                table: "GarmentShippingInstructions",
                newName: "ForwarderId");

            migrationBuilder.RenameColumn(
                name: "EMKLCode",
                table: "GarmentShippingInstructions",
                newName: "ForwarderCode");

            migrationBuilder.AddColumn<string>(
                name: "ForwarderAddress",
                table: "GarmentShippingInstructions",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForwarderName",
                table: "GarmentShippingInstructions",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForwarderAddress",
                table: "GarmentShippingInstructions");

            migrationBuilder.DropColumn(
                name: "ForwarderName",
                table: "GarmentShippingInstructions");

            migrationBuilder.RenameColumn(
                name: "ForwarderPhone",
                table: "GarmentShippingInstructions",
                newName: "EMKLName");

            migrationBuilder.RenameColumn(
                name: "ForwarderId",
                table: "GarmentShippingInstructions",
                newName: "EMKLId");

            migrationBuilder.RenameColumn(
                name: "ForwarderCode",
                table: "GarmentShippingInstructions",
                newName: "EMKLCode");
        }
    }
}
