using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter
{
    public class GarmentLocalCoverLetterViewModel : BaseViewModel, IValidatableObject
    {
        public int localSalesNoteId { get; set; }
        public string noteNo { get; set; }
        public string localCoverLetterNo { get; set; }
        public DateTimeOffset? date { get; set; }
        public Buyer buyer { get; set; }
        public string remark { get; set; }
        public string bcNo { get; set; }
        public DateTimeOffset? bcdate { get; set; }
        public string truck { get; set; }
        public string plateNumber { get; set; }
        public string driver { get; set; }
        public ShippingStaff shippingStaff { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(noteNo) || localSalesNoteId == 0)
            {
                yield return new ValidationResult("No Nota Penjualan tidak boleh kosong", new List<string> { "localSalesNote" });
            }

            if (date == null || date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "date" });
            }

            if (bcdate == null || bcdate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal BC tidak boleh kosong", new List<string> { "bcdate" });
            }

            if (buyer == null || buyer.Id == 0)
            {
                yield return new ValidationResult("Buyer tidak boleh kosong", new List<string> { "buyer" });
            }

            if (string.IsNullOrEmpty(truck))
            {
                yield return new ValidationResult("Truck tidak boleh kosong", new List<string> { "truck" });
            }

            if (string.IsNullOrEmpty(bcNo))
            {
                yield return new ValidationResult("No BC tidak boleh kosong", new List<string> { "bcNo" });
            }

            if (string.IsNullOrEmpty(plateNumber))
            {
                yield return new ValidationResult("No Polisi tidak boleh kosong", new List<string> { "plateNumber" });
            }

            if (string.IsNullOrEmpty(driver))
            {
                yield return new ValidationResult("Driver tidak boleh kosong", new List<string> { "driver" });
            }

            if (shippingStaff == null || shippingStaff.id == 0)
            {
                yield return new ValidationResult("Shipping Staff tidak boleh kosong", new List<string> { "shippingStaff" });
            }
        }
    }
}
