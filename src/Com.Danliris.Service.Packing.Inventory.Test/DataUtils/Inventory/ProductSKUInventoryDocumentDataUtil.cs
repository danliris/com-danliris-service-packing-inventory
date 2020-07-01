using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Inventory
{
    public class ProductSKUInventoryDocumentDataUtil : BaseDataUtil<ProductSKUInventoryDocumentRepository, ProductSKUInventoryDocumentModel>
    {
        public ProductSKUInventoryDocumentDataUtil(ProductSKUInventoryDocumentRepository repository) : base(repository)
        {
        }

        public override ProductSKUInventoryDocumentModel GetModel()
        {
            return new ProductSKUInventoryDocumentModel(
                "documentNo",
                DateTimeOffset.Now,
                "referenceNo",
                "referenceType",
                1,
                "storageName",
                "storageCode",
                "inventoryType",
                "remark"
                );

        }
    }
}

