using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductByDivisionOrCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.ProductByDivisionOrCategory
{
  public  class YarnProductSKUDataUtil : BaseDataUtil<YarnProductSKURepository, YarnProductSKUModel>
    {
        public YarnProductSKUDataUtil(YarnProductSKURepository repository) : base(repository)
        {
        }

        public override YarnProductSKUModel GetModel()
        {
            return new YarnProductSKUModel("code", "yarnType", "lotNo", "uomUnit", 1);
        }

      
    }
}
