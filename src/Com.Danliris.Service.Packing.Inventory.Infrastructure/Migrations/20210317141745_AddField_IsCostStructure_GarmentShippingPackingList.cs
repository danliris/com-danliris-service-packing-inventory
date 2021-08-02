using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddField_IsCostStructure_GarmentShippingPackingList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarmentShippingCostStructureItems_GarmentShippingCostStructures_CostStructureId",
                table: "GarmentShippingCostStructureItems");

            migrationBuilder.DropIndex(
                name: "IX_GarmentShippingCostStructureItems_CostStructureId",
                table: "GarmentShippingCostStructureItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsCostStructured",
                table: "GarmentPackingLists",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCostStructured",
                table: "GarmentPackingLists");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentShippingCostStructureItems_CostStructureId",
                table: "GarmentShippingCostStructureItems",
                column: "CostStructureId");

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentShippingCostStructureItems_GarmentShippingCostStructures_CostStructureId",
                table: "GarmentShippingCostStructureItems",
                column: "CostStructureId",
                principalTable: "GarmentShippingCostStructures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
