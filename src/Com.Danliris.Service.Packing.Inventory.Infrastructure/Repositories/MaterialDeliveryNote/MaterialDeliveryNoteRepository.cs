using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote
{
    public class MaterialDeliveryNoteRepository: IMaterialDeliveryNoteRepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<MaterialDeliveryNoteModel> _materialDeliveryNoteDbSet;
        private readonly IIdentityProvider _identityProvider;

        public MaterialDeliveryNoteRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _materialDeliveryNoteDbSet = dbContext.Set<MaterialDeliveryNoteModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _materialDeliveryNoteDbSet.FirstOrDefault(entity => entity.Id == id);
            EntityExtension.FlagForDelete(model, _identityProvider.Username, USER_AGENT);
            _materialDeliveryNoteDbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(MaterialDeliveryNoteModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate(6);
            } while (_materialDeliveryNoteDbSet.Any(entity => entity.Code == model.Code));
            EntityExtension.FlagForCreate(model, _identityProvider.Username, USER_AGENT);
            _materialDeliveryNoteDbSet.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<MaterialDeliveryNoteModel> ReadAll()
        {
            return _materialDeliveryNoteDbSet.AsNoTracking();
        }

        public Task<MaterialDeliveryNoteModel> ReadByIdAsync(int id)
        {
            return _materialDeliveryNoteDbSet.Where(entity => entity.Id == id).FirstAsync();
        }

        public Task<int> UpdateAsync(int id, MaterialDeliveryNoteModel model)
        {
            var modelToUpdate = _materialDeliveryNoteDbSet.FirstOrDefault(entity => entity.Id == id);

            var isModified = false;
            //if (modelToUpdate.Quantity != model.Quantity)
            //{
            //    modelToUpdate.Quantity = model.Quantity;
            //    isModified = true;
            //}

            if (isModified)
            {
                EntityExtension.FlagForUpdate(model, _identityProvider.Username, USER_AGENT);
                _materialDeliveryNoteDbSet.Update(model);
            }
            return _dbContext.SaveChangesAsync();
        }
    }
}
