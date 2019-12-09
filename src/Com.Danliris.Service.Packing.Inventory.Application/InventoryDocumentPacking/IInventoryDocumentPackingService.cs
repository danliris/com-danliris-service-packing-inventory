using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentPacking
{
    public interface IInventoryDocumentPackingService
    {
        Task Create(CreateInventoryDocumentPackingViewModel viewModel);
        ListResult<IndexViewModel> ReadByKeyword(string keyword, string order, int page, int size);
        Task<InventoryDocumentPackingModel> ReadById(int id);
    }
}