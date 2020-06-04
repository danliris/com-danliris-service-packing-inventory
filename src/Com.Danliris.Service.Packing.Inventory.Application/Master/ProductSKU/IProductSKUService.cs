using Com.Danliris.Service.Packing.Inventory.Application.DTOs;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU
{
    public interface IProductSKUService
    {
        Task<int> Create(FormDto form);
        Task<int> Update(int id, FormDto form);
        Task<int> Delete(int id);
        Task<ProductSKUDto> GetById(int id);
        Task<ProductSKUIndex> GetIndex(IndexQueryParam filter);
    }
}
