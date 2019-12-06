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
            var model = _inventoryDocumentDbSet.First(entity => entity.Id == id);
            EntityExtension.FlagForDelete(model, _identityProvider.Username, UserAgent);

            var items = _inventoryDocumentItemDbSet.Where(entity => entity.InventoryDocumentSKUId == id).ToList();
            items = items.Select(entity =>
            {
                EntityExtension.FlagForDelete(entity, _identityProvider.Username, UserAgent);

                return entity;
            }).ToList();

            _inventoryDocumentDbSet.Update(model);
            _inventoryDocumentItemDbSet.UpdateRange(items);

            return _dbContext.SaveChangesAsync();
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
            throw new NotImplementedException();
        }

        public Task<InventoryDocumentSKUModel> ReadByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(int id, InventoryDocumentSKUModel model)
        {
            throw new NotImplementedException();
        }
    }
}
