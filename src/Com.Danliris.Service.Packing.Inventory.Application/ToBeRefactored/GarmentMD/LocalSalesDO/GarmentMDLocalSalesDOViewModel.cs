using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static iTextSharp.text.pdf.AcroFields;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.LocalSalesDO
{
    public class GarmentMDLocalSalesDOViewModel : BaseViewModel, IValidatableObject
    {
        public string localSalesDONo { get; set; }
        public string localSalesContractNo { get; set; }
        public int localSalesContractId { get; set; }
        public string storageDivision { get; set; }
        public DateTimeOffset date { get; set; }
        public Buyer buyer { get; set; }
        public string to { get; set; }
        public string remark { get; set; }

        public string comodityName { get; set; }
        public string description { get; set; }
        public double quantity { get; set; }
        public UnitOfMeasurement uom { get; set; }
        public double packQuantity { get; set; }
        public UnitOfMeasurement packUom { get; set; }
        public double grossWeight { get; set; }
        public double nettWeight { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (date == null || date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "date" });
            }
            if (buyer == null || buyer.Id == 0)
            {
                yield return new ValidationResult("Buyer Agent tidak boleh kosong", new List<string> { "buyer" });
            }
            if (string.IsNullOrEmpty(localSalesContractNo))
            {
                yield return new ValidationResult("No Kontrak Penjualan tidak boleh kosong", new List<string> { "localSalesContractNo" });
            }
            if (string.IsNullOrEmpty(to))
            {
                yield return new ValidationResult("Kepada tidak boleh kosong", new List<string> { "to" });
            }
            if (string.IsNullOrEmpty(storageDivision))
            {
                yield return new ValidationResult("Bag Gudang tidak boleh kosong", new List<string> { "storageDivision" });
            }

            if (packUom == null || packUom.Id == 0)
            {
                yield return new ValidationResult("Satuan Kemasan tidak boleh kosong", new List<string> { "packUom" });
            }

            if (quantity <= 0)
            {
                yield return new ValidationResult("Quantity harus lebih dari 0", new List<string> { "quantity" });
            }

            if (nettWeight <= 0)
            {
                yield return new ValidationResult("Nett Weight harus lebih dari 0", new List<string> { "nettWeight" });
            }

            if (grossWeight <= 0)
            {
                yield return new ValidationResult("Gross Weight harus lebih dari 0", new List<string> { "grossWeight" });
            }

            if (packQuantity <= 0)
            {
                yield return new ValidationResult("Jumlah Kemasan harus lebih dari 0", new List<string> { "packQuantity" });
            }
        }
    }
}
