using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public class FabricProductPackingCompositeIdFormDto : IValidatableObject
    {
        public double? PackingSize { get; set; }
        public int? SKUId { get; set; }
        // Packing UOM
        public int? PackingUOMId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PackingSize.GetValueOrDefault() > 0)
            {
                yield return new ValidationResult("Quantity harus diisi", new List<string> { "PackingSize" });
            }

            if (SKUId.GetValueOrDefault() > 0)
            {
                yield return new ValidationResult("SKU harus diisi", new List<string> { "SKUId" });
            }

            if (PackingUOMId.GetValueOrDefault() > 0)
            {
                yield return new ValidationResult("Jenis Packing harus diisi", new List<string> { "PackingUOMId" });
            }
        }
    }
}