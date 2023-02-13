using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.SalesExport.ExportCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.SalesExport
{
    public class GarmentExportCoverLetterRepository : IGarmentExportCoverLetterRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingExportCoverLetterModel> _dbSet;

        public GarmentExportCoverLetterRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingExportCoverLetterModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingExportCoverLetterModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingExportCoverLetterModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingExportCoverLetterModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<GarmentShippingExportCoverLetterModel> ReadByExportSalesNoteIdAsync(int exportsalesnoteid)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.ExportSalesNoteId == exportsalesnoteid);
        }

        public async Task<int> UpdateAsync(int id, GarmentShippingExportCoverLetterModel model)
        {
            var modelToUpdate = _dbSet.First(s => s.Id == id);

            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBCNo(model.BCNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBCDate(model.BCDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTruck(model.Truck, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPlateNumber(model.PlateNumber, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDriver(model.Driver, _identityProvider.Username, UserAgent);
            modelToUpdate.SetShippingStaffId(model.ShippingStaffId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetShippingStaffName(model.ShippingStaffName, _identityProvider.Username, UserAgent);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
