using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPProcessType;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class IPProcessTypeRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, IPProcessTypeRepository, IPProcessTypeModel, IPProcessTypeDataUtil>
    {
        private const string Entity = "IPProcessType";
        public IPProcessTypeRepositoryTest() : base(Entity)
        {
        }
    }
}
