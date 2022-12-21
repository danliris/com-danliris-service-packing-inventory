using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListViewModel : BaseViewModel, IValidatableObject
    {
        #region Description

        public string InvoiceNo { get; set; }
        public string PackingListType { get; set; }
        public string InvoiceType { get; set; }
        public Section Section { get; set; }
        public DateTimeOffset? Date { get; set; }

        public string PaymentTerm { get; set; }
        public string LCNo { get; set; }
        public DateTimeOffset? LCDate { get; set; }
        public string IssuedBy { get; set; }
        public Buyer BuyerAgent { get; set; }

        public string Destination { get; set; }
        public string FinalDestination { get; set; }

        public string ShipmentMode { get; set; }

        public DateTimeOffset? TruckingDate { get; set; }
        public DateTimeOffset? TruckingEstimationDate { get; set; }
        public DateTimeOffset? ExportEstimationDate { get; set; }

        public bool Omzet { get; set; }
        public bool Accounting { get; set; }

        public string FabricCountryOrigin { get; set; }
        public string FabricComposition { get; set; }

        public string RemarkMd { get; set; }

		public string SampleRemarkMd { get; set; }


		public ICollection<GarmentPackingListItemViewModel> Items { get; set; }

        public string Description { get; set; }

        #endregion

        #region Measurement

        public double GrossWeight { get; set; }
        public double NettWeight { get; set; }
        public double NetNetWeight { get; set; }
        public double TotalCartons { get; set; }
        public ICollection<GarmentPackingListMeasurementViewModel> Measurements { get; set; }

        public string SayUnit { get; set; }
        public string OtherCommodity { get; set; }

        #endregion

        #region Mark

        public string ShippingMark { get; set; }
        public string SideMark { get; set; }
        public string Remark { get; set; }

        public string ShippingMarkImagePath { get; set; }
        public string SideMarkImagePath { get; set; }
        public string RemarkImagePath { get; set; }

        public string ShippingMarkImageFile { get; set; }
        public string SideMarkImageFile { get; set; }
        public string RemarkImageFile { get; set; }

        #endregion

        public bool IsUsed { get; set; }
        public bool IsPosted { get; set; }
        public bool IsCostStructured { get; set; }

        public ShippingStaff ShippingStaff { get; set; }

        public string Status { get; set; }
        public ICollection<GarmentPackingListStatusActivityViewModel> StatusActivities { get; set; }

        public bool IsShipping { get; set; }
        public bool IsSampleDelivered { get; set; }
        public bool IsSampleExpenditureGood { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
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

            if (Section == null || Section.Id == 0)
            {
                yield return new ValidationResult("Seksi tidak boleh kosong", new List<string> { "Section" });
            }

            if (Date == null || Date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "Date" });
            }
            //else if (Date > DateTimeOffset.Now)
            //{
            //    yield return new ValidationResult("Tanggal tidak boleh lebih dari hari ini", new List<string> { "Date" });
            //}

            if (string.IsNullOrWhiteSpace(PaymentTerm))
            {
                yield return new ValidationResult("Payment Term tidak boleh kosong", new List<string> { "PaymentTerm" });
            }
            else if (PaymentTerm.ToUpper() == "LC")
            {
                if (string.IsNullOrEmpty(LCNo))
                {
                    yield return new ValidationResult("LC No tidak boleh kosong", new List<string> { "LCNo" });
                }

                if (LCDate == null || LCDate == DateTimeOffset.MinValue)
                {
                    yield return new ValidationResult("Tgl. LC harus diisi", new List<string> { "LCDate" });
                }

                if (string.IsNullOrEmpty(IssuedBy))
                {
                    yield return new ValidationResult("Issued By tidak boleh kosong", new List<string> { "IssuedBy" });
                }
            }

            if (BuyerAgent == null || BuyerAgent.Id == 0)
            {
                yield return new ValidationResult("Buyer Agent tidak boleh kosong", new List<string> { "BuyerAgent" });
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
                yield return new ValidationResult("Items tidak boleh kosong", new List<string> { "ItemsCount" });
            }
            else
            {
                int errorItemsCount = 0;
                List<Dictionary<string, object>> errorItems = new List<Dictionary<string, object>>();

                foreach (var item in Items)
                {
                    Dictionary<string, object> errorItem = new Dictionary<string, object>();

                    if (string.IsNullOrWhiteSpace(item.RONo))
                    {
                        errorItem["RONo"] = "RONo tidak boleh kosong";
                        errorItemsCount++;
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
                                //
                                // if (detail.Sizes.Sum(s => s.Quantity) != (detail.CartonQuantity * detail.QuantityPCS))
                                // {
                                //     errorDetail["TotalQtySize"] = "Harus sama dengan Total Qty";
                                //     errorDetailsCount++;
                                // }
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

            if (string.IsNullOrEmpty(OtherCommodity))
            {
                yield return new ValidationResult("Other Commodity tidak boleh kosong", new List<string> { "OtherCommodity" });
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

            if (ShippingStaff == null || ShippingStaff.id == 0)
            {
                yield return new ValidationResult("Shipping Staff tidak boleh kosong", new List<string> { "ShippingStaff" });
            }
        }
    }
}
