using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddTableForNewLocalSalesContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentMDLocalSalesContracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModifiedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 128, nullable: true),
                    SalesContractNo = table.Column<string>(maxLength: 50, nullable: true),
                    SalesContractDate = table.Column<DateTimeOffset>(nullable: false),
                    TransactionTypeId = table.Column<int>(nullable: false),
                    TransactionTypeCode = table.Column<string>(maxLength: 100, nullable: true),
                    TransactionTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    SellerName = table.Column<string>(maxLength: 100, nullable: true),
                    SellerPosition = table.Column<string>(maxLength: 100, nullable: true),
                    SellerAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    SellerNPWP = table.Column<string>(maxLength: 50, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    BuyerNPWP = table.Column<string>(maxLength: 50, nullable: true),
                    IsUseVat = table.Column<bool>(nullable: false),
                    VatId = table.Column<int>(nullable: false),
                    VatRate = table.Column<int>(nullable: false),
                    ComodityName = table.Column<string>(maxLength: 250, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    RemainingQuantity = table.Column<double>(nullable: false),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 250, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    SubTotal = table.Column<decimal>(nullable: false),
                    IsLocalSalesDOCreated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentMDLocalSalesContracts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentMDLocalSalesContracts_SalesContractNo",
                table: "GarmentMDLocalSalesContracts",
                column: "SalesContractNo",
                unique: true,
                filter: "[IsDeleted]=(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentMDLocalSalesContracts");
        }
    }
}
