using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class Add_GarmentShippingCoverLetters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarmentShippingCoverLetters",
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
                    PackingListId = table.Column<int>(nullable: false),
                    InvoiceNo = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Address = table.Column<string>(maxLength: 1000, nullable: true),
                    ATTN = table.Column<string>(maxLength: 250, nullable: true),
                    Phone = table.Column<string>(maxLength: 250, nullable: true),
                    BookingDate = table.Column<DateTimeOffset>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    OrderCode = table.Column<string>(maxLength: 100, nullable: true),
                    OrderName = table.Column<string>(maxLength: 255, nullable: true),
                    PCSQuantity = table.Column<double>(nullable: false),
                    SETSQuantity = table.Column<double>(nullable: false),
                    PACKQuantity = table.Column<double>(nullable: false),
                    CartoonQuantity = table.Column<double>(nullable: false),
                    ForwarderId = table.Column<int>(nullable: false),
                    ForwarderCode = table.Column<string>(maxLength: 50, nullable: true),
                    ForwarderName = table.Column<string>(maxLength: 250, nullable: true),
                    Truck = table.Column<string>(maxLength: 250, nullable: true),
                    PlateNumber = table.Column<string>(maxLength: 250, nullable: true),
                    Driver = table.Column<string>(maxLength: 250, nullable: true),
                    ContainerNo = table.Column<string>(maxLength: 250, nullable: true),
                    Freight = table.Column<string>(maxLength: 250, nullable: true),
                    ShippingSeal = table.Column<string>(maxLength: 250, nullable: true),
                    DLSeal = table.Column<string>(maxLength: 250, nullable: true),
                    EMKLSeal = table.Column<string>(maxLength: 250, nullable: true),
                    ExportEstimationDate = table.Column<DateTimeOffset>(nullable: false),
                    Unit = table.Column<string>(maxLength: 1000, nullable: true),
                    ShippingStaff = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentShippingCoverLetters", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentShippingCoverLetters");
        }
    }
}
