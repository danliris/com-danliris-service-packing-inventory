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
    public class MaterialDeliveryNoteWeavingRepository : IMaterialDeliveryNoteWeavingRepository
    {

        private const string USER_AGENT = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<MaterialDeliveryNoteWeavingModel> _materialDeliveryNoteWeavingDbSet;
        private readonly DbSet<ItemsMaterialDeliveryNoteWeavingModel> _ItemsDbSet;
        private readonly IIdentityProvider _identityProvider;

        public MaterialDeliveryNoteWeavingRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _materialDeliveryNoteWeavingDbSet = dbContext.Set<MaterialDeliveryNoteWeavingModel>();
            _ItemsDbSet = dbContext.Set<ItemsMaterialDeliveryNoteWeavingModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _materialDeliveryNoteWeavingDbSet.FirstOrDefault(entity => entity.Id == id);
            EntityExtension.FlagForDelete(model, _identityProvider.Username, USER_AGENT);
            _materialDeliveryNoteWeavingDbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(MaterialDeliveryNoteWeavingModel model)
        {
            do
            {
                var datamod = _materialDeliveryNoteWeavingDbSet.Where(a => a.NumberOut == model.NumberOut).Count();

                if(datamod >= 0 && datamod < 9)
                {
                    datamod += 1;
                    model.Code = model.NumberOut + $"000{datamod}";
                }
                else if(datamod >= 9 && datamod < 99)
                {
                    datamod += 1;
                    model.Code = model.NumberOut + $"00{datamod}";
                }
                else if(datamod >= 999 && datamod < 9999)
                {
                    datamod += 1;
                    model.Code = model.NumberOut + $"0{datamod}";
                }
                else
                {
                    datamod += 1;
                    model.Code = model.NumberOut + $"{datamod}";
                }

            } while (_materialDeliveryNoteWeavingDbSet.Any(entity => entity.Code == model.Code));
            EntityExtension.FlagForCreate(model, _identityProvider.Username, USER_AGENT);
            _materialDeliveryNoteWeavingDbSet.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<MaterialDeliveryNoteWeavingModel> ReadAll()
        {
            return _materialDeliveryNoteWeavingDbSet.AsNoTracking();
        }

        public Task<MaterialDeliveryNoteWeavingModel> ReadByIdAsync(int id)
        {
            return _materialDeliveryNoteWeavingDbSet.Include(entity => entity.ItemsMaterialDeliveryNoteWeaving).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, MaterialDeliveryNoteWeavingModel model)
        {
            var modelToUpdate = _materialDeliveryNoteWeavingDbSet.Include(s => s.ItemsMaterialDeliveryNoteWeaving).FirstOrDefault(entity => entity.Id == id);

            modelToUpdate.SetCode(model.Code);
            modelToUpdate.SetDateSJ(model.DateSJ);
            modelToUpdate.SetDoSalesNumberId(model.DoSalesNumberId);
            modelToUpdate.SetDoSalesNumber(model.DoSalesNumber);
            modelToUpdate.SetSendTo(model.SendTo);
            modelToUpdate.SetUnitId(model.UnitId);
            modelToUpdate.SetUnitName(model.UnitName);
            modelToUpdate.SetBuyerId(model.BuyerId);
            modelToUpdate.SetBuyerCode(model.BuyerCode);
            modelToUpdate.SetBuyerName(model.BuyerName);
            modelToUpdate.SetNumberOut(model.NumberOut);
            modelToUpdate.SetStorageId(model.StorageId);
            modelToUpdate.SetStorageCode(model.StorageCode);
            modelToUpdate.SetStorageName(model.StorageName);
            modelToUpdate.SetRemark(model.Remark);
            //modelToUpdate.SetStorageUnit(model.StorageUnit);

            foreach (var itm in modelToUpdate.ItemsMaterialDeliveryNoteWeaving)
            {
                var locitem = model.ItemsMaterialDeliveryNoteWeaving.FirstOrDefault(s => s.Id == itm.Id);

                if (locitem != null)
                {
                    itm.SetItemNoSOP(locitem.ItemNoSOP);
                    itm.SetItemMaterialName(locitem.ItemMaterialName);
                    itm.SetitemGrade(locitem.ItemGrade);
                    itm.SetItemType(locitem.ItemType);
                    itm.SetinputBale(locitem.InputBale);
                    itm.SetinputPiece(locitem.InputPiece);
                    itm.SetinputMeter(locitem.InputMeter);
                    itm.SetinputKg(locitem.InputKg);
                }
                else
                {
                    _ItemsDbSet.Remove(itm);
                }
            }

            foreach (var newitm in model.ItemsMaterialDeliveryNoteWeaving.Where(s => s.Id == 0))
            {
                modelToUpdate.ItemsMaterialDeliveryNoteWeaving.Add(newitm);
            }

            return _dbContext.SaveChangesAsync();
        }
    }
}
