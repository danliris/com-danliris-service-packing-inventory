using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDraftPackingListItem
{
    public class GarmentDraftPackingListItemViewModel : BaseViewModel//, IValidatableObject
    {
        public string RONo { get; set; }
        public string SCNo { get; set; }

        public Buyer Buyer { get; set; }
        public Buyer BuyerBrand { get; set; }
        public Section Section { get; set; }

        public Comodity Comodity { get; set; }
        public string ComodityDescription { get; set; }

        public double Quantity { get; set; }

        public UnitOfMeasurement Uom { get; set; }

        public double PriceRO { get; set; }
        public double Price { get; set; }
        public double PriceFOB { get; set; }
        public double PriceCMT { get; set; }
        public double Amount { get; set; }
        public string Valas { get; set; }

        public Unit Unit { get; set; }

        public string Article { get; set; }
        public string OrderNo { get; set; }
        public string Description { get; set; }
        public string DescriptionMd { get; set; }

        public double AVG_GW { get; set; }
        public double AVG_NW { get; set; }

        public ICollection<GarmentDraftPackingListDetailViewModel> Details { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Id == 0)
        //    {
        //        throw new NotImplementedException();

        //    }
        //    else
        //    {
        //        if (string.IsNullOrWhiteSpace(RONo))
        //        {
        //            yield return new ValidationResult("RONo tidak boleh kosong", new List<string> { "ItemsCount" });
        //        }

        //        if (string.IsNullOrEmpty(OrderNo))
        //        {
        //            yield return new ValidationResult("PO Buyer tidak boleh kosong", new List<string> { "OrderNo" });
        //        }
        //        if (Comodity == null || Comodity.Id == 0)
        //        {
        //            yield return new ValidationResult("Komoditi tidak boleh kosong", new List<string> { "Comodity" });
        //        }

        //        if (string.IsNullOrWhiteSpace(ComodityDescription))
        //        {
        //            yield return new ValidationResult("ComodityDescription tidak boleh kosong", new List<string> { "ComodityDescription" });
        //        }

        //        if (Quantity <= 0)
        //        {
        //            yield return new ValidationResult("Quantity harus lebih dari 0", new List<string> { "ComodityDescription" });
        //        }

        //        if (item.Uom == null || item.Uom.Id == 0)
        //        {
        //            errorItem["Uom"] = "Satuan tidak boleh kosong";
        //            errorItemsCount++;
        //        }

        //    }
        //}
    }

    public class GarmentDraftPackingListItemViewModels : IValidatableObject
    {
        public ICollection<GarmentDraftPackingListItemViewModel> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
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
                        errorItem["OrderNo"] = "PO Buyer tidak boleh kosong";
                        errorItemsCount++;
                    }

                    if(item.Comodity==null || item.Comodity.Id == 0)
                    {
                        errorItem["Comodity"] = "Komoditi tidak boleh kosong";
                        errorItemsCount++;
                    }

                    if (string.IsNullOrWhiteSpace(item.ComodityDescription))
                    {
                        errorItem["ComodityDescription"] = "ComodityDescription tidak boleh kosong";
                        errorItemsCount++;
                    }

                    if (item.Quantity <=0)
                    {
                        errorItem["Quantity"] = "Quantity harus lebih dari 0";
                        errorItemsCount++;
                    }

                    if (item.Uom == null || item.Uom.Id == 0)
                    {
                        errorItem["Uom"] = "Satuan tidak boleh kosong";
                        errorItemsCount++;
                    }

                    if (item.Details != null)
                    {
                        if (item.Details.Count >= 1)
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

                                if (detail.Sizes != null)
                                {
                                    if (detail.Sizes.Count >= 1)
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
                                    }
                                    
                                }

                                errorDetails.Add(errorDetail);
                            }

                            if (errorDetailsCount > 0)
                            {
                                errorItem["Details"] = errorDetails;
                                errorItemsCount++;
                            }
                        }
                        

                    }

                    errorItems.Add(errorItem);
                }

                if (errorItemsCount > 0)
                {
                    yield return new ValidationResult(JsonConvert.SerializeObject(errorItems), new List<string> { "Items" });
                }
            }
        }
    }
}

