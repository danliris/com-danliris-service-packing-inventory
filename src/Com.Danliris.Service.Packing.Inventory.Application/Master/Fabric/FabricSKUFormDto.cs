using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public class FabricSKUFormDto : IValidatableObject
    {
        public int? ConstructionId { get; set; }
        public int? GradeId { get; set; }
        public int? ProcessTypeId { get; set; }
        public int? UOMId { get; set; }
        public int? WarpId { get; set; }
        public int? WeftId { get; set; }
        public int? WidthId { get; set; }
        public int? WovenTypeId { get; set; }
        public int? YarnTypeId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ConstructionId.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Konstruksi harus diisi", new List<string> { "Construction" });
            }

            if (GradeId.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Grade harus diisi", new List<string> { "Grade" });
            }

            if (ProcessTypeId.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Jenis Proses harus diisi", new List<string> { "ProcessType" });
            }

            if (UOMId.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Satuan harus diisi", new List<string> { "UOM" });
            }

            if (WarpId.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Lusi harus diisi", new List<string> { "Warp" });
            }

            if (WeftId.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Pakan harus diisi", new List<string> { "Weft" });
            }

            if (WidthId.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Lebar harus diisi", new List<string> { "Width" });
            }

            if (WovenTypeId.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Jenis Anyaman harus diisi", new List<string> { "WovenType" });
            }

            if (YarnTypeId.GetValueOrDefault() <= 0)
            {
                yield return new ValidationResult("Jenis Benang harus diisi", new List<string> { "YarnType" });
            }
        }
    }
}