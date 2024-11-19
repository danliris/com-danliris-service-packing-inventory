using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static iTextSharp.text.pdf.AcroFields;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.LocalSalesContract
{
    public class GarmentMDLocalSalesContractViewModel : BaseViewModel, IValidatableObject
    {
        public string salesContractNo { get; set; }
        public DateTimeOffset? salesContractDate { get; set; }
        public TransactionType transactionType { get; set; }
        public Buyer buyer { get; set; }
        public bool isUseVat { get; set; }
        public Vat vat { get; set; }

        public string sellerName { get; set; }
        public string sellerNPWP { get; set; }
        public string sellerPosition { get; set; }
        public string sellerAddress { get; set; }

        public string comodityName { get; set; }
        public double quantity { get; set; }
        public double remainingQuantity { get; set; }
        public UnitOfMeasurement uom { get; set; }
        public double price { get; set; }
        public string remark { get; set; }

        public decimal subTotal { get; set; }

        public bool isLocalSalesDOCreated { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (salesContractDate == null || salesContractDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "salesContractDate" });
            }

            if (transactionType == null || transactionType.id == 0)
            {
                yield return new ValidationResult("Jenis Transaksi tidak boleh kosong", new List<string> { "transactionType" });
            }

            if (buyer == null || buyer.Id == 0)
            {
                yield return new ValidationResult("Buyer tidak boleh kosong", new List<string> { "buyer" });
            }

            if (string.IsNullOrWhiteSpace(sellerName))
            {
                yield return new ValidationResult("Nama Penjual tidak boleh kosong", new List<string> { "sellerName" });
            }

            if (string.IsNullOrWhiteSpace(sellerNPWP))
            {
                yield return new ValidationResult("NPWP Penjual tidak boleh kosong", new List<string> { "sellerNPWP" });
            }

            if (string.IsNullOrWhiteSpace(sellerAddress))
            {
                yield return new ValidationResult("NPWP Penjual tidak boleh kosong", new List<string> { "sellerAddress" });
            }

            if (string.IsNullOrWhiteSpace(sellerPosition))
            {
                yield return new ValidationResult("Jabatan Penjual tidak boleh kosong", new List<string> { "sellerPosition" });
            }

            if (string.IsNullOrWhiteSpace(comodityName))
            {
                yield return new ValidationResult("Komoditi tidak boleh kosong", new List<string> { "comodity" });
            }

            if (uom == null || uom.Id == 0)
            {
                yield return new ValidationResult("satuan tidak boleh kosong", new List<string> { "uom" });
            }

            if (quantity <= 0)
            {
                yield return new ValidationResult("Quantity harus lebih dari 0", new List<string> { "quantity" });
            }

            if (price <= 0)
            {
                yield return new ValidationResult("Harga harus lebih dari 0", new List<string> { "price" });
            }
        }
    }
}
