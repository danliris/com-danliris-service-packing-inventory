using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionViewModel : BaseViewModel, IValidatableObject
    {
        public string dispositionNo { get; set; }
        public string policyType { get; set; }
        public DateTimeOffset? paymentDate { get; set; }
        public string bankName { get; set; }
        public Insurance insurance { get; set; }
        public decimal rate { get; set; }
        public string remark { get; set; }
        public ICollection<GarmentShippingInsuranceDispositionItemViewModel> items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (paymentDate == null || paymentDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "paymentDate" });
            }
            if (policyType.ToUpper() == "PIUTANG" && rate<=0)
            {
                yield return new ValidationResult("Rate tidak boleh kosong", new List<string> { "rate" });
            }
            if (insurance == null || insurance.Id == 0)
            {
                yield return new ValidationResult("Dibayar kepada tidak boleh kosong", new List<string> { "insurance" });
            }
            if (items==null || items.Count == 0)
            {
                yield return new ValidationResult("Item  harus Diisi", new List<string> { "ItemsCount" });
            }
            else
            {

                int errorItemsCount = 0;
                List<Dictionary<string, object>> errorItems = new List<Dictionary<string, object>>();

                foreach (var item in items)
                {
                    Dictionary<string, object> errorItem = new Dictionary<string, object>();

                    if (string.IsNullOrWhiteSpace(item.policyNo))
                    {
                        errorItem["policyNo"] = "No Polis tidak boleh kosong";
                        errorItemsCount++;
                    }

                    if (item.policyDate == null || item.policyDate == DateTimeOffset.MinValue)
                    {
                        errorItem["policyDate"] = "Tgl Polis tidak boleh 0";
                        errorItemsCount++;
                    }

                    if (policyType.ToUpper() != "PIUTANG" && item.currencyRate == 0)
                    {
                        errorItem["currencyRate"] = "Kurs tidak boleh 0";
                        errorItemsCount++;
                    }

                    if (policyType.ToUpper() == "PIUTANG" && item.amount == 0)
                    {
                        errorItem["amount"] = "Amount USD tidak boleh 0";
                        errorItemsCount++;
                    }


                    errorItems.Add(errorItem);
                }
                if (errorItemsCount > 0)
                {
                    yield return new ValidationResult(JsonConvert.SerializeObject(errorItems), new List<string> { "items" });
                }


            }
        }
    }
}
