using Com.Danliris.Service.Packing.Inventory.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations.MaterialDeliveryNoteWeaving
{
    public class MaterialDeliveryNoteWeavingEntityTypeConfiguration : IEntityTypeConfiguration<MaterialDeliveryNoteWeavingModel>
    {
        public void Configure(EntityTypeBuilder<MaterialDeliveryNoteWeavingModel> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .Property(s => s.CreatedAgent)
                .HasMaxLength(128);

            builder
                .Property(s => s.CreatedBy)
                .HasMaxLength(128);

            builder
                .Property(s => s.LastModifiedAgent)
                .HasMaxLength(128);

            builder
                .Property(s => s.LastModifiedBy)
                .HasMaxLength(128);

            builder
                .Property(s => s.DeletedAgent)
                .HasMaxLength(128);

            builder
                .Property(s => s.DeletedBy)
                .HasMaxLength(128);

            builder
                .Property(s => s.Code)
                .HasMaxLength(128);

            builder
                .Property(s => s.DateSJ)
                .HasMaxLength(128);

            builder
                .Property(s => s.DoSalesNumberId)
                .HasMaxLength(128);

            builder
                .Property(s => s.DoSalesNumber)
                .HasMaxLength(128);

            builder
                .Property(s => s.SendTo)
                .HasMaxLength(128);

            builder
                .Property(s => s.UnitId)
                .HasMaxLength(128);

            builder
                .Property(s => s.UnitName)
                .HasMaxLength(128);

            builder
                .Property(s => s.BuyerId)
                .HasMaxLength(128);

            builder
                .Property(s => s.BuyerCode)
                .HasMaxLength(128);

            builder
                .Property(s => s.BuyerName)
                .HasMaxLength(128);

            builder
                .Property(s => s.NumberOut)
                .HasMaxLength(128);

            builder
                .Property(s => s.StorageId)
                .HasMaxLength(128);

            builder
                .Property(s => s.StorageCode)
                .HasMaxLength(128);

            builder
                .Property(s => s.StorageName)
                .HasMaxLength(128);

            builder
                .Property(s => s.Remark)
                .HasMaxLength(128);

            //builder
            //    .Property(s => s.StorageUnit)
            //    .HasMaxLength(128);

            builder
                .HasMany(inventoryDocument => inventoryDocument.ItemsMaterialDeliveryNoteWeaving)
                .WithOne()
                .HasForeignKey("MaterialDeliveryNoteWeavingId");
        }
    }
}
