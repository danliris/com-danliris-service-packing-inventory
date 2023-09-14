using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentReceiptSubconPackingList
{
    public class GarmentReceiptSubconPackingListItemViewModel : BaseViewModel
    {
        public string RONo { get; set; }

        public string PackingOutNo { get; set; }
        public string SCNo { get; set; }

        public Buyer BuyerAgent { get; set; }
        public Buyer BuyerBrand { get; set; }
        public Section Section { get; set; }

        public Comodity Comodity { get; set; }
        public string ComodityDescription { get; set; }
        public string MarketingName { get; set; }
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

        public string Remarks { get; set; }

        public string RoType { get; set; }
        public double TotalQuantityPackingOut { get; set; }
        public double TotalQuantitySize { get; set; }
        

        public ICollection<GarmentReceiptSubconPackingListDetailViewModel> Details { get; set; }
    }
}