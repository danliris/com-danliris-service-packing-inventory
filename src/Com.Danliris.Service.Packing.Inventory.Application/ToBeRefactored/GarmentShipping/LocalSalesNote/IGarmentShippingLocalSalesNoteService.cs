using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote
{
    public interface IGarmentShippingLocalSalesNoteService
    {
        Task<int> Create(GarmentShippingLocalSalesNoteViewModel viewModel);
        Task<GarmentShippingLocalSalesNoteViewModel> ReadById(int id);
        ListResult<GarmentShippingLocalSalesNoteViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingLocalSalesNoteViewModel viewModel);
        Task<int> Delete(int id);
        Buyer GetBuyer(int id);
        IQueryable<GarmentShippingLocalSalesNoteViewModel> ReadShippingLocalSalesNoteListNow(int month, int year);
    }
}
