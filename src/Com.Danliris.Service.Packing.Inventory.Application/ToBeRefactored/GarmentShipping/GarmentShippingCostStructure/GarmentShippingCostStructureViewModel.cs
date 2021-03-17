using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingCostStructure
{
    public class GarmentShippingCostStructureViewModel : BaseViewModel, IValidatableObject
    {
        public string InvoiceNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public Comodity Comodity { get; set; }
        public string HsCode { get; set; }
        public string Destination { get; set; }
        public int FabricTypeId { get; set; }
        public string FabricType { get; set; }
        public double Amount { get; set; }

        //public ICollection<GarmentShippingCostStructureItemViewModel> Items { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(InvoiceNo))
            {
                yield return new ValidationResult("InvoiceNo Tidak boleh Kosong", new List<string> { "InvoiceNo" });
            }

            if (string.IsNullOrEmpty(HsCode))
            {
                yield return new ValidationResult("Kode HS Tidak boleh Kosong", new List<string> { "HsCode" });
            }

            if (string.IsNullOrEmpty(Destination))
            {
                yield return new ValidationResult("Destinasi Tidak boleh Kosong", new List<string> { "Destination" });
            }

            if (Date == null || Date == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal tidak boleh kosong", new List<string> { "Date" });
            }

            if (Comodity == null || Comodity.Id == 0)
            {
                yield return new ValidationResult("Nama Barang Tidak Boleh Kosong", new List<string> { "ComodityName" });
            }

            if (string.IsNullOrEmpty(FabricType))
            {
                yield return new ValidationResult("Jenis Fabric Tidak Boleh Kosong", new List<string> { "FabricType" });
            }

            if (Amount == 0)
            {
                yield return new ValidationResult("Nilai Amount Tidak Boleh Kurang dari 0", new List<string> { "Amount" });
            }

            //if (Items == null || Items.Count < 1)
            //{
            //    yield return new ValidationResult("Items tidak boleh kosong", new List<string> { "ItemsCount" });
            //}
            //else
            //{
            //    int errorItemsCount = 0;
            //    List<Dictionary<string, object>> errorItems = new List<Dictionary<string, object>>();

            //    foreach (var item in Items)
            //    {
            //        Dictionary<string, object> errorItem = new Dictionary<string, object>();

            //        if (item.SummaryPercentage == 0)
            //        {
            //            errorItem["SummaryPercentage"] = "Nilai Persentase Item tidak boleh kosong";
            //            errorItemsCount++;
            //        }

            //        if (item.SummaryValue == 0)
            //        {
            //            errorItem["SummaryValue"] = "Jumlah Nilai tidak boleh kosong";
            //            errorItemsCount++;
            //        }

            //        if (item.Details == null || item.Details.Count < 1)
            //        {
            //            errorItem["DetailsCount"] = "Details tidak boleh kosong";
            //            errorItemsCount++;
            //        }
            //        else
            //        {
            //            int errorDetailsCount = 0;
            //            List<Dictionary<string, object>> errorDetails = new List<Dictionary<string, object>>();

            //            foreach (var detail in item.Details)
            //            {
            //                Dictionary<string, object> errorDetail = new Dictionary<string, object>();

            //                if (detail.Percentage == 0)
            //                {
            //                    errorDetail["Percentage"] = "Persentase tidak boleh kosong";
            //                    errorDetailsCount++;
            //                }

            //                if(detail.Value == 0)
            //                {
            //                    errorDetail["Value"] = "Nilai tidak boleh kosong";
            //                    errorDetailsCount++;
            //                }

            //                errorDetails.Add(errorDetail);
            //            }

            //            if (errorDetailsCount > 0)
            //            {
            //                errorItem["Details"] = errorDetails;
            //                errorItemsCount++;
            //            }

            //        }

            //        errorItems.Add(errorItem);
            //    }

            //    if (errorItemsCount > 0)
            //    {
            //        yield return new ValidationResult(JsonConvert.SerializeObject(errorItems), new List<string> { "Items" });
            //    }
            //}
        }
    }
}
