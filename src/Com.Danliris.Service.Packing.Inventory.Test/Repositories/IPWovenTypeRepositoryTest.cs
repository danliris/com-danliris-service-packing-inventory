using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWovenType;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class IPWovenTypeRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, IPWovenTypeRepository, IPWovenTypeModel, IPWovenTypeDataUtil>
    {
        private const string Entity = "IPWarpType";
        public IPWovenTypeRepositoryTest() : base(Entity)
        {
        }
    }
}
