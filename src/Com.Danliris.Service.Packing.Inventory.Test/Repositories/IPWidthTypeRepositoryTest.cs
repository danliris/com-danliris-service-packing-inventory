using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWidthType;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class IPWidthTypeRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, IPWidthTypeRepository, IPWidthTypeModel, IPWidthTypeDataUtil>
    {
        private const string Entity = "IPWidthType";
        public IPWidthTypeRepositoryTest() : base(Entity)
        {
        }
    }
}
