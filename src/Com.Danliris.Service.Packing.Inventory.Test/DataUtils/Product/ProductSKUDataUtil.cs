using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Product
{
     public  class ProductSKUDataUtil : BaseDataUtil<ProductSKURepository, ProductSKUModel>
    {
        public ProductSKUDataUtil(ProductSKURepository repository) : base(repository)
        {
        }

        public override ProductSKUModel GetModel()
        {
            return new ProductSKUModel("code", "name", 1,1, "description", true);
        }

    }
}
