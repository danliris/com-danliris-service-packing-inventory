using Com.Danliris.Service.Packing.Inventory.Application.DTOs;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking
{
    public interface IProductPackingService
    {
        Task<int> Create(FormDto form);
        Task<int> Update(int id, FormDto form);
        Task<int> Delete(int id);
        Task<ProductPackingDto> GetById(int id);
        Task<ProductPackingIndex> GetIndex(IndexQueryParam filter);
    }
}
