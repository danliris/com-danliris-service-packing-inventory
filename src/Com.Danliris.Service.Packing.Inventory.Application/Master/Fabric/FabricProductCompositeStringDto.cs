using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public class FabricProductCompositeStringDto : IValidatableObject
    {
        // warna
        public string ColorWay { get; set; }
        // material konstruksi
        public string ConstructionType { get; set; }
        // grade
        public string Grade { get; set; }
        // panjang per packing
        public double? PackingSize { get; set; }
        // Packing UOM jenis packing
        public string PackingType { get; set; }
        // jenis proses
        public string ProcessType { get; set; }
        // satuan
        public string UOM { get; set; }
        // lusi
        public string WarpThread { get; set; }
        // pakan
        public string WeftThread { get; set; }
        // material width
        public string Width { get; set; }
        // material
        public string WovenType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(ColorWay))
            {
                yield return new ValidationResult("Warna harus diisi", new List<string> { "ColorWay" });
            }

            if (string.IsNullOrEmpty(ConstructionType))
            {
                yield return new ValidationResult("Konstruksi harus diisi", new List<string> { "ConstructionType" });
            }

            if (string.IsNullOrEmpty(Grade))
            {
                yield return new ValidationResult("Grade harus diisi", new List<string> { "Grade" });
            }

            if (PackingSize.GetValueOrDefault() > 0)
            {
                yield return new ValidationResult("Quantity harus diisi", new List<string> { "PackingSize" });
            }

            if (string.IsNullOrEmpty(PackingType))
            {
                yield return new ValidationResult("Jenis Packing harus diisi", new List<string> { "Jenis Packing" });
            }

            if (string.IsNullOrEmpty(ProcessType))
            {
                yield return new ValidationResult("Jenis Proses harus diisi", new List<string> { "ProcessType" });
            }

            if (string.IsNullOrEmpty(UOM))
            {
                yield return new ValidationResult("Satuan harus diisi", new List<string> { "Satuan" });
            }

            if (string.IsNullOrEmpty(WarpThread))
            {
                yield return new ValidationResult("Benang Lusi harus diisi", new List<string> { "WarpThread" });
            }

            if (string.IsNullOrEmpty(WeftThread))
            {
                yield return new ValidationResult("Benang Pakan harus diisi", new List<string> { "WeftThread" });
            }

            if (string.IsNullOrEmpty(Width))
            {
                yield return new ValidationResult("Lebar harus diisi", new List<string> { "Lebar" });
            }

            if (string.IsNullOrEmpty(WovenType))
            {
                yield return new ValidationResult("Jenis Anyaman harus diisi", new List<string> { "WovenType" });
            }
        }
    }
}