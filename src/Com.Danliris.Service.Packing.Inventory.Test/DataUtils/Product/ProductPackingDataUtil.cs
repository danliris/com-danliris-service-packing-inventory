using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Product
{
    public class ProductPackingDataUtil : BaseDataUtil<ProductPackingRepository, ProductPackingModel>
    {
        public ProductPackingDataUtil(ProductPackingRepository repository) : base(repository)
        {
        }

        public override ProductPackingModel GetModel()
        {
            return new ProductPackingModel(1,1,1,"code","name",1,1);
        }
      
    }
}
