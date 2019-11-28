using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.PackagingInventoryDocument
{
    public class PackagingInventoryDocumentRepository : IPackagingInventoryDocumentRepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<PackagingInventoryDocumentModel> _packagingInventoryDocumentDbSet;
        private readonly DbSet<PackagingInventoryDocumentItemModel> _packagingInventoryDocumentItemDbSet;
        private readonly IIdentityProvider _identityProvider;

        public PackagingInventoryDocumentRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _packagingInventoryDocumentDbSet = dbContext.Set<PackagingInventoryDocumentModel>();
            _packagingInventoryDocumentItemDbSet = dbContext.Set<PackagingInventoryDocumentItemModel>();

            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var packagingInventoryDocument = _packagingInventoryDocumentDbSet.FirstOrDefault(entity => entity.Id == id);
            var packagingInventoryDocumentItems = _packagingInventoryDocumentItemDbSet.Where(entity => entity.PackagingInventoryDocumentId == id).ToList();

            EntityExtension.FlagForDelete(packagingInventoryDocument, _identityProvider.Username, USER_AGENT);
            packagingInventoryDocumentItems = packagingInventoryDocumentItems.Select(entity =>
            {
                EntityExtension.FlagForDelete(entity, _identityProvider.Username, USER_AGENT);
                return entity;
            }).ToList();

            _packagingInventoryDocumentDbSet.Update(packagingInventoryDocument);
            _packagingInventoryDocumentItemDbSet.UpdateRange(packagingInventoryDocumentItems);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(PackagingInventoryDocumentModel model)
        {
            do
            {
                model.DocumentNo = CodeGenerator.Generate(8);
            } while (_packagingInventoryDocumentDbSet.Any(entity => entity.DocumentNo == model.DocumentNo));

            EntityExtension.FlagForCreate(model, _identityProvider.Username, USER_AGENT);
            model.Items = model.Items.Select(entity => 
            {
                EntityExtension.FlagForCreate(entity, _identityProvider.Username, USER_AGENT);
                return entity;
            }).ToList();

            _packagingInventoryDocumentDbSet.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<PackagingInventoryDocumentModel> ReadAll()
        {
            return _packagingInventoryDocumentDbSet;
        }

        public Task<PackagingInventoryDocumentModel> ReadByIdAsync(int id)
        {
            return _packagingInventoryDocumentDbSet.Where(entity => entity.Id == id).Include(entity => entity.Items).AsNoTracking().FirstAsync();
        }

        public Task<int> UpdateAsync(int id, PackagingInventoryDocumentModel model)
        {
            throw new NotImplementedException();
        }
    }
}