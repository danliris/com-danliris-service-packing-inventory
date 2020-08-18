using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.VBPayment;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.VBPayment
{
    public class GarmentShippingVBPaymentRepository : IGarmentShippingVBPaymentRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingVBPaymentModel> _dbSet;

        public GarmentShippingVBPaymentRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _dbSet = dbContext.Set<GarmentShippingVBPaymentModel>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
                .Include(i => i.Invoices)
                .Include(i => i.Units)
                .FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            foreach (var invoice in model.Invoices)
            {
                invoice.FlagForDelete(_identityProvider.Username, UserAgent);
                
            }

            foreach (var unit in model.Units)
            {
                unit.FlagForDelete(_identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingVBPaymentModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            foreach (var invoice in model.Invoices)
            {
                invoice.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            foreach (var unit in model.Units)
            {
                unit.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingVBPaymentModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingVBPaymentModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .Include(i => i.Invoices)
                .Include(i => i.Units)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingVBPaymentModel model)
        {
            var modelToUpdate = _dbSet
                .Include(i => i.Invoices)
                .Include(i => i.Units)
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetBillValue(model.BillValue, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerCode(model.BuyerCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerId(model.BuyerId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerName(model.BuyerName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetEMKLCode(model.EMKLCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetEMKLId(model.EMKLId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetEMKLInvoiceNo(model.EMKLInvoiceNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetEMKLName(model.EMKLName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetForwarderCode(model.ForwarderCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetForwarderId(model.ForwarderId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetForwarderInvoiceNo(model.ForwarderInvoiceNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetForwarderName(model.ForwarderName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIncomeTaxId(model.IncomeTaxId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIncomeTaxName(model.IncomeTaxName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIncomeTaxRate(model.IncomeTaxRate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetVatValue(model.VatValue, _identityProvider.Username, UserAgent);
            modelToUpdate.SetVBDate(model.VBDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPaymentDate(model.PaymentDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPaymentType(model.PaymentType, _identityProvider.Username, UserAgent);

            foreach (var invoiceToUpdate in modelToUpdate.Invoices)
            {
                var invoice = model.Invoices.FirstOrDefault(m => m.Id == invoiceToUpdate.Id);
                if (invoice == null)
                {
                    invoiceToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                }
            }

            foreach (var invoice in model.Invoices.Where(w => w.Id == 0))
            {
                invoice.FlagForCreate(_identityProvider.Username, UserAgent);
                modelToUpdate.Invoices.Add(invoice);
            }

            foreach (var unitToUpdate in modelToUpdate.Units)
            {
                var unit = model.Units.FirstOrDefault(m => m.Id == unitToUpdate.Id);
                unitToUpdate.SetBillValue(unitToUpdate.BillValue, _identityProvider.Username, UserAgent);
                if (unit == null)
                {
                    unitToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                }
            }

            foreach (var unit in model.Units.Where(w => w.Id == 0))
            {
                unit.FlagForCreate(_identityProvider.Username, UserAgent);
                modelToUpdate.Units.Add(unit);
            }

            return _dbContext.SaveChangesAsync();
        }
    }
}
