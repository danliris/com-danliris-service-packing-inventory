using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.SKUInventoryDocument
{
    public class SKUInventoryDocumentRepository : ISKUInventoryDocumentRepository
    {
        public SKUInventoryDocumentRepository(PackingInventoryDbContext dbContext)
        {

        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> InsertAsync(SKUInventoryDocumentModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SKUInventoryDocumentModel>> ReadAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<SKUInventoryDocumentModel> ReadByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateAsync(int id, SKUInventoryDocumentModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}