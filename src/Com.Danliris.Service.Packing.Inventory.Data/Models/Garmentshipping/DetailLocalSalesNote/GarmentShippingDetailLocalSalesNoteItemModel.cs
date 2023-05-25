using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.DetailShippingLocalSalesNote
{
    public class GarmentShippingDetailLocalSalesNoteItemModel : StandardEntity
    {
        public int LocalSalesNoteId { get; set; }
        public int UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public double Quantity { get; private set; }
        public int UomId { get; private set; }
        public string UomUnit { get; private set; }
        public double Amount { get; private set; }

        public GarmentShippingDetailLocalSalesNoteItemModel()
        {
        }

        public GarmentShippingDetailLocalSalesNoteItemModel(int localSalesNoteId, int unitId, string unitCode, string unitName, double quantity, int uomId, string uomUnit, double amount)
        {
            LocalSalesNoteId = localSalesNoteId;
            UnitId = unitId;
            UnitCode = unitCode;
            UnitName = unitName;
            Quantity = quantity;
            UomId = uomId;
            UomUnit = uomUnit;
            Amount = amount;
        }

        public void SetUnitId(int unitId, string userName, string userAgent)
        {
            if (UnitId != unitId)
            {
                UnitId = unitId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUnitCode(string unitCode, string userName, string userAgent)
        {
            if (UnitCode != unitCode)
            {
                UnitCode = unitCode;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUnitName(string unitName, string userName, string userAgent)
        {
            if (UnitName != unitName)
            {
                UnitName = unitName;
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

        public void SetAmount(double amount, string userName, string userAgent)
        {
            if (Amount != amount)
            {
                Amount = amount;
                this.FlagForUpdate(userName, userAgent);
            }
        }       
    }
}

