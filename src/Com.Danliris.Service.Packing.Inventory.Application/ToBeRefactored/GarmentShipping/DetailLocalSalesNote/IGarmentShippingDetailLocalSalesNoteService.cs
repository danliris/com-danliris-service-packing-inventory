using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.DetailShippingLocalSalesNote
{
    public interface IGarmentShippingDetailLocalSalesNoteService
    {
        Task<int> Create(GarmentShippingDetailLocalSalesNoteViewModel viewModel);
        Task<GarmentShippingDetailLocalSalesNoteViewModel> ReadById(int id);
        ListResult<GarmentShippingDetailLocalSalesNoteViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingDetailLocalSalesNoteViewModel viewModel);
        Task<int> Delete(int id);      
    }
}
