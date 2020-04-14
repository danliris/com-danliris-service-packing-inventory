using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentAval
{
    public interface IInventoryDocumentAvalService
    {
        Task<int> Create(int id, InventoryDocumentAvalViewModel viewModel);
        //Task<int> Update(int id, InventoryDocumentAvalViewModel viewModel);
        Task<InventoryDocumentAvalViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
    }
}