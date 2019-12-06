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

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.InventoryDocumentPacking
{
    public class InventoryDocumentPackingRepository : IInventoryDocumentPackingRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<InventoryDocumentPackingModel> _inventoryDocumentDbSet;
        private readonly DbSet<InventoryDocumentPackingItemModel> _inventoryDocumentItemDbSet;
        private readonly IIdentityProvider _identityProvider;

        public InventoryDocumentPackingRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _inventoryDocumentDbSet = dbContext.Set<InventoryDocumentPackingModel>();
            _inventoryDocumentItemDbSet = dbContext.Set<InventoryDocumentPackingItemModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(InventoryDocumentPackingModel model)
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

        public IQueryable<InventoryDocumentPackingModel> ReadAll()
        {
            return _inventoryDocumentDbSet.AsNoTracking();
        }

        public Task<InventoryDocumentPackingModel> ReadByIdAsync(int id)
        {
            return _inventoryDocumentDbSet.Include(entity => entity.Items).FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateAsync(int id, InventoryDocumentPackingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
