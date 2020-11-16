using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDispositionRecap
{
    public class PaymentDispositionRecapViewModel : BaseViewModel, IValidatableObject
    {
        public string recapNo { get; set; }
        public DateTimeOffset? date { get; set; }
        public EMKL emkl { get; set; }
        public IEnumerable<PaymentDispositionRecapItemViewModel> items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (date == null || date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "date" });
            }

            if (emkl == null || emkl.Id == 0)
            {
                yield return new ValidationResult("EMKL tidak boleh kosong", new List<string> { "emkl" });
            }

            if (items == null || items.Count() < 1)
            {
                yield return new ValidationResult("Items tidak boleh kosong", new List<string> { "itemsCount" });
            }
            else
            {
                int errorItemsCount = 0;
                List<Dictionary<string, object>> errorItems = new List<Dictionary<string, object>>();

                foreach (var item in items)
                {
                    Dictionary<string, object> errorItem = new Dictionary<string, object>();

                    if (item.paymentDisposition == null || item.paymentDisposition.Id == 0)
                    {
                        errorItem["paymentDisposition"] = "No Disposisi tidak boleh kosong";
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
