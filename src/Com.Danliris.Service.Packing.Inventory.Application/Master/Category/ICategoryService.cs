using Com.Danliris.Service.Packing.Inventory.Application.DTOs;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Category
{
    public interface ICategoryService
    {
        Task<int> Create(FormDto form);
        Task<int> Upsert(FormDto form);
        Task<int> Update(int id, FormDto form);
        Task<int> Delete(int id);
        Task<CategoryDto> GetById(int id);
        Task<CategoryIndex> GetIndex(IndexQueryParam filter);
    }
}
