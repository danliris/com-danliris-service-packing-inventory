using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListMerchandiserViewModel : GarmentPackingListViewModel, IValidatableObject
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

            if (Items == null || Items.Count < 1)
            {
                yield return new ValidationResult("Items tidak boleh kosong", new List<string> { "ItemsCount" });
            }
            else
            {
                int errorItemsCount = 0;
                List<Dictionary<string, object>> errorItems = new List<Dictionary<string, object>>();

                bool isBuyerAgentDiff = !Items.All(i => i.BuyerAgent != null && BuyerAgent != null && i.BuyerAgent.Id == BuyerAgent.Id);
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
                        if (isBuyerAgentDiff)
                        {
                            errorItem["BuyerAgent"] = "Buyer Agent harus sama semua";
                            errorItemsCount++;
                        }

                        if (isSectionDiff)
                        {
                            errorItem["Section"] = "Section harus sama semua";
                            errorItemsCount++;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(item.OrderNo))
                    {
                        errorItem["OrderNo"] = "Order No tidak boleh kosong";
                        errorItemsCount++;
                    }

                    if (string.IsNullOrWhiteSpace(item.Description))
                    {
                        errorItem["Description"] = "Description tidak boleh kosong";
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

                            if (detail.Sizes == null || detail.Sizes.Count < 1)
                            {
                                errorDetail["SizesCount"] = "Sizes tidak boleh kosong";
                                errorDetailsCount++;
                            }
                            else
                            {
                                int errorSizesCount = 0;
                                List<Dictionary<string, object>> errorSizes = new List<Dictionary<string, object>>();

                                foreach (var size in detail.Sizes)
                                {
                                    Dictionary<string, object> errorSize = new Dictionary<string, object>();

                                    if (size.Size == null || size.Size.Id == 0)
                                    {
                                        errorSize["Size"] = "Size tidak boleh kosong";
                                        errorSizesCount++;
                                    }

                                    errorSizes.Add(errorSize);
                                }

                                if (errorSizesCount > 0)
                                {
                                    errorDetail["Sizes"] = errorSizes;
                                    errorDetailsCount++;
                                }

                                if (detail.Sizes.Sum(s => s.Quantity) != (detail.CartonQuantity * detail.QuantityPCS))
                                {
                                    errorDetail["TotalQtySize"] = "Harus sama dengan Total Qty";
                                    errorDetailsCount++;
                                }
                            }

                            errorDetails.Add(errorDetail);
                        }

                        if (errorDetailsCount > 0)
                        {
                            errorItem["Details"] = errorDetails;
                            errorItemsCount++;
                        }

                        //if (item.Quantity != item.Details.Sum(d => d.CartonQuantity * d.QuantityPCS))
                        //{
                        //    errorItem["totalQty"] = "Harus sama dengan Qty";
                        //    errorItemsCount++;
                        //}
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

            if (Measurements == null || Measurements.Count < 1)
            {
                yield return new ValidationResult("Measurements tidak boleh kosong", new List<string> { "MeasurementsCount" });
            }
            else
            {
                int errorMeasurementsCount = 0;
                List<Dictionary<string, object>> errorMeasurements = new List<Dictionary<string, object>>();

                foreach (var measurement in Measurements)
                {
                    Dictionary<string, object> errorMeasurement = new Dictionary<string, object>();

                    //if (measurement.Length <= 0)
                    //{
                    //    errorMeasurement["Length"] = "Length harus lebih dari 0";
                    //    errorMeasurementsCount++;
                    //}

                    errorMeasurements.Add(errorMeasurement);
                }

                if (errorMeasurementsCount > 0)
                {
                    yield return new ValidationResult(JsonConvert.SerializeObject(errorMeasurements), new List<string> { "Measurements" });
                }
            }

            if (string.IsNullOrEmpty(SayUnit))
            {
                yield return new ValidationResult("Unit SAY tidak boleh kosong", new List<string> { "SayUnit" });
            }

            #endregion

            #region Mark

            if (string.IsNullOrEmpty(ShippingMark))
            {
                yield return new ValidationResult("Shipping Mark tidak boleh kosong", new List<string> { "ShippingMark" });
            }

            //if (string.IsNullOrEmpty(SideMark))
            //{
            //    yield return new ValidationResult("Side Mark tidak boleh kosong", new List<string> { "SideMark" });
            //}

            //if (string.IsNullOrEmpty(Remark))
            //{
            //    yield return new ValidationResult("Remark tidak boleh kosong", new List<string> { "Remark" });
            //}

            #endregion
        }
    }
}
