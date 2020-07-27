using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public class FabricProductSKUCompositeIdFormDto : IValidatableObject
    {
        public int? ColorWayId { get; set; }
        public int? ConstructionTypeId { get; set; }
        public int? GradeId { get; set; }
        public int? ProcessTypeId { get; set; }
        public int? UOMId { get; set; }
        public int? WarpThreadId { get; set; }
        public int? WeftThreadId { get; set; }
        public int? WidthId { get; set; }
        public int? WovenTypeId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ColorWayId.GetValueOrDefault() > 0)
            {
                yield return new ValidationResult("Warna harus diisi", new List<string> { "ColorWay" });
            }

            if (ConstructionTypeId.GetValueOrDefault() > 0)
            {
                yield return new ValidationResult("Konstruksi harus diisi", new List<string> { "ConstructionType" });
            }

            if (GradeId.GetValueOrDefault() > 0)
            {
                yield return new ValidationResult("Grade harus diisi", new List<string> { "Grade" });
            }

            if (ProcessTypeId.GetValueOrDefault() > 0)
            {
                yield return new ValidationResult("Jenis Proses harus diisi", new List<string> { "ProcessType" });
            }

            if (UOMId.GetValueOrDefault() > 0)
            {
                yield return new ValidationResult("Satuan harus diisi", new List<string> { "Satuan" });
            }

            if (WarpThreadId.GetValueOrDefault() > 0)
            {
                yield return new ValidationResult("Benang Lusi harus diisi", new List<string> { "WarpThread" });
            }

            if (WeftThreadId.GetValueOrDefault() > 0)
            {
                yield return new ValidationResult("Benang Pakan harus diisi", new List<string> { "WeftThread" });
            }

            if (WidthId.GetValueOrDefault() > 0)
            {
                yield return new ValidationResult("Lebar harus diisi", new List<string> { "Lebar" });
            }

            if (WovenTypeId.GetValueOrDefault() > 0)
            {
                yield return new ValidationResult("Jenis Anyaman harus diisi", new List<string> { "WovenType" });
            }
        }
    }
}