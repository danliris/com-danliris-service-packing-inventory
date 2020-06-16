using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWarpType;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class IPWarpTypeRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, IPWarpTypeRepository, IPWarpTypeModel, IPWarpTypeDataUtil>
    {
        private const string Entity = "IPWarpType";
        public IPWarpTypeRepositoryTest() : base(Entity)
        {
        }
    }
}
