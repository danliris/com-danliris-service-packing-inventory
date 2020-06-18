using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductByDivisionOrCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.ProductByDivisionOrCategory
{
    public class FabricProductPackingDataUtil : BaseDataUtil<FabricProductPackingRepository, FabricProductPackingModel>
    {
        public FabricProductPackingDataUtil(FabricProductPackingRepository repository) : base(repository)
        {
        }

        public override FabricProductPackingModel GetModel()
        {
            return new FabricProductPackingModel("code",1,1,1,1, 1);
        }

    }
}
