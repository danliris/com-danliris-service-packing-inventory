using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalCoverLetter;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentLocalCoverLetter
{
    public class LocalCoverLetterRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentLocalCoverLetterRepository, GarmentShippingLocalCoverLetterModel, GarmentLocalCoverLetterDataUtil>
    {
        private const string ENTITY = "GarmentLocalCoverLetter";

        public LocalCoverLetterRepositoryTest() : base(ENTITY)
        {
        }
    }
}
