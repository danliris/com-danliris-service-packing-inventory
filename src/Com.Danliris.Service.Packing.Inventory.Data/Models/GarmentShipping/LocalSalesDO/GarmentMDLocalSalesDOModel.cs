using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesDO;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalSalesDO
{
    public class GarmentMDLocalSalesDOModel : StandardEntity
    {
        public string LocalSalesDONo { get; private set; }
        public string LocalSalesContractNo { get; private set; }
        public int LocalSalesContractId { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public int BuyerId { get; private set; }
        public string BuyerCode { get; private set; }
        public string BuyerName { get; private set; }
        public string To { get; private set; }
        public string StorageDivision { get; private set; }
        public string Remark { get; private set; }

        public string ComodityName { get; private set; }
        public string Description { get; private set; }
        public double Quantity { get; private set; }
        public int UomId { get; private set; }
        public string UomUnit { get; private set; }
        public double PackQuantity { get; private set; }
        public int PackUomId { get; private set; }
        public string PackUomUnit { get; private set; }
        public double GrossWeight { get; private set; }
        public double NettWeight { get; private set; }

        public GarmentMDLocalSalesDOModel()
        {
        }

        public GarmentMDLocalSalesDOModel(
            string localSalesDONo, 
            string localSalesContractNo, 
            int localSalesContractId, 
            DateTimeOffset date, 
            int buyerId, 
            string buyerCode, 
            string buyerName, 
            string to, 
            string storageDivision, 
            string remark, 
            string comodityName,
            string description,
            double quantity,
            int uomId,
            string uomUnit,
            double packQ,
            int packUomId,
            string packUomUnit,
            double grossWeight,
            double nettWeight)
        {
            LocalSalesDONo = localSalesDONo;
            LocalSalesContractNo = localSalesContractNo;
            LocalSalesContractId = localSalesContractId;
            Date = date;
            BuyerId = buyerId;
            BuyerCode = buyerCode;
            BuyerName = buyerName;
            To = to;
            StorageDivision = storageDivision;
            Remark = remark;

            ComodityName = comodityName;
            Description = description;
            Quantity = quantity;
            UomId = uomId;
            UomUnit = uomUnit;

            PackQuantity = packQ;
            PackUomId = packUomId;
            PackUomUnit = packUomUnit;

            GrossWeight = grossWeight;
            NettWeight = nettWeight;
        }

        public void SetLocalSalesContractId(int localSalesContractId, string userName, string userAgent)
        {
            if (LocalSalesContractId != localSalesContractId)
            {
                LocalSalesContractId = localSalesContractId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetLocalSalesContractNo(string localSalesContractNo, string userName, string userAgent)
        {
            if (LocalSalesContractNo != localSalesContractNo)
            {
                LocalSalesContractNo = localSalesContractNo;
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

        public void SetDescription(string description, string userName, string userAgent)
        {
            if (Description != description)
            {
                Description = description;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPackQuantity(double packQ, string userName, string userAgent)
        {
            if (PackQuantity != packQ)
            {
                PackQuantity = packQ;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPackUomId(int packUomId, string userName, string userAgent)
        {
            if (PackUomId != packUomId)
            {
                PackUomId = packUomId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPackUomUnit(string packUomUnit, string userName, string userAgent)
        {
            if (PackUomUnit != packUomUnit)
            {
                PackUomUnit = packUomUnit;
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
    }
}
