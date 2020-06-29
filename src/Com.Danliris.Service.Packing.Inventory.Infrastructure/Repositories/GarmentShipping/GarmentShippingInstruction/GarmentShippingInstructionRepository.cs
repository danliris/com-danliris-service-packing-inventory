using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Com.Moonlay.Models;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionRepository : IGarmentShippingInstructionRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingInvoiceModel> _garmentshippingInvoiceDbSet;
        private readonly DbSet<GarmentShippingInstructionModel> _dbSet;

        public GarmentShippingInstructionRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingInstructionModel>();
            _garmentshippingInvoiceDbSet = dbContext.Set<GarmentShippingInvoiceModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
                .FirstOrDefault(s => s.Id == id);

            var invoice = _garmentshippingInvoiceDbSet.FirstOrDefault(a => a.Id == model.InvoiceId);
            invoice.SetIsUsed(false, _identityProvider.Username, UserAgent);
            model.FlagForDelete(_identityProvider.Username, UserAgent);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingInstructionModel model)
        {
            var invoice = _garmentshippingInvoiceDbSet.FirstOrDefault(a => a.Id == model.InvoiceId);
            invoice.SetIsUsed(true, _identityProvider.Username, UserAgent);

            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingInstructionModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingInstructionModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingInstructionModel model)
        {
            var modelToUpdate = _dbSet
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetCarrier(model.Carrier, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCartonNo(model.CartonNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetEMKLCode(model.EMKLCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetEMKLId(model.EMKLId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetEMKLName(model.EMKLName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetFeederVessel(model.FeederVessel, _identityProvider.Username, UserAgent);
            modelToUpdate.SetFlight(model.Flight, _identityProvider.Username, UserAgent);
            modelToUpdate.SetNotify(model.Notify, _identityProvider.Username, UserAgent);
            modelToUpdate.SetOceanVessel(model.OceanVessel, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPlaceOfDelivery(model.PlaceOfDelivery, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPortOfDischarge(model.PortOfDischarge, _identityProvider.Username, UserAgent);
            modelToUpdate.SetShippedBy(model.ShippedBy, _identityProvider.Username, UserAgent);
            modelToUpdate.SetATTN(model.ATTN, _identityProvider.Username, UserAgent);
            modelToUpdate.SetFax(model.Fax, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCC(model.CC, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPhone(model.Phone, _identityProvider.Username, UserAgent);
            modelToUpdate.SetLadingBill(model.LadingBill, _identityProvider.Username, UserAgent);
            modelToUpdate.SetLadingDate(model.LadingDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetFreight(model.Freight, _identityProvider.Username, UserAgent);

            modelToUpdate.SetSpecialInstruction(model.SpecialInstruction, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTransit(model.Transit, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }
    }
}
