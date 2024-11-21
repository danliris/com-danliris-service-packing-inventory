using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalMDSalesNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentMD.LocalSalesNote
{
    public class GarmentMDLocalSalesNoteRepository : IGarmentMDLocalSalesNoteRepository
    {
       
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentMDLocalSalesNoteModel> _dbSet;
        private readonly DbSet<GarmentMDLocalSalesContractModel> _salesContractDbSet;
        
        public GarmentMDLocalSalesNoteRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentMDLocalSalesNoteModel>();
            _salesContractDbSet = dbContext.Set<GarmentMDLocalSalesContractModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
                .Include(i => i.Items)
                .FirstOrDefault(s => s.Id == id);

            //var sc= _salesContractDbSet.FirstOrDefault(a => a.Id == model.LocalSalesContractId);

            //sc.SetIsUsed(false, _identityProvider.Username, UserAgent);
            model.FlagForDelete(_identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                var sc = _salesContractDbSet.FirstOrDefault(a => a.Id == model.LocalSalesContractId);
                sc.SetRemainingQuantity(sc.RemainingQuantity + item.Quantity, _identityProvider.Username, UserAgent);

                item.FlagForDelete(_identityProvider.Username, UserAgent);

                foreach (var detail in item.Details)
                {
                    detail.FlagForDelete(_identityProvider.Username, UserAgent);
                }
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentMDLocalSalesNoteModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            //var sc = _salesContractDbSet.FirstOrDefault(a => a.Id == model.LocalSalesContractId);
            //sc.SetIsUsed(true, _identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                var sc = _salesContractDbSet.FirstOrDefault(a => a.Id == model.LocalSalesContractId);
                sc.SetRemainingQuantity(sc.RemainingQuantity - item.Quantity, _identityProvider.Username, UserAgent);

                item.FlagForCreate(_identityProvider.Username, UserAgent);

                foreach (var detail in item.Details)
                {
                    detail.FlagForCreate(_identityProvider.Username, UserAgent);
                }
            }

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentMDLocalSalesNoteModel> ReadAll()
        {
            return _dbSet.Include(i => i.Items).AsNoTracking();
        }

        public Task<GarmentMDLocalSalesNoteModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .Include(i => i.Items)
                .ThenInclude(s => s.Details)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentMDLocalSalesNoteModel model)
        {
            var modelToUpdate = _dbSet
                .Include(i => i.Items)
                .ThenInclude(s => s.Details)
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTempo(model.Tempo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetExpenditureNo(model.ExpenditureNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDispositionNo(model.DispositionNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUseVat(model.UseVat, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPaymentType(model.PaymentType, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPaymentType(model.PaymentType, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRejectedReason(null, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIsRejectedShipping(false, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIsRejectedFinance(false, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBCNo(model.BCNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBCDate(model.BCDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBCType(model.BCType, _identityProvider.Username, UserAgent);

            foreach (var itemToUpdate in modelToUpdate.Items)
            {
                var item = model.Items.FirstOrDefault(m => m.Id == itemToUpdate.Id);
                var sc = _salesContractDbSet.FirstOrDefault(a => a.Id == modelToUpdate.LocalSalesContractId);

                if (item != null)
                {
                    var qty = sc.RemainingQuantity + itemToUpdate.Quantity - item.Quantity;
                    sc.SetRemainingQuantity(qty, _identityProvider.Username, UserAgent);

                    itemToUpdate.SetComodityName(item.ComodityName, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetQuantity(item.Quantity, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUomId(item.UomId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUomUnit(item.UomUnit, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetPrice(item.Price, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetRemark(item.Remark, _identityProvider.Username, UserAgent);

                    foreach (var detail in itemToUpdate.Details)
                    {
                        var itemDetailToUpdate = item.Details.FirstOrDefault(f => f.Id == detail.Id);

                        if (itemDetailToUpdate == null)
                        {
                            detail.FlagForDelete(_identityProvider.Username, UserAgent);
                        }
                    }
                }
                else
                {
                    itemToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);

                    sc.SetRemainingQuantity(sc.RemainingQuantity + itemToUpdate.Quantity, _identityProvider.Username, UserAgent);
                }
            }

            foreach (var item in model.Items)
            {
                var macthItem = modelToUpdate.Items.FirstOrDefault(m => m.Id == item.Id);

                foreach (var detail in item.Details)
                {
                    if (detail.Id == 0)
                    {
                        detail.FlagForCreate(_identityProvider.Username, UserAgent);
                        macthItem.Details.Add(detail);
                    }
                }
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> ApproveFinanceAsync(int id)
        {
            var modelToUpdate = _dbSet
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetApproveFinanceDate(DateTimeOffset.Now, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIsApproveFinance(true, _identityProvider.Username, UserAgent);
            modelToUpdate.SetApproveFinanceBy(_identityProvider.Username, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> ApproveShippingAsync(int id)
        {
            var modelToUpdate = _dbSet
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetApproveShippingDate(DateTimeOffset.Now, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIsApproveShipping(true, _identityProvider.Username, UserAgent);
            modelToUpdate.SetApproveShippingBy(_identityProvider.Username, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> RejectShippingAsync(int id, GarmentMDLocalSalesNoteModel model)
        {
            var modelToUpdate = _dbSet
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetIsRejectedShipping(true, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRejectedReason(model.RejectedReason, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> RejectFinanceAsync(int id, GarmentMDLocalSalesNoteModel model)
        {
            var modelToUpdate = _dbSet
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetIsRejectedFinance(true, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRejectedReason(model.RejectedReason, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIsApproveShipping(false, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }
    }
}
