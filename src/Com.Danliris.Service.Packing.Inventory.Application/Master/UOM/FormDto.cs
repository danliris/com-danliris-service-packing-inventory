using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.UOM
{
    public class FormDto : IValidatableObject
    {
        public string Unit { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Unit))
            {
                yield return new ValidationResult("Unit harus diisi", new List<string> { "Unit" });
            }
        }
    }
}