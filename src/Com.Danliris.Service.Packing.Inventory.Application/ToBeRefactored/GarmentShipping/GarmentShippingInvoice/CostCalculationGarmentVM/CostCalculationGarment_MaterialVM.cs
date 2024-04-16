using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice.CostCalculationGarmentVM
{
    public class CostCalculationGarment_MaterialViewModel : BaseViewModel
    {
        public int MaterialIndex { get; set; }

        public string Code { get; set; }
        public string PO_SerialNumber { get; set; }
        public string PO { get; set; }
        //public CategoryViewModel Category { get; set; }
        public int AutoIncrementNumber { get; set; }
        //public GarmentProductViewModel Product { get; set; } // product code, id
        public string Description { get; set; }
        public string ProductRemark { get; set; }
        public double? Quantity { get; set; }
        //public UOMViewModel UOMQuantity { get; set; }
        public double? Price { get; set; }
        //public UOMViewModel UOMPrice { get; set; }
        public double? Conversion { get; set; }
        public double Total { get; set; }
        public bool isFabricCM { get; set; }
        public double? CM_Price { get; set; }
        public double? ShippingFeePortion { get; set; }
        public double TotalShippingFee { get; set; }
        public double BudgetQuantity { get; set; }
        public string Information { get; set; }
        public bool IsPosted { get; set; }

        public bool? IsPRMaster { get; set; } // Terisi waktu validasi RO, cek apakah barang dibuat PR Master

        public long PRMasterId { get; set; }
        public long PRMasterItemId { get; set; }
        public string POMaster { get; set; }

        public double AvailableQuantity { get; set; } // untuk validasi
    }
}
