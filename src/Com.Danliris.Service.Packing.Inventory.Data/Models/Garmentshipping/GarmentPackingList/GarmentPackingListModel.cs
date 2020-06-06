using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList
{
    public class GarmentPackingListModel : StandardEntity
    {
        #region Description

        public string InvoiceNo { get; private set; }
        public string PackingListType { get; private set; }
        public string InvoiceType { get; private set; }
        public int SectionId { get; private set; }
        public string SectionCode { get; private set; }
        public DateTimeOffset Date { get; private set; }

        public string LCNo { get; private set; }
        public string IssuedBy { get; private set; }

        public int BuyerAgentId { get; private set; }
        public string BuyerAgentCode { get; private set; }
        public string BuyerAgentName { get; private set; }

        public string Destination { get; private set; }

        public DateTimeOffset TruckingDate { get; private set; }
        public DateTimeOffset ExportEstimationDate { get; private set; }

        public bool Omzet { get; private set; }
        public bool Accounting { get; private set; }

        public ICollection<GarmentPackingListItemModel> Items { get; private set; }

        #endregion

        #region Measurement

        public double GrossWeight { get; private set; }
        public double NettWeight { get; private set; }
        public double TotalCartons { get; private set; }
        public ICollection<GarmentPackingListMeasurementModel> Measurements { get; private set; }

        #endregion

        #region Mark

        public string ShippingMark { get; private set; }
        public string SideMark { get; private set; }
        public string Remark { get; private set; }

        #endregion

        public GarmentPackingListModel()
        {
            Items = new HashSet<GarmentPackingListItemModel>();
            Measurements = new HashSet<GarmentPackingListMeasurementModel>();
        }

        public GarmentPackingListModel(string invoiceNo, string packingListType, string invoiceType, int sectionId, string sectionCode, DateTimeOffset date, string lCNo, string issuedBy, int buyerAgentId, string buyerAgentCode, string buyerAgentName, string destination, DateTimeOffset truckingDate, DateTimeOffset exportEstimationDate, bool omzet, bool accounting, ICollection<GarmentPackingListItemModel> items, double grossWeight, double nettWeight, double totalCartons, ICollection<GarmentPackingListMeasurementModel> measurements, string shippingMark, string sideMark, string remark)
        {
            InvoiceNo = invoiceNo;
            PackingListType = packingListType;
            InvoiceType = invoiceType;
            SectionId = sectionId;
            SectionCode = sectionCode;
            Date = date;
            LCNo = lCNo;
            IssuedBy = issuedBy;
            BuyerAgentId = buyerAgentId;
            BuyerAgentCode = buyerAgentCode;
            BuyerAgentName = buyerAgentName;
            Destination = destination;
            TruckingDate = truckingDate;
            ExportEstimationDate = exportEstimationDate;
            Omzet = omzet;
            Accounting = accounting;
            Items = items;
            GrossWeight = grossWeight;
            NettWeight = nettWeight;
            TotalCartons = totalCartons;
            Measurements = measurements;
            ShippingMark = shippingMark;
            SideMark = sideMark;
            Remark = remark;
        }

        public void SetPackingListType(string packingListType, string userName, string userAgent)
        {
            if (PackingListType != packingListType)
            {
                PackingListType = packingListType;
                this.FlagForUpdate(userName, userAgent);
            }
        }

    }
}
