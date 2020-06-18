using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU
{
    public interface IProductSKURepository : IRepository<ProductSKUModel>
    {
        Task<bool> IsExist(string name);
    }
}