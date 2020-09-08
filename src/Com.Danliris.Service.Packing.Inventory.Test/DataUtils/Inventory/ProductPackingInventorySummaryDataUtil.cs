using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Inventory
{
 public   class ProductPackingInventorySummaryDataUtil : BaseDataUtil<ProductPackingInventorySummaryRepository, ProductPackingInventorySummaryModel>
    {
        public ProductPackingInventorySummaryDataUtil(ProductPackingInventorySummaryRepository repository) : base(repository)
        {
        }

        public override ProductPackingInventorySummaryModel GetModel()
        {
            return new ProductPackingInventorySummaryModel(1, 1, "storageCode", "storageName", 1, 1);
        }
    }
}
