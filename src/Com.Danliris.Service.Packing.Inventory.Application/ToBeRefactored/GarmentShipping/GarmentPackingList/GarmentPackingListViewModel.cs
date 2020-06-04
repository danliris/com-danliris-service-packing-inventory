using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListViewModel : BaseViewModel, IValidatableObject
    {
        #region Description

        public string InvoiceNo { get; set; }
        public string InvoiceType { get; set; }
        public Section Section { get; set; }
        public DateTimeOffset? Date { get; set; }
        public string PriceType { get; set; }

        public string LCNo { get; set; }
        public string IssuedBy { get; set; }
        public string Comodity { get; set; }

        public string Destination { get; set; }

        public DateTimeOffset? TruckingDate { get; set; }
        public DateTimeOffset? ExportEstimationDate { get; set; }

        public bool Omzet { get; set; }
        public bool Accounting { get; set; }

        public List<GarmentPackingListItemViewModel> Items { get; set; }

        public double AVG_GW { get; set; }
        public double AVG_NW { get; set; }

        #endregion

        #region Measurement

        public double GrossWeight { get; set; }
        public double NettWeight { get; set; }
        public double TotalCartons { get; set; }
        public List<GarmentPackingListMeasurementViewModel> Measurements { get; set; }

        #endregion

        #region Mark

        public string ShippingMark { get; set; }
        public string SideMark { get; set; }
        public string Remark { get; set; }

        #endregion

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            #region Description

            if (string.IsNullOrEmpty(InvoiceType))
            {
                yield return new ValidationResult("Jenis Invoice tidak boleh kosong", new List<string> { "InvoiceType" });
            }

            if (Section == null || Section.Id == 0)
            {
                yield return new ValidationResult("Seksi tidak boleh kosong", new List<string> { "Section" });
            }

            if (Date == null || Date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "Date" });
            }
            else if (Date > DateTimeOffset.Now)
            {
                yield return new ValidationResult("Tanggal tidak boleh lebih dari hari ini", new List<string> { "Date" });
            }

            if (string.IsNullOrEmpty(PriceType))
            {
                yield return new ValidationResult("Price Type tidak boleh kosong", new List<string> { "PriceType" });
            }

            if (string.IsNullOrEmpty(LCNo))
            {
                yield return new ValidationResult("LC No tidak boleh kosong", new List<string> { "LCNo" });
            }

            if (string.IsNullOrEmpty(IssuedBy))
            {
                yield return new ValidationResult("Issued By tidak boleh kosong", new List<string> { "IssuedBy" });
            }

            if (string.IsNullOrEmpty(Comodity))
            {
                yield return new ValidationResult("Komoditi tidak boleh kosong", new List<string> { "Comodity" });
            }

            if (string.IsNullOrEmpty(Destination))
            {
                yield return new ValidationResult("Destination tidak boleh kosong", new List<string> { "Destination" });
            }

            if (TruckingDate == null || TruckingDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal Trucking tidak boleh kosong", new List<string> { "TruckingDate" });
            }

            if (ExportEstimationDate == null || ExportEstimationDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal Perkiraan Export tidak boleh kosong", new List<string> { "ExportEstimationDate" });
            }

            if (Items == null || Items.Count < 1)
            {
                yield return new ValidationResult("Items tidak boleh kosong", new List<string> { "Items" });
            }
            else
            {
                int errorItemsCount = 0;
                List<Dictionary<string, object>> errorItems = new List<Dictionary<string, object>>();

                foreach (var item in Items)
                {
                    Dictionary<string, object> errorItem = new Dictionary<string, object>();

                    if (item.RONo == null)
                    {
                        errorItem["RONo"] = "RONo tidak boleh kosong";
                        errorItemsCount++;
                    }

                    if (item.OrderNo == null)
                    {
                        errorItem["OrderNo"] = "Order No tidak boleh kosong";
                        errorItemsCount++;
                    }

                    if (item.Description == null)
                    {
                        errorItem["Description"] = "Description tidak boleh kosong";
                        errorItemsCount++;
                    }

                    if (item.Details == null || item.Details.Count < 1)
                    {
                        errorItem["Details"] = "Details tidak boleh kosong";
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

            if (Measurements == null || Measurements.Count < 1)
            {
                yield return new ValidationResult("Measurements tidak boleh kosong", new List<string> { "Measurements" });
            }
            else
            {
                int errorMeasurementsCount = 0;
                List<Dictionary<string, object>> errorMeasurements = new List<Dictionary<string, object>>();

                foreach (var measurement in Measurements)
                {
                    Dictionary<string, object> errorMeasurement = new Dictionary<string, object>();

                    if (measurement.Length <= 0)
                    {
                        errorMeasurement["Length"] = "Length harus lebih dari 0";
                        errorMeasurementsCount++;
                    }

                    errorMeasurements.Add(errorMeasurement);
                }

                if (errorMeasurementsCount > 0)
                {
                    yield return new ValidationResult(JsonConvert.SerializeObject(errorMeasurements), new List<string> { "Measurements" });
                }
            }

            #endregion

            #region Mark

            if (string.IsNullOrEmpty(ShippingMark))
            {
                yield return new ValidationResult("Shipping Mark tidak boleh kosong", new List<string> { "ShippingMark" });
            }

            if (string.IsNullOrEmpty(SideMark))
            {
                yield return new ValidationResult("Side Mark tidak boleh kosong", new List<string> { "SideMark" });
            }

            if (string.IsNullOrEmpty(Remark))
            {
                yield return new ValidationResult("Remark tidak boleh kosong", new List<string> { "Remark" });
            }

            #endregion
        }
    }
}
