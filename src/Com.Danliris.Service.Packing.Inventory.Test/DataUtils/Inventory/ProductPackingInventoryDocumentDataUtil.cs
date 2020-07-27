using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Inventory
{
    public class ProductPackingInventoryDocumentDataUtil : BaseDataUtil<ProductPackingInventoryDocumentRepository, ProductPackingInventoryDocumentModel>
    {
        public ProductPackingInventoryDocumentDataUtil(ProductPackingInventoryDocumentRepository repository) : base(repository)
        {
        }
        public override ProductPackingInventoryDocumentModel GetModel()
        {
            return new ProductPackingInventoryDocumentModel();
        }
    }
}
