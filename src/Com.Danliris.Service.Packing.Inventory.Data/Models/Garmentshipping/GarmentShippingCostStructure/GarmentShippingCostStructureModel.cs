using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingCostStructure
{
    public class GarmentShippingCostStructureModel : StandardEntity
    {
        #region Header
        public int PackingListId { get; private set; }
        public string InvoiceNo { get; private set; }
        public DateTimeOffset Date { get; private set; }

        public int ComodityId { get; private set; }
        public string ComodityCode { get; private set; }
        public string ComodityName { get; private set; }

        public string HsCode { get; private set; }
        public string Destination { get; private set; }

        public int FabricTypeId { get; private set; }
        public string FabricType { get; private set; }

        public double Amount { get; private set; }

        //public ICollection<GarmentShippingCostStructureItemModel> Items { get; private set; }
        #endregion

        public GarmentShippingCostStructureModel()
        {
            //Items = new HashSet<GarmentShippingCostStructureItemModel>();
        }

        public GarmentShippingCostStructureModel(string invoiceNo, DateTimeOffset date, int comodityId, string comodityCode, string comodityName, string hsCode, string destination, int fabricTypeId, string fabrictype, double amount, int packingListId) //, ICollection<GarmentShippingCostStructureItemModel> items)
        {
            InvoiceNo = invoiceNo;
            Date = date;
            ComodityId = comodityId;
            ComodityCode = comodityCode;
            ComodityName = comodityName;
            HsCode = hsCode;
            Destination = destination;
            FabricTypeId = fabricTypeId;
            FabricType = fabrictype;
            Amount = amount;
            PackingListId = packingListId;
            //Items = items;
        }

        public void SetComodityId(int comodityId, string userName, string userAgent)
        {
            if (ComodityId != comodityId)
            {
                ComodityId = comodityId; 
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetComodityCode(string comodityCode, string userName, string userAgent)
        {
            if (ComodityCode != comodityCode)
            {
                ComodityCode = comodityCode; 
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetComodityName(string comodityName, string userName, string userAgent)
        {
            if (ComodityName != comodityName)
            {
                ComodityName = comodityName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetHsCode(string hsCode, string userName, string userAgent)
        {
            if(HsCode != hsCode)
            {
                HsCode = hsCode;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetDestination(string destination, string userName, string userAgent)
        {
            if (Destination != destination)
            {
                Destination = destination;
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

        public void SetFabricType(string fabricType, string userName, string userAgent)
        {
            if (FabricType != fabricType)
            {
                FabricType = fabricType;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPackingListId(int packingListId, string userName, string userAgent)
        {
            if (PackingListId != packingListId)
            {
                PackingListId = packingListId;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
