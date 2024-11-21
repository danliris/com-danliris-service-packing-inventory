using Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalMDSalesNote;
using Com.Moonlay.Models;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalMDSalesNote
{
    public class GarmentMDLocalSalesNoteItemModel : StandardEntity
    {
        public int LocalSalesNoteId { get; set; }
        public int LocalSalesContractId { get; set; }
        public string ComodityName { get; private set; }

        public double Quantity { get; private set; }

        public int UomId { get; private set; }
        public string UomUnit { get; private set; }

        public double Price { get; private set; }
        public string Remark { get; private set; }
        public ICollection<GarmentMDLocalSalesNoteDetailModel> Details { get; private set; }

        public GarmentMDLocalSalesNoteItemModel()
        {
        }

        public GarmentMDLocalSalesNoteItemModel(int localSalesContractId,string comodityName, double quantity, int uomId, string uomUnit, double price, string remark, ICollection<GarmentMDLocalSalesNoteDetailModel> details)
        {
            LocalSalesContractId = localSalesContractId;
            ComodityName = comodityName;
            Quantity = quantity;
            UomId = uomId;
            UomUnit = uomUnit;
            Price = price;
            Remark = remark;
            Details = details;
        }


        public void SetComodityName(string comodityName, string userName, string userAgent)
        {
            if (ComodityName != comodityName)
            {
                ComodityName = comodityName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

      
        public void SetQuantity(double quantity, string userName, string userAgent)
        {
            if (Quantity != quantity)
            {
                Quantity = quantity;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUomId(int uomId, string userName, string userAgent)
        {
            if (UomId != uomId)
            {
                UomId = uomId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUomUnit(string uomUnit, string userName, string userAgent)
        {
            if (UomUnit != uomUnit)
            {
                UomUnit = uomUnit;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPrice(double price, string userName, string userAgent)
        {
            if (Price != price)
            {
                Price = price;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetRemark(string remark, string userName, string userAgent)
        {
            if (Remark != remark)
            {
                Remark = remark;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}

