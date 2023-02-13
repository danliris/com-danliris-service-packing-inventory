using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.SalesExport.ExportCoverLetter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.GarmentShipping.SalesExport
{
    public class GarmentExportCoverLetterConfig : IEntityTypeConfiguration<GarmentShippingExportCoverLetterModel>
    {
        public void Configure(EntityTypeBuilder<GarmentShippingExportCoverLetterModel> builder)
        {
            /* StandardEntity */
            builder.HasKey(s => s.Id);
            builder.Property(s => s.CreatedAgent).HasMaxLength(128);
            builder.Property(s => s.CreatedBy).HasMaxLength(128);
            builder.Property(s => s.LastModifiedAgent).HasMaxLength(128);
            builder.Property(s => s.LastModifiedBy).HasMaxLength(128);
            builder.Property(s => s.DeletedAgent).HasMaxLength(128);
            builder.Property(s => s.DeletedBy).HasMaxLength(128);
            builder.HasQueryFilter(f => !f.IsDeleted);
            /* StandardEntity */

            builder.Property(s => s.NoteNo).HasMaxLength(50);
            builder.Property(s => s.ExportCoverLetterNo).HasMaxLength(50);
            builder.Property(s => s.BuyerCode).HasMaxLength(100);
            builder.Property(s => s.BuyerName).HasMaxLength(255);
            builder.Property(s => s.BuyerAdddress).HasMaxLength(1000);
            builder.Property(s => s.Remark).HasMaxLength(1000);
            builder.Property(s => s.BCNo).HasMaxLength(50);

            builder.Property(s => s.Truck).HasMaxLength(250);
            builder.Property(s => s.PlateNumber).HasMaxLength(250);
            builder.Property(s => s.Driver).HasMaxLength(250);
            builder.Property(s => s.ShippingStaffName).HasMaxLength(250);
        }
    }
}
