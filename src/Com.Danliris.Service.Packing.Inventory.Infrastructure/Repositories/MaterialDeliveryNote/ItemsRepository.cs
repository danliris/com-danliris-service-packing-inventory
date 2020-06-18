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
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote
{
    public class ItemsRepository : IItemsRepository
    {

        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<ItemsModel> _ItemsDbSet;
        private readonly DbSet<MaterialDeliveryNoteModel> _MaterialDbSet;
        private readonly IIdentityProvider _identityProvider;

        public ItemsRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _ItemsDbSet = dbContext.Set<ItemsModel>();
            _MaterialDbSet = dbContext.Set<MaterialDeliveryNoteModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

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

            var modelToUpdate = _MaterialDbSet.Include(s => s.Items).FirstOrDefault(entity => entity.Id == id);

            foreach (var item in modelToUpdate.Items)
            {
                var ItemsDetail = _ItemsDbSet.FirstOrDefault(s => s.Id == item.Id);

                if (ItemsDetail != null)
                {

                    item.SetNoSPP(model.NoSPP);
                    item.SetMaterialName(model.MaterialName);
                    item.SetInputLot(model.InputLot);
                    item.SetWeightBruto(model.WeightBruto);
                    item.SetWeightDOS(model.WeightDOS);
                    item.SetWeightCone(model.WeightCone);
                    item.SetWeightBale(model.WeightBale);
                    item.SetGetTotal(model.GetTotal);
                }
                else
                {
                    _ItemsDbSet.Remove(item);
                }
            }

            foreach (var newItem in _ItemsDbSet.Where(s => s.Id == 0))
            {
                modelToUpdate.Items.Add(newItem);
            }

            return _dbContext.SaveChangesAsync();
        }
    }
}
