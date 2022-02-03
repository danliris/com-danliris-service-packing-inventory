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

        public string PaymentTerm { get; private set; }
        public string LCNo { get; private set; }
        public DateTimeOffset LCDate { get; private set; }
        public string IssuedBy { get; private set; }

        public int BuyerAgentId { get; private set; }
        public string BuyerAgentCode { get; private set; }
        public string BuyerAgentName { get; private set; }

        public string Destination { get; private set; }
        public string FinalDestination { get; private set; }

        public string ShipmentMode { get; private set; }

        public DateTimeOffset TruckingDate { get; private set; }
        public DateTimeOffset TruckingEstimationDate { get; private set; }
        public DateTimeOffset ExportEstimationDate { get; private set; }

        public bool Omzet { get; private set; }
        public bool Accounting { get; private set; }

        public string FabricCountryOrigin { get; private set; }
        public string FabricComposition { get; private set; }

        public string RemarkMd { get; private set; }

        public ICollection<GarmentPackingListItemModel> Items { get; private set; }


        public int ShippingStaffId { get; private set; }
        public string ShippingStaffName { get; private set; }

        public string Description { get; private set; }


        #endregion

        #region Measurement

        public double GrossWeight { get; private set; }
        public double NettWeight { get; private set; }
        public double NetNetWeight {get; private set;}
        public double TotalCartons { get; private set; }
        public ICollection<GarmentPackingListMeasurementModel> Measurements { get; private set; }

        public string SayUnit { get; private set; }
        public string OtherCommodity { get; private set; }

        #endregion

        #region Mark

        public string ShippingMark { get; private set; }
        public string SideMark { get; private set; }
        public string Remark { get; private set; }

        public string ShippingMarkImagePath { get; private set; }
        public string SideMarkImagePath { get; private set; }
        public string RemarkImagePath { get; private set; }

        #endregion

        public bool IsUsed { get; private set; }
        public bool IsPosted { get; private set; }
        public bool IsCostStructured { get; private set; }


        public GarmentPackingListStatusEnum Status { get; private set; }
        public ICollection<GarmentPackingListStatusActivityModel> StatusActivities { get; private set; }
        public bool IsShipping { get; private set; }
        public bool IsSampleDelivered { get; private set; }
        public bool IsSampleExpenditureGood { get; private set; }

        public GarmentPackingListModel()
        {
            Items = new HashSet<GarmentPackingListItemModel>();
            Measurements = new HashSet<GarmentPackingListMeasurementModel>();
            StatusActivities = new HashSet<GarmentPackingListStatusActivityModel>();
        }

        public GarmentPackingListModel(string invoiceNo, string packingListType, string invoiceType, int sectionId, string sectionCode, DateTimeOffset date, string paymentTerm, string lCNo, DateTimeOffset lCDate, string issuedBy, int buyerAgentId, string buyerAgentCode, string buyerAgentName, string destination, string finalDestination, string shipmentMode, DateTimeOffset truckingDate, DateTimeOffset truckingEstimationDate, DateTimeOffset exportEstimationDate, bool omzet, bool accounting, string fabricCountryOrigin, string fabricComposition, string remarkMd, ICollection<GarmentPackingListItemModel> items, double grossWeight, double nettWeight, double netNetWeight, double totalCartons, ICollection<GarmentPackingListMeasurementModel> measurements, string sayUnit, string shippingMark, string sideMark, string remark, string shippingMarkImagePath, string sideMarkImagePath, string remarkImagePath, bool isUsed, bool isPosted, int shippingStaffId, string shippingStaffName, GarmentPackingListStatusEnum status, string description, bool isCostStructured, string otherCommodity, bool isShipping, bool isSampleDelivered, bool isSampleExpenditureGood)
        {
            InvoiceNo = invoiceNo;
            PackingListType = packingListType;
            InvoiceType = invoiceType;
            SectionId = sectionId;
            SectionCode = sectionCode;
            Date = date;
            PaymentTerm = paymentTerm;
            LCNo = lCNo;
            LCDate = lCDate;
            IssuedBy = issuedBy;
            BuyerAgentId = buyerAgentId;
            BuyerAgentCode = buyerAgentCode;
            BuyerAgentName = buyerAgentName;
            Destination = destination;
            FinalDestination = finalDestination;
            ShipmentMode = shipmentMode;
            TruckingDate = truckingDate;
            TruckingEstimationDate = truckingEstimationDate;
            ExportEstimationDate = exportEstimationDate;
            Omzet = omzet;
            Accounting = accounting;
            FabricCountryOrigin = fabricCountryOrigin;
            FabricComposition = fabricComposition;
            RemarkMd = remarkMd;
            Items = items;
            GrossWeight = grossWeight;
            NettWeight = nettWeight;
            NetNetWeight = netNetWeight;
            TotalCartons = totalCartons;
            Measurements = measurements;
            SayUnit = sayUnit;
            OtherCommodity = otherCommodity;
            ShippingMark = shippingMark;
            SideMark = sideMark;
            Remark = remark;
            ShippingMarkImagePath = shippingMarkImagePath;
            SideMarkImagePath = sideMarkImagePath;
            RemarkImagePath = remarkImagePath;
            IsUsed = isUsed;
            IsPosted = isPosted;
            IsCostStructured = isCostStructured;
            Status = status;
            ShippingStaffId = shippingStaffId;
            ShippingStaffName = shippingStaffName;
            StatusActivities = new HashSet<GarmentPackingListStatusActivityModel>();
            Description = description;
            IsShipping = isShipping;
            IsSampleDelivered = isSampleDelivered;
            IsSampleExpenditureGood = isSampleExpenditureGood;
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

        public void SetPaymentTerm(string paymentTerm, string userName, string userAgent)
        {
            if (PaymentTerm != paymentTerm)
            {
                PaymentTerm = paymentTerm;
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

        public void SetLCDate(DateTimeOffset lcDate, string userName, string userAgent)
        {
            if (LCDate != lcDate)
            {
                LCDate = lcDate;
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

        public void SetFinalDestination(string finalDestination, string userName, string userAgent)
        {
            if (FinalDestination != finalDestination)
            {
                FinalDestination = finalDestination;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetShipmentMode(string shipmentMode, string userName, string userAgent)
        {
            if (ShipmentMode != shipmentMode)
            {
                ShipmentMode = shipmentMode;
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

        public void SetTruckingEstimationDate(DateTimeOffset truckingEstimationDate, string userName, string userAgent)
        {
            if (TruckingEstimationDate != truckingEstimationDate)
            {
                TruckingEstimationDate = truckingEstimationDate;
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

        public void SetFabricCountryOrigin(string fabricCountryOrigin, string userName, string userAgent)
        {
            if (FabricCountryOrigin != fabricCountryOrigin)
            {
                FabricCountryOrigin = fabricCountryOrigin;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetFabricComposition(string fabricComposition, string userName, string userAgent)
        {
            if (FabricComposition != fabricComposition)
            {
                FabricComposition = fabricComposition;
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

        public void SetNetNetWeight(double netNetWeight, string userName, string userAgent)
        {
            if (NetNetWeight != netNetWeight)
            {
                NetNetWeight = netNetWeight;
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

        public void SetSayUnit(string sayUnit, string userName, string userAgent)
        {
            if (SayUnit != sayUnit)
            {
                SayUnit = sayUnit;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetOtherCommodity(string otherCommodity, string userName, string userAgent)
        {
            if (OtherCommodity != otherCommodity)
            {
                OtherCommodity = otherCommodity;
                this.FlagForUpdate(userName, userAgent);
            }
        }

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

        public void SetRemarkMd(string remark, string userName, string userAgent)
        {
            if (RemarkMd != remark)
            {
                RemarkMd = remark;
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

        public void SetIsPosted(bool isPosted, string userName, string userAgent)
        {
            if (IsPosted != isPosted)
            {
                IsPosted = isPosted;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetShippingStaff(int shippingStaffId, string shippingStaffName, string userName, string userAgent)
        {
            if (ShippingStaffId != shippingStaffId || ShippingStaffName != shippingStaffName)
            {
                ShippingStaffId = shippingStaffId;
                ShippingStaffName = shippingStaffName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetStatus(GarmentPackingListStatusEnum status, string userName, string userAgent)
        {
            if (Status != status)
            {
                Status = status;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetShippingMarkImagePath(string value, string userName, string userAgent)
        {
            if (ShippingMarkImagePath != value)
            {
                ShippingMarkImagePath = value;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetSideMarkImagePath(string value, string userName, string userAgent)
        {
            if (SideMarkImagePath != value)
            {
                SideMarkImagePath = value;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetRemarkImagePath(string value, string userName, string userAgent)
        {
            if (RemarkImagePath != value)
            {
                RemarkImagePath = value;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetDescription(string value, string userName, string userAgent)
        {
            if (Description != value)
            {
                Description = value;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIsCostStructured(bool isCostStructured, string userName, string userAgent)
        {
            if (IsCostStructured != isCostStructured)
            {
                IsCostStructured = isCostStructured;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIsShipping(bool isShipping, string userName, string userAgent)
        {
            if (IsShipping != isShipping)
            {
                IsShipping = isShipping;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIsSampleDelivered(bool isSampleDelivered, string userName, string userAgent)
        {
            if (IsSampleDelivered != isSampleDelivered)
            {
                IsSampleDelivered = isSampleDelivered;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIsSampleExpenditureGood(bool isSampleExpenditureGood, string userName, string userAgent)
        {
            if (IsSampleExpenditureGood != isSampleExpenditureGood)
            {
                IsSampleExpenditureGood = isSampleExpenditureGood;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
