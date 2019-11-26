using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductSKUs",
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
                    Code = table.Column<string>(maxLength: 32, nullable: true),
                    Composition = table.Column<string>(maxLength: 128, nullable: true),
                    Construction = table.Column<string>(maxLength: 128, nullable: true),
                    Currency = table.Column<string>(maxLength: 1024, nullable: true),
                    Description = table.Column<string>(maxLength: 1024, nullable: true),
                    Design = table.Column<string>(maxLength: 128, nullable: true),
                    Grade = table.Column<string>(maxLength: 32, nullable: true),
                    Lot = table.Column<string>(maxLength: 128, nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ProductType = table.Column<string>(maxLength: 32, nullable: true),
                    SKUId = table.Column<string>(maxLength: 128, nullable: true),
                    SKUCode = table.Column<string>(maxLength: 32, nullable: true),
                    Tags = table.Column<string>(maxLength: 512, nullable: true),
                    UOM = table.Column<string>(maxLength: 1024, nullable: true),
                    Width = table.Column<string>(maxLength: 128, nullable: true),
                    WovenType = table.Column<string>(maxLength: 128, nullable: true),
                    YarnType1 = table.Column<string>(maxLength: 128, nullable: true),
                    YarnType2 = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSKUs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSKUs");
        }
    }
}
