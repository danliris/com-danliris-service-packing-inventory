using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CoverLetter
{
    public class GarmentCoverLetterViewModel : BaseViewModel, IValidatableObject
    {
        public int packingListId { get; set; }
        public int invoiceId { get; set; }
        public string invoiceNo { get; set; }

        public DateTimeOffset? date { get; set; }
        public EMKL emkl { get; set; }
        public string destination { get; set; }
        public string address { get; set; }
        public string pic { get; set; }
        public string attn { get; set; }
        public string phone { get; set; }
        public DateTimeOffset? bookingDate { get; set; }

        public Buyer order { get; set; }
        public double pcsQuantity { get; set; }
        public double setsQuantity { get; set; }
        public double packQuantity { get; set; }
        public double cartoonQuantity { get; set; }
        public Forwarder forwarder { get; set; }
        public string truck { get; set; }
        public string plateNumber { get; set; }
        public string driver { get; set; }
        public string containerNo { get; set; }
        public string freight { get; set; }
        public string shippingSeal { get; set; }
        public string dlSeal { get; set; }
        public string emklSeal { get; set; }
        public DateTimeOffset? exportEstimationDate { get; set; }
        public string unit { get; set; }
        public ShippingStaff shippingStaff { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(invoiceNo))
            {
                yield return new ValidationResult("Invoice No tidak boleh kosong", new List<string> { "invoiceNo" });
            }

            if (date == null || date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "date" });
            }

            if (emkl == null || emkl.Id == 0)
            {
                yield return new ValidationResult("EMKL tidak boleh kosong", new List<string> { "emkl" });
            }

            if (string.IsNullOrEmpty(destination))
            {
                yield return new ValidationResult("Tujuan tidak boleh kosong", new List<string> { "destination" });
            }

            if (string.IsNullOrEmpty(pic))
            {
                yield return new ValidationResult("PIC tidak boleh kosong", new List<string> { "pic" });
            }

            if (string.IsNullOrEmpty(address))
            {
                yield return new ValidationResult("Alamat tidak boleh kosong", new List<string> { "address" });
            }

            if (string.IsNullOrEmpty(attn))
            {
                yield return new ValidationResult("ATTN tidak boleh kosong", new List<string> { "attn" });
            }

            if (string.IsNullOrEmpty(phone))
            {
                yield return new ValidationResult("Telepon tidak boleh kosong", new List<string> { "phone" });
            }

            if (bookingDate == null || bookingDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal Booking Shipment tidak boleh kosong", new List<string> { "bookingDate" });
            }

            if (order == null || order.Id == 0)
            {
                yield return new ValidationResult("Order tidak boleh kosong", new List<string> { "order" });
            }

            if (forwarder == null || forwarder.id == 0)
            {
                yield return new ValidationResult("Forwarder tidak boleh kosong", new List<string> { "forwarder" });
            }

            if (string.IsNullOrEmpty(truck))
            {
                yield return new ValidationResult("Truck tidak boleh kosong", new List<string> { "truck" });
            }

            if (string.IsNullOrEmpty(plateNumber))
            {
                yield return new ValidationResult("No Polisi tidak boleh kosong", new List<string> { "plateNumber" });
            }

            if (string.IsNullOrEmpty(driver))
            {
                yield return new ValidationResult("Driver tidak boleh kosong", new List<string> { "driver" });
            }

            //if (string.IsNullOrEmpty(containerNo))
            //{
            //    yield return new ValidationResult("Container No tidak boleh kosong", new List<string> { "containerNo" });
            //}

            if (string.IsNullOrEmpty(freight))
            {
                yield return new ValidationResult("Freight tidak boleh kosong", new List<string> { "freight" });
            }

            if (exportEstimationDate == null || exportEstimationDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tgl Perkiraan Export tidak boleh kosong", new List<string> { "exportEstimationDate" });
            }

            if (string.IsNullOrEmpty(unit))
            {
                yield return new ValidationResult("Unit tidak boleh kosong", new List<string> { "unit" });
            }

            if (shippingStaff == null || shippingStaff.id == 0)
            {
                yield return new ValidationResult("Shipping Staff tidak boleh kosong", new List<string> { "shippingStaff" });
            }
        }
    }
}
