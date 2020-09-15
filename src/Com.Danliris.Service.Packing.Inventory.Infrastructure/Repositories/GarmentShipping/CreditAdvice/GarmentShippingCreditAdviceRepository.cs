using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice
{
    public class GarmentShippingCreditAdviceRepository : IGarmentShippingCreditAdviceRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingCreditAdviceModel> _dbSet;

        public GarmentShippingCreditAdviceRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingCreditAdviceModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingCreditAdviceModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingCreditAdviceModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingCreditAdviceModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<int> UpdateAsync(int id, GarmentShippingCreditAdviceModel model)
        {
            var modelToUpdate = _dbSet.First(s => s.Id == id);
            modelToUpdate.SetValas(model.Valas, _identityProvider.Username, UserAgent);
            modelToUpdate.SetLCType(model.LCType, _identityProvider.Username, UserAgent);
            modelToUpdate.SetInkaso(model.Inkaso, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDisconto(model.Disconto, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSRNo(model.SRNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetNegoDate(model.NegoDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPaymentDate(model.PaymentDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCondition(model.Condition, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBankComission(model.BankComission, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDiscrepancyFee(model.DiscrepancyFee, _identityProvider.Username, UserAgent);
            modelToUpdate.SetNettNego(model.NettNego, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBTBCADate(model.BTBCADate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBTBAmount(model.BTBAmount, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBTBRatio(model.BTBRatio, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBTBRate(model.BTBRate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBTBTransfer(model.BTBTransfer, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBTBMaterial(model.BTBMaterial, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBillDays(model.BillDays, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBillAmount(model.BillAmount, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBillCA(model.BillCA, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCreditInterest(model.CreditInterest, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBankCharges(model.BankCharges, _identityProvider.Username, UserAgent);
            modelToUpdate.SetOtherCharge(model.OtherCharge, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDocumentPresente(model.DocumentPresente, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCargoPolicyNo(model.CargoPolicyNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCargoPolicyDate(model.CargoPolicyDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCargoPolicyValue(model.CargoPolicyValue, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAccountsReceivablePolicyNo(model.AccountsReceivablePolicyNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAccountsReceivablePolicyDate(model.AccountsReceivablePolicyDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAccountsReceivablePolicyValue(model.AccountsReceivablePolicyValue, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDocumentSendDate(model.DocumentSendDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
