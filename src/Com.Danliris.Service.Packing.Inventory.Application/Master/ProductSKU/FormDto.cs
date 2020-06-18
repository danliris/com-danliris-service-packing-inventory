using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU
{
    public class FormDto : IValidatableObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int? UOMId { get; set; }
        public int? CategoryId { get; set; }
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Code))
            {
                yield return new ValidationResult("Kode harus diisi", new List<string> { "Code" });
            }
            else if (!int.TryParse(Code, out int code))
            {
                yield return new ValidationResult("Kode harus berupa angka", new List<string> { "Code" });
            }

            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Nama Barang harus diisi", new List<string> { "Name" });
            }

            if (UOMId.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Satuan harus diisi", new List<string> { "UOM" });
            }

            if (CategoryId.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Kategori Barang harus diisi", new List<string> { "Category" });
            }
        }
    }
}