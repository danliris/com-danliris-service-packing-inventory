using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class EnhanceDPOutputSPPShipping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DyeingPrintingAreaOutputs_BonNo",
                table: "DyeingPrintingAreaOutputs");

            migrationBuilder.DropIndex(
                name: "IX_DyeingPrintingAreaInputs_BonNo",
                table: "DyeingPrintingAreaInputs");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryNote",
                table: "DyeingPrintingAreaOutputProductionOrders",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryNote",
                table: "DyeingPrintingAreaOutputProductionOrders");

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaOutputs_BonNo",
                table: "DyeingPrintingAreaOutputs",
                column: "BonNo",
                unique: true,
                filter: "[IsDeleted]=(0)");

            migrationBuilder.CreateIndex(
                name: "IX_DyeingPrintingAreaInputs_BonNo",
                table: "DyeingPrintingAreaInputs",
                column: "BonNo",
                unique: true,
                filter: "[IsDeleted]=(0)");
        }
    }
}
