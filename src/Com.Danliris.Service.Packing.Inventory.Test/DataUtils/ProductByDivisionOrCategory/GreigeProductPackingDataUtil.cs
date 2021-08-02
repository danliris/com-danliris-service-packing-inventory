using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductByDivisionOrCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.ProductByDivisionOrCategory
{
    public class GreigeProductPackingDataUtil : BaseDataUtil<GreigeProductPackingRepository, GreigeProductPackingModel>
    {
        public GreigeProductPackingDataUtil(GreigeProductPackingRepository repository) : base(repository)
        {
        }

        public override GreigeProductPackingModel GetModel()
        {
            return new GreigeProductPackingModel("code",1,1,1, "uomUnit",1);
        }

        
    }
}
