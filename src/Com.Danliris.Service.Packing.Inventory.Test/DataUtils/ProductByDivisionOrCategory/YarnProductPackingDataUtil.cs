using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductByDivisionOrCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.ProductByDivisionOrCategory
{
   public class YarnProductPackingDataUtil : BaseDataUtil<YarnProductPackingRepository, YarnProductPackingModel>
    {
        public YarnProductPackingDataUtil(YarnProductPackingRepository repository) : base(repository)
        {
        }

        public override YarnProductPackingModel GetModel()
        {
            return new YarnProductPackingModel("code", 1, 1, 1, "uomUnit", 1);
        }

       
    }
}
