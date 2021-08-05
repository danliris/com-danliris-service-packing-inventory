using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalCoverLetter;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetter
{
    public interface IGarmentLocalCoverLetterRepository : IRepository<GarmentShippingLocalCoverLetterModel>
    {
        Task<GarmentShippingLocalCoverLetterModel> ReadByLocalSalesNoteIdAsync(int localsalesnoteid);
    }
}
