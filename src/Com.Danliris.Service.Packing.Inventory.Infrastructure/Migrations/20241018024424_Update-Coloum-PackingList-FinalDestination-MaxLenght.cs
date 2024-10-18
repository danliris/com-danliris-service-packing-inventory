using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class UpdateColoumPackingListFinalDestinationMaxLenght : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AlterColumn<string>(
                name: "FinalDestination",
                table: "GarmentPackingLists",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.AlterColumn<string>(
                name: "FinalDestination",
                table: "GarmentPackingLists",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

           
        }
    }
}
