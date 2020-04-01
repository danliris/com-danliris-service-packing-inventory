using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ProductPacking
{
    public interface IProductPackingService
    {
        Task Create(ProductPackingFormViewModel viewModel);
        Task Update(int id, ProductPackingFormViewModel viewModel);
        Task Delete(int id);
        Task<ProductPackingModel> ReadById(int id);
        ListResult<IndexViewModel> ReadByKeyword(string keyword, string order, int page, int size);
    }
}
