using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingCostStructure
{
    public class GarmentShippingCostStructureItemModel : StandardEntity
    {
        public int CostStructureId { get; private set; }
        public double SummaryValue { get; private set; }
        public double SummaryPercentage { get; private set; }
        public GarmentShippingCostStructureTypeEnum CostStructureType { get; private set; }
        public ICollection<GarmentShippingCostStructureDetailModel> Details { get; private set; }

        public GarmentShippingCostStructureItemModel()
        {
            Details = new HashSet<GarmentShippingCostStructureDetailModel>();
        }

        public GarmentShippingCostStructureItemModel(double summaryValue, double summaryPercentage, GarmentShippingCostStructureTypeEnum costStructureType, ICollection<GarmentShippingCostStructureDetailModel> details)
        {
            SummaryValue = summaryValue;
            SummaryPercentage = summaryPercentage;
            CostStructureType = costStructureType;
            Details = details;
        }
    }
}
