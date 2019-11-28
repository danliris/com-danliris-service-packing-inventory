using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Data
{
    public interface IRepository<TModel>
    {
        Task<int> DeleteAsync(int id);
        Task<int> InsertAsync(TModel model);
        IQueryable<TModel> ReadAll();
        Task<TModel> ReadByIdAsync(int id);
        Task<int> UpdateAsync(int id, TModel model);
    }
}