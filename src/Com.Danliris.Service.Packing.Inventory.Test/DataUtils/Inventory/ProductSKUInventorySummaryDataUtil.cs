using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Inventory
{
   public class ProductSKUInventorySummaryDataUtil : BaseDataUtil<ProductSKUInventorySummaryRepository, ProductSKUInventorySummaryModel>
    {
        public ProductSKUInventorySummaryDataUtil(ProductSKUInventorySummaryRepository repository) : base(repository)
        {
        }

        public override ProductSKUInventorySummaryModel GetModel()
        {
            return new ProductSKUInventorySummaryModel(1, 1, "storageCode", "storageName", 1, 1);
        }
    }
}

