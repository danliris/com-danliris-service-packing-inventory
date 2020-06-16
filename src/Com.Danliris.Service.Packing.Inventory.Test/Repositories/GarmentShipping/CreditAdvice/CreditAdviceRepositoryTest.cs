using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentCreditAdvice;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentCreditAdvice
{
    public class CreditAdviceRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingCreditAdviceRepository, GarmentShippingCreditAdviceModel, GarmentCreditAdviceDataUtil>
    {
        private const string ENTITY = "GarmentCreditAdvice";

        public CreditAdviceRepositoryTest() : base(ENTITY)
        {
        }
    }
}
