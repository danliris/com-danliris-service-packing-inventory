using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListDraftCopyViewModel : GarmentPackingListViewModel, IValidatableObject
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

            if (Items == null || Items.Count < 1)
            {
                    yield return new ValidationResult("Items tidak boleh kosong", new List<string> { "ItemsCount" });
            }
            else
            {
                int errorItemsCount = 0;
                List<Dictionary<string, object>> errorItems = new List<Dictionary<string, object>>();

                bool isSectionDiff = !Items.All(i => i.Section != null && Section != null && i.Section.Code == Section.Code);

                foreach (var item in Items)
                {
                    Dictionary<string, object> errorItem = new Dictionary<string, object>();

                    if (string.IsNullOrWhiteSpace(item.RONo))
                    {
                        errorItem["RONo"] = "RONo tidak boleh kosong";
                        errorItemsCount++;
                    }
                    else
                    {
                        if (isSectionDiff)
                        {
                            errorItem["Section"] = "Section harus sama semua";
                            errorItemsCount++;
                        }
                    }

                    
                    if (string.IsNullOrWhiteSpace(item. OrderNo))
                    {
                        errorItem["OrderNo"] = "PO Buyer tidak boleh kosong";
                        errorItemsCount++;
                    }

                    if (item.Details == null || item.Details.Count < 1)
                    {
                        errorItem["DetailsCount"] = "Details tidak boleh kosong";
                        errorItemsCount++;
                    }
                    else
                    {
                        int errorDetailsCount = 0;
                        List<Dictionary<string, object>> errorDetails = new List<Dictionary<string, object>>();

                        foreach (var detail in item.Details)
                        {
                            Dictionary<string, object> errorDetail = new Dictionary<string, object>();

                            if (string.IsNullOrWhiteSpace(detail.Colour))
                            {
                                errorDetail["Colour"] = "Colour tidak boleh kosong";
                                errorDetailsCount++;
                            }

                            errorDetails.Add(errorDetail);
                        }

                        if (errorDetailsCount > 0)
                        {
                            errorItem["Details"] = errorDetails;
                            errorItemsCount++;
                        }

                    }
                    

                    errorItems.Add(errorItem);
                }

                if (errorItemsCount > 0)
                {
                    yield return new ValidationResult(JsonConvert.SerializeObject(errorItems), new List<string> { "Items" });
                }
            }

            #endregion

            #region Measurement

            if (string.IsNullOrEmpty(SayUnit))
            {
                yield return new ValidationResult("Unit SAY tidak boleh kosong", new List<string> { "SayUnit" });
            }

            //if (Measurements != null && Measurements.Count > 0)
            //{
            //    yield return new ValidationResult("Measurements tidak boleh ada", new List<string> { "MeasurementsCount" });
            //}

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
