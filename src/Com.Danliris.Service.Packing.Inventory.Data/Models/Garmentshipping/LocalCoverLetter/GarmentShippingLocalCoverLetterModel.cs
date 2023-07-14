﻿using Com.Moonlay.Models;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalCoverLetter
{
    public class GarmentShippingLocalCoverLetterModel : StandardEntity
    {
        public int LocalSalesNoteId { get; private set; }
        public string NoteNo { get; private set; }
        public string LocalCoverLetterNo { get; private set; }

        public DateTimeOffset Date { get; private set; }
        public int BuyerId { get; private set; }
        public string BuyerCode { get; private set; }
        public string BuyerName { get; private set; }
        public string BuyerAdddress { get; private set; }
        public string Remark { get; private set; }
        public string BCNo { get; private set; }
        public DateTimeOffset BCDate { get; private set; }
        public string Truck { get; private set; }
        public string PlateNumber { get; private set; }
        public string Driver { get; private set; }

        public int ShippingStaffId { get; private set; }
        public string ShippingStaffName { get; private set; }

        public GarmentShippingLocalCoverLetterModel()
        {
        }

        public GarmentShippingLocalCoverLetterModel(int localSalesNoteId, string noteNo, string localCoverLetterNo, DateTimeOffset date, int buyerId, string buyerCode, string buyerName, string buyerAdddress, string remark, string bcNo, DateTimeOffset bcDate, string truck, string plateNumber, string driver, int shippingStaffId, string shippingStaffName)
        {
            LocalSalesNoteId = localSalesNoteId;
            NoteNo = noteNo;
            LocalCoverLetterNo = localCoverLetterNo;
            Date = date;
            BuyerId = buyerId;
            BuyerCode = buyerCode;
            BuyerName = buyerName;
            BuyerAdddress = buyerAdddress;
            Remark = remark;
            BCNo = bcNo;
            BCDate = bcDate;
            Truck = truck;
            PlateNumber = plateNumber;
            Driver = driver;
            ShippingStaffId = shippingStaffId;
            ShippingStaffName = shippingStaffName;
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

        public void SetBCDate(DateTimeOffset bcDate, string userName, string userAgent)
        {
            if (BCDate != bcDate)
            {
                BCDate = bcDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBCNo(string bcNo, string userName, string userAgent)
        {
            if (BCNo != bcNo)
            {
                BCNo = bcNo;
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

        public void SetTruck(string truck, string userName, string userAgent)
        {
            if (Truck != truck)
            {
                Truck = truck;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPlateNumber(string plateNumber, string userName, string userAgent)
        {
            if (PlateNumber != plateNumber)
            {
                PlateNumber = plateNumber;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetDriver(string driver, string userName, string userAgent)
        {
            if (Driver != driver)
            {
                Driver = driver;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetShippingStaffId(int shippingStaffId, string userName, string userAgent)
        {
            if (ShippingStaffId != shippingStaffId)
            {
                ShippingStaffId = shippingStaffId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetShippingStaffName(string shippingStaffName, string userName, string userAgent)
        {
            if (ShippingStaffName != shippingStaffName)
            {
                ShippingStaffName = shippingStaffName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

    }
}
