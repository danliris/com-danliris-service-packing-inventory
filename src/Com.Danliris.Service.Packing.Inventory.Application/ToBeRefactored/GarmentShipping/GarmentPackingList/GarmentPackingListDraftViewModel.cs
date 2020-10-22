using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListDraftViewModel : GarmentPackingListViewModel, IValidatableObject
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            #region Description

            if (string.IsNullOrEmpty(PackingListType))
            {
                yield return new ValidationResult("Jenis Packing List tidak boleh kosong", new List<string> { "PackingListType" });
            }

            if (string.IsNullOrEmpty(InvoiceType))
            {
                yield return new ValidationResult("Jenis Invoice tidak boleh kosong", new List<string> { "InvoiceType" });
            }

            if (Date == null || Date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "Date" });
            }

            if (BuyerAgent == null || BuyerAgent.Id == 0)
            {
                yield return new ValidationResult("Buyer Agent tidak boleh kosong", new List<string> { "BuyerAgent" });
            }

            if (string.IsNullOrEmpty(Destination))
            {
                yield return new ValidationResult("Destination tidak boleh kosong", new List<string> { "Destination" });
            }

            if (ShippingStaff == null || ShippingStaff.id == 0)
            {
                yield return new ValidationResult("Shipping Staff tidak boleh kosong", new List<string> { "ShippingStaff" });
            }

            if (Items != null && Items.Count > 0)
            {
                yield return new ValidationResult("Items tidak boleh ada", new List<string> { "ItemsCount" });
            }

            #endregion

            #region Measurement

            if (string.IsNullOrEmpty(SayUnit))
            {
                yield return new ValidationResult("Unit SAY tidak boleh kosong", new List<string> { "SayUnit" });
            }

            if (Measurements != null && Measurements.Count > 0)
            {
                yield return new ValidationResult("Measurements tidak boleh ada", new List<string> { "MeasurementsCount" });
            }

            #endregion

            #region Mark

            if (string.IsNullOrWhiteSpace(ShippingMark))
            {
                yield return new ValidationResult("Shipping Mark tidak boleh kosong", new List<string> { "ShippingMark" });
            }

            #endregion
        }
    }
}
