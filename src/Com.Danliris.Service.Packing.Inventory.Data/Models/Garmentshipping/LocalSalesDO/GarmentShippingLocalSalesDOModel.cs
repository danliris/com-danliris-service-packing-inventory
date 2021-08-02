using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesDO
{
    public class GarmentShippingLocalSalesDOModel : StandardEntity
    {
        public string LocalSalesDONo { get; private set; }
        public string LocalSalesNoteNo { get; private set; }
        public int LocalSalesNoteId { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public int BuyerId { get; private set; }
        public string BuyerCode { get; private set; }
        public string BuyerName { get; private set; }
        public string To { get; private set; }
        public string StorageDivision { get; private set; }
        public string Remark { get; private set; }
        public ICollection<GarmentShippingLocalSalesDOItemModel> Items { get; set; }
        public GarmentShippingLocalSalesDOModel(string localSalesDONo, string localSalesNoteNo, int localSalesNoteId, DateTimeOffset date, int buyerId, string buyerCode, string buyerName, string to, string storageDivision, string remark, ICollection<GarmentShippingLocalSalesDOItemModel> items)
        {
            LocalSalesDONo = localSalesDONo;
            LocalSalesNoteNo = localSalesNoteNo;
            LocalSalesNoteId = localSalesNoteId;
            Date = date;
            BuyerId = buyerId;
            BuyerCode = buyerCode;
            BuyerName = buyerName;
            To = to;
            StorageDivision = storageDivision;
            Remark = remark;
            Items = items;
        }

        public GarmentShippingLocalSalesDOModel()
        {
        }

        public void SetLocalSalesNoteId(int localSalesNoteId, string userName, string userAgent)
        {
            if (LocalSalesNoteId != localSalesNoteId)
            {
                LocalSalesNoteId = localSalesNoteId;
                this.FlagForUpdate(userName, userAgent);
            }
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

        public void SetStorageDivision(string storageDivision, string userName, string userAgent)
        {
            if (StorageDivision != storageDivision)
            {
                StorageDivision = storageDivision;
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
