using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote
{
    public class MaterialDeliveryNoteRepository : IMaterialDeliveryNoteRepository
    {
        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<MaterialDeliveryNoteModel> _materialDeliveryNoteDbSet;
        private readonly DbSet<ItemsModel> _ItemsDbSet;
        private readonly IIdentityProvider _identityProvider;

        public MaterialDeliveryNoteRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _materialDeliveryNoteDbSet = dbContext.Set<MaterialDeliveryNoteModel>();
            _ItemsDbSet = dbContext.Set<ItemsModel>();
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
                var datamod = _materialDeliveryNoteDbSet.Where(a => a.BonCode == model.BonCode).Count();

                if (datamod >= 0 && datamod < 9)
                {
                    datamod += 1;
                    model.Code = model.BonCode + $"000{datamod}";
                }
                else if (datamod >= 9 && datamod < 99)
                {
                    datamod += 1;
                    model.Code = model.BonCode + $"00{datamod}";
                }
                else if (datamod >= 999 && datamod < 9999)
                {
                    datamod += 1;
                    model.Code = model.BonCode + $"0{datamod}";
                }
                else
                {
                    datamod += 1;
                    model.Code = model.BonCode + $"{datamod}";
                }

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
            return _materialDeliveryNoteDbSet.Include(entity => entity.Items).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, MaterialDeliveryNoteModel model)
        {

            var modelToUpdate = _materialDeliveryNoteDbSet.Include(s => s.Items).FirstOrDefault(entity => entity.Id == id);

            modelToUpdate.SetCode(model.BonCode);
            modelToUpdate.SetDateSJ(model.DateSJ);
            modelToUpdate.SetBonCode(model.BonCode);
            modelToUpdate.SetDateFrom(model.DateFrom);
            modelToUpdate.SetDateTo(model.DateTo);
            modelToUpdate.SetDONumberId(model.DoNumberId);
            modelToUpdate.SetDONumber(model.DONumber);
            modelToUpdate.SetFONumber(model.FONumber);
            modelToUpdate.SetReceiverId(model.ReceiverId);
            modelToUpdate.SetReceiverCode(model.ReceiverCode);
            modelToUpdate.SetReceiverName(model.ReceiverName);
            modelToUpdate.SetRemark(model.Remark);
            modelToUpdate.SetSCNumberId(model.SCNumberId);
            modelToUpdate.SetSCNumber(model.SCNumber);
            modelToUpdate.SetSenderId(model.SenderId);
            modelToUpdate.SetSenderCode(model.SenderCode);
            modelToUpdate.SetSenderName(model.SenderName);
            modelToUpdate.SetStorageid(model.StorageId);
            modelToUpdate.SetStorageCode(model.StorageCode);
            modelToUpdate.SetStorageName(model.StorageName);

            foreach (var itm in modelToUpdate.Items)
            {
                var locitem = model.Items.FirstOrDefault(s => s.Id == itm.Id);

                if (locitem != null)
                {
                    itm.Setidsop(locitem.IdSOP);
                    itm.SetNoSPP(locitem.NoSOP);
                    itm.SetMaterialName(locitem.MaterialName);
                    itm.SetInputLot(locitem.InputLot);
                    itm.SetWeightBruto(locitem.WeightBruto);
                    itm.SetWeightDOS(locitem.WeightDOS);
                    itm.SetWeightCone(locitem.WeightCone);
                    itm.SetWeightBale(locitem.WeightBale);
                    itm.SetGetTotal(locitem.GetTotal);
                }
                else
                {
                    _ItemsDbSet.Remove(itm);
                }
            }

            foreach (var newitm in model.Items.Where(s => s.Id == 0))
            {
                modelToUpdate.Items.Add(newitm);
            }

            return _dbContext.SaveChangesAsync();
        }
    }
}
