using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionViewModel : BaseViewModel, IValidatableObject
    {
        public string dispositionNo { get;  set; }
        public string paymentType { get;  set; }
        public string paymentMethod { get;  set; }
        public string paidAt { get;  set; }
        public string sendBy { get;  set; }

        public BuyerAgent buyerAgent { get; set; }

        public string paymentTerm { get;  set; }

        public Forwarder forwarder { get;  set; }
        public Courier courier { get;  set; }
        public EMKL emkl { get;  set; }


        public string address { get;  set; }
        public string npwp { get;  set; }

        public string invoiceNumber { get;  set; }
        public DateTimeOffset invoiceDate { get;  set; }
        public string invoiceTaxNumber { get;  set; }

        public decimal billValue { get;  set; }
        public decimal vatValue { get;  set; }

        public IncomeTax incomeTax { get;  set; }
        public decimal IncomeTaxValue { get;  set; }

        public decimal totalBill { get;  set; }
        public DateTimeOffset paymentDate { get;  set; }
        public string bank { get;  set; }
        public string accNo { get;  set; }

        public bool isFreightCharged { get;  set; }
        public string freightBy { get;  set; }
        public string freightNo { get;  set; }
        public DateTimeOffset freightDate { get;  set; }
        public string flightVessel { get; set; }

        public string remark { get;  set; }
        public ICollection<GarmentShippingPaymentDispositionInvoiceDetailViewModel> invoiceDetails { get; set; }
        public ICollection<GarmentShippingPaymentDispositionBillDetailViewModel> billDetails { get; set; }
        public ICollection<GarmentShippingPaymentDispositionUnitChargeViewModel> unitCharges { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (incomeTax == null || incomeTax.id == 0)
            {
                yield return new ValidationResult("PPH tidak boleh kosong", new List<string> { "incomeTax" });
            }
            if (paymentDate == null || paymentDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal Pembayaran tidak boleh kosong", new List<string> { "paymentDate" });
            }
            if (paymentType == "FORWARDER")
            {
                if (forwarder == null || forwarder.id == 0)
                {
                    yield return new ValidationResult("Forwarder tidak boleh kosong", new List<string> { "forwarder" });
                }
                if (isFreightCharged)
                {
                    if (string.IsNullOrWhiteSpace(freightNo))
                    {
                        yield return new ValidationResult("AWB/BL No tidak boleh kosong", new List<string> { "freightNo" });
                    }
                    if (freightDate == null || freightDate == DateTimeOffset.MinValue)
                    {
                        yield return new ValidationResult("Tanggal AWB/BL tidak boleh kosong", new List<string> { "freightDate" });
                    }
                    if (string.IsNullOrWhiteSpace(flightVessel))
                    {
                        yield return new ValidationResult("Flight/Vessel tidak boleh kosong", new List<string> { "flightVessel" });
                    }

                }
            }
            if (paymentType == "EMKL")
            {
                if (emkl == null || emkl.Id == 0)
                {
                    yield return new ValidationResult("EMKL tidak boleh kosong", new List<string> { "emkl" });
                }
            }
            if (paymentType == "COURIER")
            {
                if (courier == null || courier.Id == 0)
                {
                    yield return new ValidationResult("Kurir tidak boleh kosong", new List<string> { "courier" });
                }
            }
            else
            {
                if (buyerAgent == null || buyerAgent.Id == 0)
                {
                    yield return new ValidationResult("Buyer tidak boleh kosong", new List<string> { "buyerAgent" });
                }
            }
            if (billDetails == null || billDetails.Count == 0)
            {
                yield return new ValidationResult("Perincian Tagihan tidak boleh kosong", new List<string> { "billDetailsCount" });
            }
            else
            {
                int errorBillCount = 0;
                List<Dictionary<string, object>> errorBills = new List<Dictionary<string, object>>();

                foreach (var bill in billDetails)
                {
                    Dictionary<string, object> errorBill = new Dictionary<string, object>();

                    if (string.IsNullOrWhiteSpace(bill.billDescription))
                    {
                        errorBill["billDescription"] = "Tagihan tidak boleh kosong";
                        errorBillCount++;
                    }

                    if (bill.amount <= 0)
                    {
                        errorBill["amount"] = "Nominal harus lebih dari 0";
                        errorBillCount++;
                    }

                    errorBills.Add(errorBill);
                }

                if (errorBillCount > 0)
                {
                    yield return new ValidationResult(JsonConvert.SerializeObject(errorBills), new List<string> { "bills" });
                }
            }
            if (paymentType=="EMKL" || paymentType == "FORWARDER")
            {
                if (invoiceDetails == null || invoiceDetails.Count == 0)
                {
                    yield return new ValidationResult("Detail Invoice tidak boleh kosong", new List<string> { "invoiceDetailsCount" });
                }
                else
                {
                    int errorItemsCount = 0;
                    List<Dictionary<string, object>> errorItems = new List<Dictionary<string, object>>();

                    foreach (var invoice in invoiceDetails)
                    {
                        Dictionary<string, object> errorItem = new Dictionary<string, object>();

                        if (string.IsNullOrWhiteSpace(invoice.invoiceNo) || invoice.invoiceId==0)
                        {
                            errorItem["invoiceNo"] = "Invoice tidak boleh kosong";
                            errorItemsCount++;
                        }
                        if(isFreightCharged)
                        {
                            if (invoice.grossWeight <= 0)
                            {
                                errorItem["grossWeight"] = "GW harus lebih dari 0";
                                errorItemsCount++;
                            }
                            if (invoice.grossWeight <= 0)
                            {
                                errorItem["grossWeight"] = "GW harus lebih dari 0";
                                errorItemsCount++;
                            }
                        }

                        if (invoice.totalCarton <= 0)
                        {
                            errorItem["totalCarton"] = "Total Carton harus lebih dari 0";
                            errorItemsCount++;
                        }

                        errorItems.Add(errorItem);
                    }

                    if (errorItemsCount > 0)
                    {
                        yield return new ValidationResult(JsonConvert.SerializeObject(errorItems), new List<string> { "invoices" });
                    }
                }
            }
            if (paymentType == "COURIER")
            {
                if (unitCharges == null || unitCharges.Count == 0)
                {
                    yield return new ValidationResult("Beban Unit tidak boleh kosong", new List<string> { "unitChargesCount" });
                }
                else
                {
                    int errorUnitCount = 0;
                    List<Dictionary<string, object>> errorUnits = new List<Dictionary<string, object>>();

                    foreach (var unit in unitCharges)
                    {
                        Dictionary<string, object> errorUnit = new Dictionary<string, object>();

                        if (unit.unit==null || unit.unit.Id==0)
                        {
                            errorUnit["unit"] = "Unit tidak boleh kosong";
                            errorUnitCount++;
                        }

                        if (unit.billAmount <= 0)
                        {
                            errorUnit["billAmount"] = "Tagihan harus lebih dari 0";
                            errorUnitCount++;
                        }

                        errorUnits.Add(errorUnit);
                    }

                    if (errorUnitCount > 0)
                    {
                        yield return new ValidationResult(JsonConvert.SerializeObject(errorUnits), new List<string> { "unitCharges" });
                    }
                }
            }

        }
    }
}
