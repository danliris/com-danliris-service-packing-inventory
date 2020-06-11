using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Moonlay.Models;
using Com.Danliris.Service.Packing.Inventory.Data;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNoteWeaving
{
    public class ItemsMaterialDeliveryNoteWeavingRepository : IItemsMaterialDeliveryNoteWeavingRepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<ItemsMaterialDeliveryNoteWeavingModel> _ItemsDbSet;
        private readonly DbSet<MaterialDeliveryNoteWeavingModel> _MaterialDbSet;
        private readonly IIdentityProvider _identityProvider;

        public ItemsMaterialDeliveryNoteWeavingRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _ItemsDbSet = dbContext.Set<ItemsMaterialDeliveryNoteWeavingModel>();
            _MaterialDbSet = dbContext.Set<MaterialDeliveryNoteWeavingModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _ItemsDbSet.FirstOrDefault(entity => entity.Id == id);
            EntityExtension.FlagForDelete(model, _identityProvider.Username, USER_AGENT);
            _ItemsDbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(ItemsMaterialDeliveryNoteWeavingModel model)
        {
            EntityExtension.FlagForCreate(model, _identityProvider.Username, USER_AGENT);
            _ItemsDbSet.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<ItemsMaterialDeliveryNoteWeavingModel> ReadAll()
        {
            return _ItemsDbSet.AsNoTracking();
        }

        public Task<ItemsMaterialDeliveryNoteWeavingModel> ReadByIdAsync(int id)
        {
            return _ItemsDbSet.Where(entity => entity.Id == id).FirstAsync();
        }

        public Task<int> UpdateAsync(int id, ItemsMaterialDeliveryNoteWeavingModel model)
        {
            var modelToUpdate = _MaterialDbSet.Include(s => s.ItemsMaterialDeliveryNoteWeaving).FirstOrDefault(entity => entity.Id == id);

            foreach (var item in modelToUpdate.ItemsMaterialDeliveryNoteWeaving)
            {
                var ItemsDetail = _ItemsDbSet.FirstOrDefault(s => s.Id == item.Id);

                if (ItemsDetail != null)
                {
                    item.SetItemNoSOP(model.itemNoSOP);
                    item.SetItemMaterialName(model.itemMaterialName);
                    item.SetitemGrade(model.itemGrade);
                    item.SetItemType(model.itemType);
                    item.SetinputBale(model.inputBale);
                    item.SetinputPiece(model.inputPiece);
                    item.SetinputMeter(model.inputMeter);
                    item.SetinputKg(model.inputKg);
                }
                else
                {
                    _ItemsDbSet.Remove(item);
                }
            }

            foreach (var newItem in _ItemsDbSet.Where(s => s.Id == 0))
            {
                modelToUpdate.ItemsMaterialDeliveryNoteWeaving.Add(newItem);
            }

            return _dbContext.SaveChangesAsync();
        }
    }
}
