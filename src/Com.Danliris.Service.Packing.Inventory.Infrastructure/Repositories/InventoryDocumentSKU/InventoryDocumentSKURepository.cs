using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using Com.Moonlay.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.InventoryDocumentSKU
{
    public class InventoryDocumentSKURepository : IInventoryDocumentSKURepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<InventoryDocumentSKUModel> _inventoryDocumentDbSet;
        private readonly DbSet<InventoryDocumentSKUItemModel> _inventoryDocumentItemDbSet;
        private readonly IIdentityProvider _identityProvider;

        public InventoryDocumentSKURepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _inventoryDocumentDbSet = dbContext.Set<InventoryDocumentSKUModel>();
            _inventoryDocumentItemDbSet = dbContext.Set<InventoryDocumentSKUItemModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(InventoryDocumentSKUModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate(6);
            } while (_inventoryDocumentDbSet.Any(entity => entity.Code == model.Code));

            EntityExtension.FlagForCreate(model, _identityProvider.Username, UserAgent);

            model.Items = model.Items.Select(entity =>
            {
                EntityExtension.FlagForCreate(entity, _identityProvider.Username, UserAgent);
                return entity;
            }).ToList();

            _inventoryDocumentDbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<InventoryDocumentSKUModel> ReadAll()
        {
            return _inventoryDocumentDbSet.AsNoTracking();
        }

        public Task<InventoryDocumentSKUModel> ReadByIdAsync(int id)
        {
            return _inventoryDocumentDbSet.Include(entity => entity.Items).FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateAsync(int id, InventoryDocumentSKUModel model)
        {
            throw new NotImplementedException();
        }
    }
}
