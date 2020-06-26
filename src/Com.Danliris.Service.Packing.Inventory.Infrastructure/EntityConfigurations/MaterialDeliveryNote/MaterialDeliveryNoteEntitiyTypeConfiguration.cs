using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.EntityConfigurations
{
    public class MaterialDeliveryNoteEntitiyTypeConfiguration : IEntityTypeConfiguration<MaterialDeliveryNoteModel>
    {
        public void Configure(EntityTypeBuilder<MaterialDeliveryNoteModel> builder)
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
                .Property(s => s.BonCode)
                .HasMaxLength(128);

            builder
                .Property(s => s.DateFrom)
                .HasMaxLength(128);

            builder
                .Property(s => s.DateTo)
                .HasMaxLength(128);

            builder
                .Property(s => s.DoNumberId)
                .HasMaxLength(128);

            builder
                .Property(s => s.DONumber)
                .HasMaxLength(128);

            builder
                .Property(s => s.FONumber)
                .HasMaxLength(128);

            builder
                .Property(s => s.ReceiverId)
                .HasMaxLength(128);

            builder
                .Property(s => s.ReceiverCode)
                .HasMaxLength(128);

            builder
                .Property(s => s.ReceiverName)
                .HasMaxLength(128);

            builder
                .Property(s => s.Remark)
                .HasMaxLength(128);

            builder
                .Property(s => s.SCNumberId)
                .HasMaxLength(128);

            builder
                .Property(s => s.SCNumber)
                .HasMaxLength(128);

            builder
               .Property(s => s.SenderId)
               .HasMaxLength(128);

            builder
               .Property(s => s.SenderCode)
               .HasMaxLength(128);

            builder
               .Property(s => s.SenderName)
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
        }
    }
}
