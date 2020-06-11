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
        public int PackingListId { get; set; }
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
        #endregion
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(InvoiceNo))
            {
                yield return new ValidationResult("Invoice No  tidak boleh kosong", new List<string> { "PackingListType" });
            }

            if (Date == null || Date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "Date" });
            }

            if (string.IsNullOrEmpty(CC))
            {
                yield return new ValidationResult("CC  tidak boleh kosong", new List<string> { "PackingListType" });
            }

            if (string.IsNullOrEmpty(Phone))
            {
                yield return new ValidationResult("Phone tidak boleh kosong", new List<string> { "PackingListType" });
            }

            if (string.IsNullOrEmpty(ShippedBy))
            {
                yield return new ValidationResult("ShippedBy tidak boleh kosong", new List<string> { "PackingListType" });
            }

            if (string.IsNullOrEmpty(CartonNo))
            {
                yield return new ValidationResult("Carton No tidak boleh kosong", new List<string> { "PackingListType" });
            }

            if (string.IsNullOrEmpty(PortOfDischarge))
            {
                yield return new ValidationResult("Port Of Discharge tidak boleh kosong", new List<string> { "PackingListType" });
            }

            if (string.IsNullOrEmpty(PlaceOfDelivery))
            {
                yield return new ValidationResult("Place Of Delivery tidak boleh kosong", new List<string> { "PackingListType" });
            }

            if (string.IsNullOrEmpty(FeederVessel))
            {
                yield return new ValidationResult("Feeder Vessel BY/DD tidak boleh kosong", new List<string> { "PackingListType" });
            }

            if (string.IsNullOrEmpty(OceanVessel))
            {
                yield return new ValidationResult("Ocean Vessel BY/DD tidak boleh kosong", new List<string> { "PackingListType" });
            }

            if (string.IsNullOrEmpty(Carrier))
            {
                yield return new ValidationResult("Ocean Vessel BY/DD tidak boleh kosong", new List<string> { "PackingListType" });
            }

            if (string.IsNullOrEmpty(Flight))
            {
                yield return new ValidationResult("Ocean Vessel BY/DD tidak boleh kosong", new List<string> { "PackingListType" });
            }

            if (string.IsNullOrEmpty(OceanVessel))
            {
                yield return new ValidationResult("Ocean Vessel BY/DD tidak boleh kosong", new List<string> { "PackingListType" });
            }
        }
    }
}
