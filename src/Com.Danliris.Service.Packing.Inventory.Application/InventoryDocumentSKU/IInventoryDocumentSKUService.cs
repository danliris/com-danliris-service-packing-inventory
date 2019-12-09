using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentSKU
{
    public interface IInventoryDocumentSKUService
    {
        Task Create(CreateInventoryDocumentSKUViewModel viewModel);
        ListResult<IndexViewModel> ReadByKeyword(string keyword, int page, int size);
        Task<InventoryDocumentSKUModel> ReadById(int id);
    }
}