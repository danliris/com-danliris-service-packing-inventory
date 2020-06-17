using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductByDivisionOrCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.ProductByDivisionOrCategory
{
    public  class FabricProductSKUDataUtil : BaseDataUtil<FabricProductSKURepository, FabricProductSKUModel>
    {
        public FabricProductSKUDataUtil(FabricProductSKURepository repository) : base(repository)
        {
        }

        public override FabricProductSKUModel GetModel()
        {
            return new FabricProductSKUModel("code", "composition", "construction", "width", "design","A", "uomUnit",1);
        }

    }
}
