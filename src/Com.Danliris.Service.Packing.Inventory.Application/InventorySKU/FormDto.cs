using Com.Danliris.Service.Packing.Inventory.Application.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventorySKU
{
    public class FormDto : IValidatableObject
    {
        public DateTimeOffset? Date { get; set; }
        public string ReferenceNo { get; set; }
        public string ReferenceType { get; set; }
        public StorageDto Storage { get; set; }
        public string Type { get; set; }
        public string Remark { get; set; }
        public List<FormItemDto> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Date.HasValue || Date.GetValueOrDefault() > DateTimeOffset.Now)
            {
                yield return new ValidationResult("Date harus diisi", new List<string> { "Date" });
            }

            if (string.IsNullOrEmpty(ReferenceNo))
            {
                yield return new ValidationResult("No. Referensi harus diisi", new List<string> { "ReferenceNo" });
            }

            if (string.IsNullOrEmpty(ReferenceType))
            {
                yield return new ValidationResult("Jenis Referensi harus diisi", new List<string> { "ReferenceType" });
            }

            if (Storage == null || Storage._id.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Gudang harus diisi", new List<string> { "Storage" });
            }

            if (string.IsNullOrEmpty(Type))
            {
                yield return new ValidationResult("Type harus diisi", new List<string> { "Type" });
            }

            if (Items == null || Items.Count <= 0)
            {
                yield return new ValidationResult("Item harus diisi", new List<string> { "Item" });
            }
            else
            {
                var count = 0;
                var errors = "[";

                foreach (var item in Items)
                {
                    errors += "{";

                    if (item.ProductSKUId.GetValueOrDefault() <= 0)
                    {
                        count++;
                        errors += "'Product': 'Barang harus diisi',";
                    }

                    if (item.UOMId.GetValueOrDefault() <= 0)
                    {
                        count++;
                        errors += "'UOM': 'Satuan harus diisi',";
                    }

                    if (item.Quantity.GetValueOrDefault() <= 0)
                    {
                        count++;
                        errors += "'Quantity': 'Jumlah harus lebih dari 0',";
                    }

                    errors += "},";
                }

                errors += "]";

                if (count > 0)
                    yield return new ValidationResult(errors, new List<string> { "Items" });
            }

        }
    }
}