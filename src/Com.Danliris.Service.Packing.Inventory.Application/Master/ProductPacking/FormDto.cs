using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking
{
    public class FormDto : IValidatableObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ProductSKUId { get; set; }
        public int? UOMId { get; set; }
        public double? PackingSize { get; set; }
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Code))
            {
                yield return new ValidationResult("Kode harus diisi", new List<string> { "Code" });
            }
            else if (!long.TryParse(Code, out long code))
            {
                yield return new ValidationResult("Kode harus berupa angka", new List<string> { "Code" });
            }

            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Nama Packing harus diisi", new List<string> { "Name" });
            }

            if (ProductSKUId.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Barang SKU harus diisi", new List<string> { "ProductSKU" });
            }

            if (UOMId.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Satuan harus diisi", new List<string> { "UOM" });
            }

            if (PackingSize.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Kuantiti per packing harus diisi", new List<string> { "PackingSize" });
            }
        }
    }
}