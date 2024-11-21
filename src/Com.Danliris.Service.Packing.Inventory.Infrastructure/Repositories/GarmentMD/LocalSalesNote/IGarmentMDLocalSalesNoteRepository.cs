using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalMDSalesNote;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentMD.LocalSalesNote
{
    public interface IGarmentMDLocalSalesNoteRepository : IRepository<GarmentMDLocalSalesNoteModel>
    {
        //Task<int> ApproveShippingAsync(int id);
        //Task<int> ApproveFinanceAsync(int id);
        //Task<int> RejectFinanceAsync(int id, GarmentShippingLocalSalesNoteModel model);
        //Task<int> RejectShippingAsync(int id, GarmentShippingLocalSalesNoteModel model);
    }
}
