using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract
{
    public class GarmentShippingLocalSalesContractModel : StandardEntity
    {
        
        public string SalesContractNo { get; private set; }
        public DateTimeOffset SalesContractDate { get; private set; }

        public int TransactionTypeId { get; set; }
        public string TransactionTypeCode { get; set; }
        public string TransactionTypeName { get; set; }

        public string SellerName { get; private set; }
        public string SellerPosition { get; private set; }
        public string SellerAddress { get; private set; }
        public string SellerNPWP { get; private set; }

        public int BuyerId { get; private set; }
        public string BuyerCode { get; private set; }
        public string BuyerName { get; private set; }
        public string BuyerAddress { get; private set; }
        public string BuyerNPWP { get; private set; }

        public bool IsUseVat { get; private set; }

        public decimal SubTotal { get; private set; }

        public bool IsUsed { get; private set; }
        public ICollection<GarmentShippingLocalSalesContractItemModel> Items { get; private set; }

        public GarmentShippingLocalSalesContractModel()
        {
        }

        public GarmentShippingLocalSalesContractModel(string salesContractNo, DateTimeOffset salesContractDate, int transactionTypeId, string transactionTypeCode, string transactionTypeName, string sellerName, string sellerPosition, string sellerAddress, string sellerNPWP, int buyerId, string buyerCode, string buyerName, string buyerAddress, string buyerNPWP, bool isUseVat, decimal subTotal, bool isUsed, ICollection<GarmentShippingLocalSalesContractItemModel> items)
        {
            SalesContractNo = salesContractNo;
            SalesContractDate = salesContractDate;
            TransactionTypeId = transactionTypeId;
            TransactionTypeCode = transactionTypeCode;
            TransactionTypeName = transactionTypeName;
            SellerName = sellerName;
            SellerPosition = sellerPosition;
            SellerAddress = sellerAddress;
            SellerNPWP = sellerNPWP;
            BuyerId = buyerId;
            BuyerCode = buyerCode;
            BuyerName = buyerName;
            BuyerAddress = buyerAddress;
            BuyerNPWP = buyerNPWP;
            IsUseVat = isUseVat;
            SubTotal = subTotal;
            IsUsed = isUsed;
            Items = items;
        }

        public void SetSellerName(string sellerName, string userName, string userAgent)
        {
            if (SellerName != sellerName)
            {
                SellerName = sellerName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetSellerPosition(string sellerPosition, string userName, string userAgent)
        {
            if (SellerPosition != sellerPosition)
            {
                SellerPosition = sellerPosition;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetSellerAddress(string sellerAddress, string userName, string userAgent)
        {
            if (SellerAddress != sellerAddress)
            {
                SellerAddress = sellerAddress;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetSellerNPWP(string sellerNPWP, string userName, string userAgent)
        {
            if (SellerNPWP != sellerNPWP)
            {
                SellerNPWP = sellerNPWP;
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

        public void SetBuyerAddress(string buyerAddress, string userName, string userAgent)
        {
            if (BuyerAddress != buyerAddress)
            {
                BuyerAddress = buyerAddress;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerNPWP(string buyerNPWP, string userName, string userAgent)
        {
            if (BuyerNPWP != buyerNPWP)
            {
                BuyerNPWP = buyerNPWP;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIsUseVat(bool isUseVat, string userName, string userAgent)
        {
            if (IsUseVat != isUseVat)
            {
                IsUseVat = isUseVat;
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

        public void SetSubTotal(decimal subTotal, string userName, string userAgent)
        {
            if (SubTotal != subTotal)
            {
                SubTotal = subTotal;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
