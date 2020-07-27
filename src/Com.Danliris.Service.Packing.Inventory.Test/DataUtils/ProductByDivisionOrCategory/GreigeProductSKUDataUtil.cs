using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductByDivisionOrCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.ProductByDivisionOrCategory
{
    public class GreigeProductSKUDataUtil : BaseDataUtil<GreigeProductSKURepository, GreigeProductSKUModel>
    {
        public GreigeProductSKUDataUtil(GreigeProductSKURepository repository) : base(repository)
        {
        }

        public override GreigeProductSKUModel GetModel()
        {
            return new GreigeProductSKUModel("code", "wovenType", "construction", "width", "warp", "weft", "grade", "uomUnit");
        }

     
    }
}
