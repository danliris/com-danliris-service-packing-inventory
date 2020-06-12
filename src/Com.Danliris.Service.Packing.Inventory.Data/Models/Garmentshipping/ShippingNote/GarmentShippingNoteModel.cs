﻿using Com.Moonlay.Models;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote
{
    public class GarmentShippingNoteModel : StandardEntity
    {
        public GarmentShippingNoteTypeEnum? NoteType { get; private set; }
        public string NoteNo { get; private set; }
        public DateTimeOffset Date { get; private set; }

        public int BuyerId { get; private set; }
        public string BuyerCode { get; private set; }
        public string BuyerName { get; private set; }

        public double TotalAmount { get; private set; }

        public ICollection<GarmentShippingNoteItemModel> Items { get; private set; }

        public GarmentShippingNoteModel()
        {
        }

        public GarmentShippingNoteModel(GarmentShippingNoteTypeEnum noteType, string noteNo, DateTimeOffset date, int buyerId, string buyerCode, string buyerName, double totalAmount, ICollection<GarmentShippingNoteItemModel> items)
        {
            NoteType = noteType;
            NoteNo = noteNo;
            Date = date;
            BuyerId = buyerId;
            BuyerCode = buyerCode;
            BuyerName = buyerName;
            TotalAmount = totalAmount;
            Items = items;
        }

        public void SetDate(DateTimeOffset date, string userName, string userAgent)
        {
            if (Date != date)
            {
                Date = date;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerId(int buyerId, string userName, string userAgent)
        {
            if (BuyerId != buyerId)
            {
                BuyerId = buyerId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerCode(string buyerCode, string userName, string userAgent)
        {
            if (BuyerCode != buyerCode)
            {
                BuyerCode = buyerCode;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerName(string buyerName, string userName, string userAgent)
        {
            if (BuyerName != buyerName)
            {
                BuyerName = buyerName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetTotalAmount(double totalAmount, string userName, string userAgent)
        {
            if (TotalAmount != totalAmount)
            {
                TotalAmount = totalAmount;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
