using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalCoverLetterTS;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNoteTS;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetterTS
{
    public class GarmentLocalCoverLetterTSRepository : IGarmentLocalCoverLetterTSRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingLocalCoverLetterTSModel> _dbSet;
        private readonly DbSet<GarmentShippingLocalSalesNoteTSModel> _salesNoteDbSet;

        public GarmentLocalCoverLetterTSRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingLocalCoverLetterTSModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _salesNoteDbSet = dbContext.Set<GarmentShippingLocalSalesNoteTSModel>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);
            var salesNote = _salesNoteDbSet.FirstOrDefault(a => a.Id == model.LocalSalesNoteId);
            salesNote.SetIsCL(false, _identityProvider.Username, UserAgent);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingLocalCoverLetterTSModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            var salesNote = _salesNoteDbSet.FirstOrDefault(a => a.Id == model.LocalSalesNoteId);
            salesNote.SetIsCL(true, _identityProvider.Username, UserAgent);

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingLocalCoverLetterTSModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingLocalCoverLetterTSModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<GarmentShippingLocalCoverLetterTSModel> ReadByLocalSalesNoteIdAsync(int localsalesnoteid)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.LocalSalesNoteId == localsalesnoteid);
        }

        public async Task<int> UpdateAsync(int id, GarmentShippingLocalCoverLetterTSModel model)
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
