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

        public bool IsUsed { get; private set; }

        public GarmentPackingListModel()
        {
            Items = new HashSet<GarmentPackingListItemModel>();
            Measurements = new HashSet<GarmentPackingListMeasurementModel>();
        }

        public GarmentPackingListModel(string invoiceNo, string packingListType, string invoiceType, int sectionId, string sectionCode, DateTimeOffset date, string lCNo, string issuedBy, int buyerAgentId, string buyerAgentCode, string buyerAgentName, string destination, DateTimeOffset truckingDate, DateTimeOffset exportEstimationDate, bool omzet, bool accounting, ICollection<GarmentPackingListItemModel> items, double grossWeight, double nettWeight, double totalCartons, ICollection<GarmentPackingListMeasurementModel> measurements, string shippingMark, string sideMark, string remark, bool isUsed)
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
            IsUsed = isUsed;
        }

        public void SetPackingListType(string packingListType, string userName, string userAgent)
        {
            if (PackingListType != packingListType)
            {
                PackingListType = packingListType;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetSectionId(int sectionId, string userName, string userAgent)
        {
            if (SectionId != sectionId)
            {
                SectionId = sectionId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetSectionCode(string sectionCode, string userName, string userAgent)
        {
            if (SectionCode != sectionCode)
            {
                SectionCode = sectionCode;
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

        public void SetLCNo(string lCNo, string userName, string userAgent)
        {
            if (LCNo != lCNo)
            {
                LCNo = lCNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIssuedBy(string issuedBy, string userName, string userAgent)
        {
            if (IssuedBy != issuedBy)
            {
                IssuedBy = issuedBy;
                this.FlagForUpdate(userName, userAgent);
            }
        }
        public void SetBuyerAgentId(int buyerAgentId, string userName, string userAgent)
        {
            if (BuyerAgentId != buyerAgentId)
            {
                BuyerAgentId = buyerAgentId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerAgentCode(string buyerAgentCode, string userName, string userAgent)
        {
            if (BuyerAgentCode != buyerAgentCode)
            {
                BuyerAgentCode = buyerAgentCode;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerAgentName(string buyerAgentName, string userName, string userAgent)
        {
            if (BuyerAgentName != buyerAgentName)
            {
                BuyerAgentName = buyerAgentName;
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

        public void SetTruckingDate(DateTimeOffset truckingDate, string userName, string userAgent)
        {
            if (TruckingDate != truckingDate)
            {
                TruckingDate = truckingDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetExportEstimationDate(DateTimeOffset exportEstimationDate, string userName, string userAgent)
        {
            if (ExportEstimationDate != exportEstimationDate)
            {
                ExportEstimationDate = exportEstimationDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetOmzet(bool omzet, string userName, string userAgent)
        {
            if (Omzet != omzet)
            {
                Omzet = omzet;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetAccounting(bool accounting, string userName, string userAgent)
        {
            if (Accounting != accounting)
            {
                Accounting = accounting;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        //public void SetItems(string items, string userName, string userAgent)
        //{
        //    if (Items != items)
        //    {
        //        Items = items;
        //        this.FlagForUpdate(userName, userAgent);
        //    }
        //}

        public void SetGrossWeight(double grossWeight, string userName, string userAgent)
        {
            if (GrossWeight != grossWeight)
            {
                GrossWeight = grossWeight;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetNettWeight(double nettWeight, string userName, string userAgent)
        {
            if (NettWeight != nettWeight)
            {
                NettWeight = nettWeight;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetTotalCartons(double totalCartons, string userName, string userAgent)
        {
            if (TotalCartons != totalCartons)
            {
                TotalCartons = totalCartons;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        //public void SetMeasurements(string measurements, string userName, string userAgent)
        //{
        //    if (Measurements != measurements)
        //    {
        //        Measurements = measurements;
        //        this.FlagForUpdate(userName, userAgent);
        //    }
        //}

        public void SetShippingMark(string shippingMark, string userName, string userAgent)
        {
            if (ShippingMark != shippingMark)
            {
                ShippingMark = shippingMark;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetSideMark(string sideMark, string userName, string userAgent)
        {
            if (SideMark != sideMark)
            {
                SideMark = sideMark;
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

        public void SetIsUsed(bool isUsed, string userName, string userAgent)
        {
            if (IsUsed != isUsed)
            {
                IsUsed = isUsed;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
