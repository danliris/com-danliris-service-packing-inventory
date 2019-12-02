using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;

namespace Com.Danliris.Service.Packing.Inventory.Application.ProductSKU
{
    public interface IProductSKUService
    {
        Task<int> Create(CreateProductSKUViewModel viewModel);
        Task<int> Delete(int id);
        Task<ListResult<ProductSKUModel>> ReadByQuery(int page, int size, string keyword);
        Task<ProductSKUModel> ReadById(int id);
    }
}