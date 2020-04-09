using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductPacking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class ProductPackingDataUtil : BaseDataUtil<ProductPackingRepository, ProductPackingModel>
    {
        public ProductPackingDataUtil(ProductPackingRepository repository) : base(repository)
        {
        }

        public override ProductPackingModel GetModel()
        {
            return new ProductPackingModel("code", "tp", 1, 1);
        }
    }
}
