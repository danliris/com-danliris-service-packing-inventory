using System;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using System.Collections.Generic;
using System.Text;
using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductRFID
{
    public class ProductRFIDDto
    {
        public ProductRFIDDto()
        {
        }

        public ProductRFIDDto(ProductRFIDModel productRFID, ProductPackingModel productPacking, ProductSKUModel product, UnitOfMeasurementModel uom, FabricProductSKUModel fabricSKU)
        {
            LastModifiedUtc = productRFID.LastModifiedUtc;
            RFID = productRFID.RFID;
            ProductPackingCode = productPacking.Code;
            UOMUnit = uom.Unit;
            PackingSize = productPacking.PackingSize;
            ProductSKUName = product.Name;
            Construction = fabricSKU.MaterialName +" / "+ fabricSKU.MaterialConstructionName +" / "+fabricSKU.YarnMaterialName+" / "+ fabricSKU.FinishWidth;
            Color = fabricSKU.Color;
            Width = fabricSKU.Width;
            FinishWidth = fabricSKU.FinishWidth;
        }
        public DateTime LastModifiedUtc { get; set; }
        public string RFID { get; set; }
        public string ProductPackingCode { get; set; }
        public string UOMUnit { get; set; }
        public double PackingSize { get; set; }
        public int Id { get; set; }
        public string ProductSKUName { get; set; }
        public string Construction { get; set; }
        public string Color { get; set; }
        public string Width { get; set; }
        public string FinishWidth { get; set;  }
    }
}
