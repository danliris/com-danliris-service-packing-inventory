using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Category
{
    public class FormDto : IValidatableObject
    {
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Nama harus diisi", new List<string> { "Name" });
            }
        }
    }
}