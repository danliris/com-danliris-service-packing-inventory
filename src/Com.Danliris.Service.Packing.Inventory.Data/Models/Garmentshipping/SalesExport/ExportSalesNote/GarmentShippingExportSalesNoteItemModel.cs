using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.SalesExport
{
    public class GarmentShippingExportSalesNoteItemModel : StandardEntity
    {
        public int ExportSalesNoteId { get; set; }
        public int ExportSalesContractItemId { get; set; }
        public int ProductId { get; private set; }
        public string ProductCode { get; private set; }
        public string ProductName { get; private set; }

        public double Quantity { get; private set; }

        public int UomId { get; private set; }
        public string UomUnit { get; private set; }

        public double Price { get; private set; }

        public double PackageQuantity { get; private set; }

        public int PackageUomId { get; private set; }
        public string PackageUomUnit { get; private set; }
        public string Remark { get; private set; }

        public GarmentShippingExportSalesNoteItemModel()
        {
        }

        public GarmentShippingExportSalesNoteItemModel(int exportSalesContractItemId, int productId, string productCode, string productName, double quantity, int uomId, string uomUnit, double price, double packageQuantity, int packageUomId, string packageUomUnit, string remark)
        {
            ExportSalesContractItemId = exportSalesContractItemId;
            ProductId = productId;
            ProductCode = productCode;
            ProductName = productName;
            Quantity = quantity;
            UomId = uomId;
            UomUnit = uomUnit;
            Price = price;
            PackageQuantity = packageQuantity;
            PackageUomId = packageUomId;
            PackageUomUnit = packageUomUnit;
            Remark = remark;
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

        public void SetPackageQuantity(double packageQuantity, string userName, string userAgent)
        {
            if (PackageQuantity != packageQuantity)
            {
                PackageQuantity = packageQuantity;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPackageUomId(int packageUomId, string userName, string userAgent)
        {
            if (PackageUomId != packageUomId)
            {
                PackageUomId = packageUomId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPackageUomUnit(string packageUomUnit, string userName, string userAgent)
        {
            if (PackageUomUnit != packageUomUnit)
            {
                PackageUomUnit = packageUomUnit;
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

