using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ExportSalesDO
{
    public class GarmentShippingExportSalesDOModel : StandardEntity
    {

        public string ExportSalesDONo { get; private set; }
        public string InvoiceNo { get; private set; }
        public int PackingListId { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public int BuyerAgentId { get; private set; }
        public string BuyerAgentCode { get; private set; }
        public string BuyerAgentName { get; private set; }
        public string To { get; private set; }
        public string UnitName { get; private set; }
        public int UnitId { get; private set; }
        public string UnitCode { get; private set; }
        public ICollection<GarmentShippingExportSalesDOItemModel> Items { get; set; }

        public GarmentShippingExportSalesDOModel(string exportSalesDONo, string invoiceNo, int packingListId, DateTimeOffset date, int buyerAgentId, string buyerAgentCode, string buyerAgentName, string to, string unitName, int unitId, string unitCode, ICollection<GarmentShippingExportSalesDOItemModel> items)
        {
            ExportSalesDONo = exportSalesDONo;
            InvoiceNo = invoiceNo;
            PackingListId = packingListId;
            Date = date;
            BuyerAgentId = buyerAgentId;
            BuyerAgentCode = buyerAgentCode;
            BuyerAgentName = buyerAgentName;
            To = to;
            UnitName = unitName;
            UnitId = unitId;
            UnitCode = unitCode;
            Items = items;
        }

        public GarmentShippingExportSalesDOModel()
        {
        }

        public void SetDate(DateTimeOffset date, string userName, string userAgent)
        {
            if (Date != date)
            {
                Date = date;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetTo(string to, string userName, string userAgent)
        {
            if (To != to)
            {
                To = to;
                this.FlagForUpdate(userName, userAgent);
            }
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
    }
}
