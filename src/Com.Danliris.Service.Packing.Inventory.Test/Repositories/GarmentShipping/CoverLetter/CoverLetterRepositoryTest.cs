using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentCoverLetter;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentCoverLetter
{
    public class CoverLetterRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentCoverLetterRepository, GarmentCoverLetterModel, GarmentCoverLetterDataUtil>
    {
        private const string ENTITY = "GarmentCoverLetter";

        public CoverLetterRepositoryTest() : base(ENTITY)
        {
        }
    }
}
