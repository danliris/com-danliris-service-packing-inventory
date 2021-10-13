using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDebiturBalance
{
    public class GarmentDebiturBalanceViewModel : BaseViewModel, IValidatableObject
    {
        public BuyerAgent buyerAgent { get; set; }
        public DateTimeOffset? balanceDate { get; set; }
        public decimal balanceAmount { get; set; }
        public decimal balanceAmountIDR { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (balanceDate == null || balanceDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "balanceDate" });
            }

            if (buyerAgent == null || buyerAgent.Id == 0)
            {
                yield return new ValidationResult("Buyer Agent tidak boleh kosong", new List<string> { "buyerAgent" });
            }

        }
    }
}
