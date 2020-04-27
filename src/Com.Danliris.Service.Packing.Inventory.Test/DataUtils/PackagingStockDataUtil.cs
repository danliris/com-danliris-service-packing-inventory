using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.PackagingStock;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class PackagingStockDataUtil : BaseDataUtil<PackagingStockRepository, PackagingStockModel>
    {
        public PackagingStockDataUtil(PackagingStockRepository repository) : base(repository)
        {

        }
        public override PackagingStockModel GetModel()
        {
            return new PackagingStockModel(1,"012","White","ROols",1123,"METER",100,true);
        }

    }
}
