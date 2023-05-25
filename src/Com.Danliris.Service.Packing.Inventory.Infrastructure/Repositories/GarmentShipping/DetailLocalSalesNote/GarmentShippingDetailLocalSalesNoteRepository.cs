using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.DetailShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.DetailLocalSalesNote
{
    public class GarmentShippingDetailLocalSalesNoteRepository : IGarmentShippingDetailLocalSalesNoteRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingDetailLocalSalesNoteModel> _dbSet;
        private readonly DbSet<GarmentShippingLocalSalesNoteModel> _salesNoteDbSet;

        public GarmentShippingDetailLocalSalesNoteRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingDetailLocalSalesNoteModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _salesNoteDbSet = dbContext.Set<GarmentShippingLocalSalesNoteModel>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
                .Include(i => i.Items)
                .FirstOrDefault(s => s.Id == id);

            var salesNote = _salesNoteDbSet.FirstOrDefault(a => a.Id == model.LocalSalesNoteId);
            salesNote.SetIsDetail(false, _identityProvider.Username, UserAgent);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingDetailLocalSalesNoteModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            var salesNote = _salesNoteDbSet.FirstOrDefault(a => a.Id == model.LocalSalesNoteId);
            salesNote.SetIsDetail(true, _identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {   
                item.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingDetailLocalSalesNoteModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingDetailLocalSalesNoteModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .Include(i => i.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingDetailLocalSalesNoteModel model)
        {
            var modelToUpdate = _dbSet
                .Include(i => i.Items)
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetLocalSalesContractId(model.LocalSalesContractId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSalesContractNo(model.SalesContractNo, _identityProvider.Username, UserAgent);

            modelToUpdate.SetLocalSalesNoteId(model.LocalSalesNoteId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetNoteNo(model.NoteNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);

            modelToUpdate.SetTransactionTypeId(model.TransactionTypeId, _identityProvider.Username, UserAgent);           
            modelToUpdate.SetTransactionTypeCode(model.TransactionTypeCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTransactionTypeName(model.TransactionTypeName, _identityProvider.Username, UserAgent);

            modelToUpdate.SetBuyerId(model.BuyerId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerCode(model.BuyerCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerName(model.BuyerName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAmount(model.Amount, _identityProvider.Username, UserAgent);

            foreach (var itemToUpdate in modelToUpdate.Items)
            {
                var item = model.Items.FirstOrDefault(m => m.Id == itemToUpdate.Id);

                if (item != null)
                {             
                    itemToUpdate.SetUnitId(item.UnitId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUnitCode(item.UnitCode, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUnitName(item.UnitName, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetQuantity(item.Quantity, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUomId(item.UomId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUomUnit(item.UomUnit, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetAmount(item.Amount, _identityProvider.Username, UserAgent);
                }
                else
                {
                    itemToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                }
            }

            foreach (var item in model.Items.Where(w => w.Id == 0))
            {         
                modelToUpdate.Items.Add(item);
            }

            return _dbContext.SaveChangesAsync();
        }       
    }
}
