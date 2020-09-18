using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract
{
    public class GarmentShippingLocalSalesContractItemModel : StandardEntity
    {

        public int LocalSalesContractId { get; set; }
        public int ProductId { get; private set; }
        public string ProductCode { get; private set; }
        public string ProductName { get; private set; }

        public double Quantity { get; private set; }

        public int UomId { get; private set; }
        public string UomUnit { get; private set; }

        public double Price { get; private set; }

        public GarmentShippingLocalSalesContractItemModel(int productId, string productCode, string productName, double quantity, int uomId, string uomUnit, double price)
        {
            ProductId = productId;
            ProductCode = productCode;
            ProductName = productName;
            Quantity = quantity;
            UomId = uomId;
            UomUnit = uomUnit;
            Price = price;
        }

        public GarmentShippingLocalSalesContractItemModel()
        {
        }
        public void SetProductId(int productId, string userName, string userAgent)
        {
            if (ProductId != productId)
            {
                ProductId = productId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetProductCode(string productCode, string userName, string userAgent)
        {
            if (ProductCode != productCode)
            {
                ProductCode = productCode;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetProductName(string productName, string userName, string userAgent)
        {
            if (ProductName != productName)
            {
                ProductName = productName;
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
    }
}
