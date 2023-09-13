using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentReceiptSubconPackingList
{
    public class GarmentReceiptSubconPackingListModel : StandardEntity
    {
        public int LocalSalesNoteId { get; private set; }
        public string LocalSalesNoteNo { get; private set; }
        public DateTimeOffset LocalSalesNoteDate { get; private set; }

        public int LocalSalesContractId { get; private set; }
        public string LocalSalesContractNo { get; private set; }

        public int TransactionTypeId { get; set; }
        public string TransactionTypeCode { get; set; }
        public string TransactionTypeName { get; set; }

        public int BuyerId { get; private set; }
        public string BuyerCode { get; private set; }
        public string BuyerName { get; private set; }
        public string BuyerNPWP { get; private set; }
        public string PaymentTerm { get; private set; }

        public bool Omzet { get; private set; }
        public bool Accounting { get; private set; }

        public double GrossWeight { get; private set; }
        public double NettWeight { get; private set; }
        public double NetNetWeight { get; private set; }
        public double TotalCartons { get; private set; }

        public bool IsApproved { get; private set; }
        public bool IsUsed { get; private set; }
        public ICollection<GarmentReceiptSubconPackingListItemModel> Items { get; private set; }


        public GarmentReceiptSubconPackingListModel()
        {
            Items = new HashSet<GarmentReceiptSubconPackingListItemModel>();
        }

        public GarmentReceiptSubconPackingListModel(int localSalesNoteId, string localSalesNoteNo,DateTimeOffset localSalesNoteDate, int localSalesContractId, string localSalesContractNo, int transactionTypeId, string transactionTypeCode, string transactionTypeName, int buyerId, string buyerCode, string buyerName, string buyerNPWP,string paymentTerm, bool omzet, bool accounting, ICollection<GarmentReceiptSubconPackingListItemModel> items, double grossWeight, double nettWeight, double netNetWeight, double totalCartons,bool isApproved,bool isUsed)
        {
            LocalSalesNoteNo = localSalesNoteNo;
            LocalSalesNoteId = localSalesNoteId;
            LocalSalesNoteDate = localSalesNoteDate;
            LocalSalesContractId = localSalesContractId;
            LocalSalesContractNo = localSalesContractNo;
            TransactionTypeId = transactionTypeId;
            TransactionTypeCode = transactionTypeCode;
            TransactionTypeName = transactionTypeName;
            BuyerId = buyerId;
            BuyerCode = buyerCode;
            BuyerName = buyerName;
            BuyerNPWP = buyerNPWP;
            PaymentTerm = paymentTerm;
            Omzet = omzet;
            Accounting = accounting;
            Items = items;
            GrossWeight = grossWeight;
            NetNetWeight = netNetWeight;
            NettWeight = nettWeight;
            TotalCartons = totalCartons;
            IsUsed = isUsed;
            IsApproved = isApproved;
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

       

        public void SetIsUsed(bool isUsed, string userName, string userAgent)
        {
            if (IsUsed != isUsed)
            {
                IsUsed = isUsed;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIsApproved(bool isApproved, string userName, string userAgent)
        {
            if (IsApproved != isApproved)
            {
                IsApproved = isApproved;
                this.FlagForUpdate(userName, userAgent);
            }
        }
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


    }
}
