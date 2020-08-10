using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.VBPayment
{
    public class GarmentShippingVBPaymentViewModel : BaseViewModel, IValidatableObject
    {
        public string vbNo { get;  set; }
        public DateTimeOffset vbDate { get;  set; }
        public string paymentType { get;  set; }
        public Buyer buyer { get;  set; }
        public EMKL emkl { get;  set; }
        public Forwarder forwarder { get;  set; }
        public string emklInvoiceNo { get;  set; }
        public string forwarderInvoiceNo { get;  set; }

        public double billValue { get;  set; }
        public double vatValue { get;  set; }
        public DateTimeOffset paymentDate { get;  set; }

        public IncomeTax incomeTax { get;  set; }

        public ICollection<GarmentShippingVBPaymentUnitViewModel> units { get;  set; }
        public ICollection<GarmentShippingVBPaymentInvoiceViewModel> invoices { get;  set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (vbDate == null || vbDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "vbDate" });
            }
            if (buyer == null || buyer.Id == 0)
            {
                yield return new ValidationResult("Buyer tidak boleh kosong", new List<string> { "buyer" });
            }
            if (paymentType == "EMKL")
            {
                if (string.IsNullOrEmpty(emklInvoiceNo))
                {
                    yield return new ValidationResult("No Invoice EMKL tidak boleh kosong", new List<string> { "emklInvoiceNo" });
                }
                if (emkl == null || emkl.Id == 0)
                {
                    yield return new ValidationResult("Buyer tidak boleh kosong", new List<string> { "emkl" });
                }
            }
            else
            {
                if (string.IsNullOrEmpty(forwarderInvoiceNo))
                {
                    yield return new ValidationResult("No Invoice Forwarder tidak boleh kosong", new List<string> { "forwarderInvoiceNo" });
                }
                if (forwarder == null || forwarder.id == 0)
                {
                    yield return new ValidationResult("forwarder tidak boleh kosong", new List<string> { "forwarder" });
                }
            }
            if (paymentDate == null || paymentDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal pembayaran tidak boleh kosong", new List<string> { "paymentDate" });
            }
            if (billValue <= 0 )
            {
                yield return new ValidationResult("Nilai Tagihan harus lebih dari 0", new List<string> { "billValue" });
            }
            if (units == null || units.Count == 0)
            {
                yield return new ValidationResult("Beban Unit tidak boleh kosong", new List<string> { "unitsCount" });
            }
            else
            {
                int errorUnitsCount = 0;
                List<Dictionary<string, object>> errorUnits = new List<Dictionary<string, object>>();

                foreach (var item in units)
                {
                    Dictionary<string, object> errorUnit = new Dictionary<string, object>();

                    if (item.unit == null || item.unit.Id == 0)
                    {
                        errorUnit["unit"] = "Unit tidak boleh kosong";
                        errorUnitsCount++;
                    }

                    if (item.billValue <= 0)
                    {
                        errorUnit["billValue"] = "Tagihan harus lebih dari 0";
                        errorUnitsCount++;
                    }

                    errorUnits.Add(errorUnit);
                }

                if (errorUnitsCount > 0)
                {
                    yield return new ValidationResult(JsonConvert.SerializeObject(errorUnits), new List<string> { "units" });
                }
            }
            if (invoices == null || invoices.Count == 0)
            {
                yield return new ValidationResult("Detail Invoice tidak boleh kosong", new List<string> { "invoicesCount" });
            }
            else
            {
                int errorInvoicesCount = 0;
                List<Dictionary<string, object>> errorInvoices = new List<Dictionary<string, object>>();

                foreach (var item in invoices)
                {
                    Dictionary<string, object> errorInvoice = new Dictionary<string, object>();

                    if (item.invoiceId == 0)
                    {
                        errorInvoice["invoice"] = "Invoice tidak boleh kosong";
                        errorInvoicesCount++;
                    }

                    errorInvoices.Add(errorInvoice);
                }

                if (errorInvoicesCount > 0)
                {
                    yield return new ValidationResult(JsonConvert.SerializeObject(errorInvoices), new List<string> { "invoices" });
                }
            }
        }
    }
}
