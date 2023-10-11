using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShipingNoteCreditAdvice;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNoteCreditAdvice
{
    public class GarmentShippingNoteCreditAdviceRepository : IGarmentShippingNoteCreditAdviceRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingNoteModel> _garmentshippingnoteDbSet;
        private readonly DbSet<GarmentShippingNoteCreditAdviceModel> _dbSet;

        public GarmentShippingNoteCreditAdviceRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingNoteCreditAdviceModel>();
            _garmentshippingnoteDbSet = dbContext.Set<GarmentShippingNoteModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);

            var shippingNote = _garmentshippingnoteDbSet.FirstOrDefault(a => a.Id == model.ShippingNoteId);
            shippingNote.SetAmountCA(shippingNote.AmountCA - model.PaidAmount, _identityProvider.Username, UserAgent);
          
            model.FlagForDelete(_identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingNoteCreditAdviceModel model)
        {
            var shippingNote = _garmentshippingnoteDbSet.FirstOrDefault(a => a.Id == model.ShippingNoteId);
            shippingNote.SetAmountCA(shippingNote.AmountCA + model.PaidAmount, _identityProvider.Username, UserAgent);
           
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingNoteCreditAdviceModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingNoteCreditAdviceModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<int> UpdateAsync(int id, GarmentShippingNoteCreditAdviceModel model)
        {
            var modelToUpdate = _dbSet.First(s => s.Id == id);

            modelToUpdate.SetPaymentDate(model.PaymentDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetReceiptNo(model.ReceiptNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPaymentTerm(model.PaymentTerm, _identityProvider.Username, UserAgent);

            //modelToUpdate.SetAmount(model.Amount, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetPaidAmount(model.PaidAmount, _identityProvider.Username, UserAgent);
            //modelToUpdate.SetBalanceAmount(model.BalanceAmount, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBankComission(model.BankComission, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCreditInterest(model.CreditInterest, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBankCharges(model.BankCharges, _identityProvider.Username, UserAgent);
            modelToUpdate.SetInsuranceCharge(model.InsuranceCharge, _identityProvider.Username, UserAgent);
            modelToUpdate.SetNettNego(model.NettNego, _identityProvider.Username, UserAgent);
   
            modelToUpdate.SetDocumentSendDate(model.DocumentSendDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);
            modelToUpdate.SetShippingNoteId(model.ShippingNoteId, _identityProvider.Username, UserAgent);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
