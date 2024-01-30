using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalCoverLetterTS;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetterTS
{
    public interface IGarmentLocalCoverLetterTSRepository : IRepository<GarmentShippingLocalCoverLetterTSModel>
    {
        Task<GarmentShippingLocalCoverLetterTSModel> ReadByLocalSalesNoteIdAsync(int localsalesnoteid);
    }
}
