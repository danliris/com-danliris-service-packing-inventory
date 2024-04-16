using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice.CostCalculationGarmentVM
{
    public class CostCalculationGarmentViewModel : BaseViewModel
    {
        public int AutoIncrementNumber { get; set; }
        public double? AccessoriesAllowance { get; set; }
        public string Article { get; set; }
        public double? CommissionPortion { get; set; }
        public double CommissionRate { get; set; }
        public DateTimeOffset ConfirmDate { get; set; }
        public string CommodityDescription { get; set; }
        public double? ConfirmPrice { get; set; }
        public List<CostCalculationGarment_MaterialViewModel> CostCalculationGarment_Materials { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public string Description { get; set; }
        public double? FabricAllowance { get; set; }
        public double? Freight { get; set; }
        public double FreightCost { get; set; }
        public string ImageFile { get; set; }
        public double Index { get; set; }
        public double? Insurance { get; set; }
        public int? LeadTime { get; set; }
        public string Code { get; set; }
        public int? RO_GarmentId { get; set; }
        public string RO_Number { get; set; }
        public string Section { get; set; }
        public string SectionName { get; set; }
        public string MarketingName { get; set; }
        public string ResponsibleName { get; set; }
        public string ApprovalCC { get; set; }
        public string ApprovalRO { get; set; }
        public string ApprovalKadiv { get; set; }
        public int? Quantity { get; set; }
        public string SizeRange { get; set; }
        public double? SMV_Cutting { get; set; }
        public double? SMV_Sewing { get; set; }
        public double? SMV_Finishing { get; set; }
        public double SMV_Total { get; set; }

        public RateViewModel Rate { get; set; }

        public double Risk { get; set; }
        public double ProductionCost { get; set; }
        public double NETFOB { get; set; }
        public double NETFOBP { get; set; }
        public string ImagePath { get; set; }
        public int? RO_RetailId { get; set; }
        public string UnitName { get; set; }

        public long? SCGarmentId { get; set; }

        public long PreSCId { get; set; }
        public string PreSCNo { get; set; }
        public string CCType { get; set; }


        public int BookingOrderId { get; set; }
        public string BookingOrderNo { get; set; }
        public double BOQuantity { get; set; }
        public int BookingOrderItemId { get; set; }


        public bool IsValidatedROSample { get; set; }
        public DateTimeOffset ValidationSampleDate { get; set; }
        public string ValidationSampleBy { get; set; }

        public bool IsValidatedROMD { get; set; }
        public DateTimeOffset ValidationMDDate { get; set; }
        public string ValidationMDBy { get; set; }

        public bool IsValidatedROPPIC { get; set; }
        public DateTimeOffset ValidationPPICDate { get; set; }
        public string ValidationPPICBy { get; set; }

        public bool IsROAccepted { get; set; }
        public DateTimeOffset ROAcceptedDate { get; set; }
        public string ROAcceptedBy { get; set; }
        public bool IsROAvailable { get; set; }
        public DateTimeOffset ROAvailableDate { get; set; }
        public string ROAvailableBy { get; set; }
        public bool IsRODistributed { get; set; }
        public DateTimeOffset RODistributionDate { get; set; }
        public string RODistributionBy { get; set; }

        public bool IsPosted { get; set; }
    }

    public class RateViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double? Value { get; set; }
    }
}
