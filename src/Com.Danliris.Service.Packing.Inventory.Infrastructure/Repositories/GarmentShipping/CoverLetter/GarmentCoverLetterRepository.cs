using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter
{
    public class GarmentCoverLetterRepository : IGarmentCoverLetterRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingCoverLetterModel> _dbSet;

        public GarmentCoverLetterRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingCoverLetterModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingCoverLetterModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingCoverLetterModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingCoverLetterModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<int> UpdateAsync(int id, GarmentShippingCoverLetterModel model)
        {
            var modelToUpdate = _dbSet.First(s => s.Id == id);

            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetName(model.Name, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAddress(model.Address, _identityProvider.Username, UserAgent);
            modelToUpdate.SetATTN(model.ATTN, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPhone(model.Phone, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBookingDate(model.BookingDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetOrderId(model.OrderId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetOrderCode(model.OrderCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetOrderName(model.OrderName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPCSQuantity(model.PCSQuantity, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSETSQuantity(model.SETSQuantity, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPACKQuantity(model.PACKQuantity, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCartoonQuantity(model.CartoonQuantity, _identityProvider.Username, UserAgent);
            modelToUpdate.SetForwarderId(model.ForwarderId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetForwarderCode(model.ForwarderCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetForwarderName(model.ForwarderName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTruck(model.Truck, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPlateNumber(model.PlateNumber, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDriver(model.Driver, _identityProvider.Username, UserAgent);
            modelToUpdate.SetContainerNo(model.ContainerNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetFreight(model.Freight, _identityProvider.Username, UserAgent);
            modelToUpdate.SetShippingSeal(model.ShippingSeal, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDLSeal(model.DLSeal, _identityProvider.Username, UserAgent);
            modelToUpdate.SetEMKLSeal(model.EMKLSeal, _identityProvider.Username, UserAgent);
            modelToUpdate.SetExportEstimationDate(model.ExportEstimationDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUnit(model.Unit, _identityProvider.Username, UserAgent);
            modelToUpdate.SetShippingStaff(model.ShippingStaff, _identityProvider.Username, UserAgent);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
