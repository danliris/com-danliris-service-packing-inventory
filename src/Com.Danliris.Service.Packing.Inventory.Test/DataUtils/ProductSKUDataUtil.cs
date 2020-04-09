using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class ProductSKUDataUtil : BaseDataUtil<ProductSKURepository, ProductSKUModel>
    {
        public ProductSKUDataUtil(ProductSKURepository repository) : base(repository)
        {
        }

        public override ProductSKUModel GetModel()
        {
            return new ProductSKUModel("code", "com", "cpm", "des", "gra", "10", "FABRIC", "mtr", "1", "a", "a", "s");
        }

        public ProductSKUModel GetModelGreige()
        {
            return new ProductSKUModel("code", "com", "cpm", "des", "gra", "10", "GREIGE", "mtr", "1", "a", "a", "s");
        }

        public ProductSKUModel GetModelYarn()
        {
            return new ProductSKUModel("code", "com", "cpm", "des", "gra", "10", "YARN", "mtr", "1", "a", "a", "s");
        }
    }
}
