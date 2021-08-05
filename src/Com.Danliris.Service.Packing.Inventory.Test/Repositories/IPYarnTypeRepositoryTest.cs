using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPYarnType;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class IPYarnTypeRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, IPYarnTypeRepository, IPYarnTypeModel, IPYarnTypeDataUtil>
    {
        private const string Entity = "IPYarnType";
        public IPYarnTypeRepositoryTest() : base(Entity)
        {
        }
    }
}
