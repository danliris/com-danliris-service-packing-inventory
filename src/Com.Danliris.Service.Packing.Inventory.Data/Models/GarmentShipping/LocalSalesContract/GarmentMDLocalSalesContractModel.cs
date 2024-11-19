using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalSalesContract
{
    public class GarmentMDLocalSalesContractModel : StandardEntity
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
        public int VatId { get; private set; }
        public int VatRate { get; private set; }

        public string ComodityName { get; private set; }

        public double Quantity { get; private set; }
        public double RemainingQuantity { get; private set; }

        public int UomId { get; private set; }
        public string UomUnit { get; private set; }
        public double Price { get; private set; }

        public string Remark { get; private set; }

        public decimal SubTotal { get; private set; }

        public bool IsLocalSalesDOCreated { get; private set; }


        public GarmentMDLocalSalesContractModel()
        {
        }

        public GarmentMDLocalSalesContractModel(string salesContractNo, DateTimeOffset salesContractDate, int transactionTypeId, string transactionTypeCode, string transactionTypeName, string sellerName, string sellerPosition, string sellerAddress, string sellerNPWP, int buyerId, string buyerCode, string buyerName, string buyerAddress, string buyerNPWP, bool isUseVat, int vatId, int vatRate, decimal subTotal, bool isUsed, string comodity, double quantity, double remainingQuantity, int uomId, string uomUnit, double price, string remark)
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
            VatId = vatId;
            VatRate = vatRate;
            SubTotal = subTotal;

            ComodityName = comodity;
            Quantity = quantity;
            RemainingQuantity = remainingQuantity;

            UomId = uomId;
            UomUnit = uomUnit;
            Price = price;

            Remark = remark;

            IsLocalSalesDOCreated = isUsed;
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

        public void SetVatId(int vatId, string userName, string userAgent)
        {
            if (VatId != vatId)
            {
                VatId = vatId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetVatRate(int vatRate, string userName, string userAgent)
        {
            if (VatRate != vatRate)
            {
                VatRate = vatRate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIsLocalSalesDOCreated(bool isUsed, string userName, string userAgent)
        {
            if (IsLocalSalesDOCreated != isUsed)
            {
                IsLocalSalesDOCreated = isUsed;
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

        public void SetComodityName(string comodity, string userName, string userAgent)
        {
            if (ComodityName != comodity)
            {
                ComodityName = comodity;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetQuantity(double quantity, string userName, string userAgent)
        {
            if (Quantity != quantity)
            {
                Quantity = quantity;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetRemainingQuantity(double remainingQuantity, string userName, string userAgent)
        {
            if (RemainingQuantity != remainingQuantity)
            {
                RemainingQuantity = remainingQuantity;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUomId(int uomId, string userName, string userAgent)
        {
            if (UomId != uomId)
            {
                UomId = uomId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUomUnit(string uomUnit, string userName, string userAgent)
        {
            if (UomUnit != uomUnit)
            {
                UomUnit = uomUnit;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPrice(double price, string userName, string userAgent)
        {
            if (Price != price)
            {
                Price = price;
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