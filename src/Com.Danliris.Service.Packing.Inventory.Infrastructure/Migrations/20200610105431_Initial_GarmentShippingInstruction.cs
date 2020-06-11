using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Initial_GarmentShippingInstruction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingInstructions",
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
                    InvoiceNo = table.Column<string>(maxLength: 50, nullable: true),
                    PackingListId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    EMKLId = table.Column<int>(nullable: false),
                    EMKLCode = table.Column<string>(maxLength: 50, nullable: true),
                    EMKLName = table.Column<string>(maxLength: 255, nullable: true),
                    ATTN = table.Column<string>(maxLength: 1000, nullable: true),
                    Fax = table.Column<string>(maxLength: 500, nullable: true),
                    CC = table.Column<string>(maxLength: 500, nullable: true),
                    ShippingStaffId = table.Column<int>(nullable: false),
                    ShippingStaffName = table.Column<string>(maxLength: 500, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    ShippedBy = table.Column<string>(maxLength: 20, nullable: true),
                    TruckingDate = table.Column<DateTimeOffset>(nullable: false),
                    CartonNo = table.Column<string>(maxLength: 50, nullable: true),
                    PortOfDischarge = table.Column<string>(maxLength: 255, nullable: true),
                    PlaceOfDelivery = table.Column<string>(maxLength: 255, nullable: true),
                    FeederVessel = table.Column<string>(maxLength: 255, nullable: true),
                    OceanVessel = table.Column<string>(maxLength: 255, nullable: true),
                    Carrier = table.Column<string>(maxLength: 255, nullable: true),
                    Flight = table.Column<string>(maxLength: 255, nullable: true),
                    Transit = table.Column<string>(maxLength: 255, nullable: true),
                    BankAccountId = table.Column<int>(nullable: false),
                    BankAccountName = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerAgentId = table.Column<int>(nullable: false),
                    BuyerAgentCode = table.Column<string>(maxLength: 100, nullable: true),
                    BuyerAgentName = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerAgentAddress = table.Column<string>(maxLength: 4000, nullable: true),
                    Notify = table.Column<string>(maxLength: 2000, nullable: true),
                    SpecialInstruction = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingInstructions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingInstructions");
        }
    }
}
