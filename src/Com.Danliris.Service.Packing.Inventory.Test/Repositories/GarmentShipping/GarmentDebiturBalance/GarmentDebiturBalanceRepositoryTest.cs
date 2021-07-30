using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDebiturBalance;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentDebiturBalance;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentDebiturBalance;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentDebiturBalance
{
    public class GarmentDebiturBalanceRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentDebiturBalanceRepository, GarmentDebiturBalanceModel, GarmentDebiturBalanceDataUtil>
    {
        private const string ENTITY = "GarmentDebiturBalance";

        public GarmentDebiturBalanceRepositoryTest() : base(ENTITY)
        {
        }
    }
}
