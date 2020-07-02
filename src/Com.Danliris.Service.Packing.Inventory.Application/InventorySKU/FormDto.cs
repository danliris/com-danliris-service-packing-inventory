using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventorySKU
{
    public class FormDto : IValidatableObject
    {
        public DateTimeOffset? Date { get; internal set; }
        public string ReferenceNo { get; internal set; }
        public string ReferenceType { get; internal set; }
        public Storage Storage { get; set; }
        public string Type { get; internal set; }
        public string Remark { get; internal set; }
        public List<FormItemDto> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Date.HasValue || Date.GetValueOrDefault() > DateTimeOffset.Now)
            {
                yield return new ValidationResult("Nama harus diisi", new List<string> { "Name" });
            }

            if (string.IsNullOrEmpty(ReferenceNo))
            {
                yield return new ValidationResult("No. Referensi harus diisi", new List<string> { "ReferenceNo" });
            }

            if (string.IsNullOrEmpty(ReferenceType))
            {
                yield return new ValidationResult("Jenis Referensi harus diisi", new List<string> { "ReferenceType" });
            }

            if (Storage == null || Storage.Id.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Gudang harus diisi", new List<string> { "Storage" });
            }

            if (string.IsNullOrEmpty(Type))
            {
                yield return new ValidationResult("Nama harus diisi", new List<string> { "Name" });
            }

            if (Items == null || Items.Count <= 0)
            {
                yield return new ValidationResult("Item harus diisi", new List<string> { "Item" });
            }
            else
            {
                
            }
        }
    }
}