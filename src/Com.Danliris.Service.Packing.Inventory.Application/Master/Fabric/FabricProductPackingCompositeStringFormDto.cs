using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public class FabricProductPackingCompositeStringFormDto : IValidatableObject
    {
        public double? PackingSize { get; set; }
        public string SKUCode { get; set; }
        // Packing UOM
        public string PackingUOM { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PackingSize.GetValueOrDefault() > 0)
            {
                yield return new ValidationResult("Quantity harus diisi", new List<string> { "PackingSize" });
            }

            if (string.IsNullOrWhiteSpace(SKUCode))
            {
                yield return new ValidationResult("Kode SKU harus diisi", new List<string> { "SKUCode" });
            }

            if (string.IsNullOrWhiteSpace(PackingUOM))
            {
                yield return new ValidationResult("Jenis Packing harus diisi", new List<string> { "PackingUOM" });
            }
        }
    }
}