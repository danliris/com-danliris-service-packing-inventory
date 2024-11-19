using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
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
    }
}