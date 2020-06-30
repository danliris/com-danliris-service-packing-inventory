using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionViewModel : BaseViewModel, IValidatableObject
    {
        public string InvoiceNo { get; set; }
        public int InvoiceId { get; set; }
        public DateTimeOffset Date { get; set; }
        public EMKL EMKL { get; set; }
        public string ATTN { get; set; }
        public string Fax { get; set; }
        public string CC { get; set; }
        public int ShippingStaffId { get; set; }
        public string ShippingStaffName { get; set; }
        public string Phone { get; set; }

        #region Detail Instruction
        public string ShippedBy { get; set; }
        public DateTimeOffset TruckingDate { get; set; }
        public string CartonNo { get; set; }
        public string PortOfDischarge { get; set; }
        public string PlaceOfDelivery { get; set; }
        public string FeederVessel { get; set; }
        public string OceanVessel { get; set; }
        public string Carrier { get; set; }
        public string Flight { get; set; }
        public string Transit { get; set; }
        public int BankAccountId { get; set; }
        public string BankAccountName { get; set; }
        public Buyer BuyerAgent { get; set; }
        public string BuyerAgentAddress { get; set; }
        public string Notify { get; set; }
        public string SpecialInstruction { get; set; }
        public DateTimeOffset? LadingDate { get; set; }
        public string LadingBill { get; set; }
        public string Freight { get; set; }
        public string Marks { get; set; }
        #endregion
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(InvoiceNo))
            {
                yield return new ValidationResult("Invoice No  tidak boleh kosong", new List<string> { "InvoiceNo" });
            }

            if (Date == null || Date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "Date" });
            }

            if (string.IsNullOrEmpty(CC))
            {
                yield return new ValidationResult("CC  tidak boleh kosong", new List<string> { "CC" });
            }

            if (string.IsNullOrEmpty(Phone))
            {
                yield return new ValidationResult("Phone tidak boleh kosong", new List<string> { "Phone" });
            }

            if (string.IsNullOrEmpty(ShippedBy))
            {
                yield return new ValidationResult("ShippedBy tidak boleh kosong", new List<string> { "ShippedBy" });
            }

            if (string.IsNullOrEmpty(CartonNo))
            {
                yield return new ValidationResult("Carton No tidak boleh kosong", new List<string> { "CartonNo" });
            }

            if (string.IsNullOrEmpty(PortOfDischarge))
            {
                yield return new ValidationResult("Port Of Discharge tidak boleh kosong", new List<string> { "PortOfDischarge" });
            }

            if (string.IsNullOrEmpty(PlaceOfDelivery))
            {
                yield return new ValidationResult("Place Of Delivery tidak boleh kosong", new List<string> { "PlaceOfDelivery" });
            }

            if (string.IsNullOrEmpty(FeederVessel))
            {
                yield return new ValidationResult("Feeder Vessel BY/DD tidak boleh kosong", new List<string> { "FeederVessel" });
            }

            if (string.IsNullOrEmpty(OceanVessel))
            {
                yield return new ValidationResult("Ocean Vessel BY/DD tidak boleh kosong", new List<string> { "OceanVessel" });
            }

            if (string.IsNullOrEmpty(Carrier))
            {
                yield return new ValidationResult("Carrier BY/DD tidak boleh kosong", new List<string> { "Carrier" });
            }

            if (string.IsNullOrEmpty(Flight))
            {
                yield return new ValidationResult("Flight tidak boleh kosong", new List<string> { "Flight" });
            }

            if (string.IsNullOrEmpty(Transit))
            {
                yield return new ValidationResult("Transit tidak boleh kosong", new List<string> { "Transit" });
            }

            if (string.IsNullOrEmpty(SpecialInstruction))
            {
                yield return new ValidationResult("Special Instruction tidak boleh kosong", new List<string> { "SpecialInstruction" });
            }

            if (string.IsNullOrEmpty(Notify))
            {
                yield return new ValidationResult("Notify tidak boleh kosong", new List<string> { "Notify" });
            }

            if (EMKL == null || EMKL.Id == 0)
            {
                yield return new ValidationResult("Applicant tidak boleh kosong", new List<string> { "EMKL" });
            }
        }
    }
}
