using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote
{
    public class ItemsRepository : IItemsRepository
    {

        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<ItemsModel> _ItemsDbSet;
        private readonly IIdentityProvider _identityProvider;

        Task<int> IRepository<ItemsModel>.DeleteAsync(int id)
        {
            var model = _ItemsDbSet.FirstOrDefault(entity => entity.Id == id);
            EntityExtension.FlagForDelete(model, _identityProvider.Username, USER_AGENT);
            _ItemsDbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        Task<int> IRepository<ItemsModel>.InsertAsync(ItemsModel model)
        {
            EntityExtension.FlagForCreate(model, _identityProvider.Username, USER_AGENT);
            _ItemsDbSet.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        IQueryable<ItemsModel> IRepository<ItemsModel>.ReadAll()
        {
            return _ItemsDbSet.AsNoTracking();
        }

        Task<ItemsModel> IRepository<ItemsModel>.ReadByIdAsync(int id)
        {
            return _ItemsDbSet.Where(entity => entity.Id == id).FirstAsync();
        }

        Task<int> IRepository<ItemsModel>.UpdateAsync(int id, ItemsModel model)
        {
            var modelToUpdate = _ItemsDbSet.FirstOrDefault(entity => entity.Id == id);

            var isModified = false;

            if (isModified)
            {
                EntityExtension.FlagForUpdate(model, _identityProvider.Username, USER_AGENT);
                _ItemsDbSet.Update(model);
            }
            return _dbContext.SaveChangesAsync();
        }
    }
}
