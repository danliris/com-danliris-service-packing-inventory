using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LetterOfCredit
{
    public class GarmentLetterOfCreditRepository : IGarmentLetterOfCreditRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingLetterOfCreditModel> _dbSet;

        public GarmentLetterOfCreditRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingLetterOfCreditModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingLetterOfCreditModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingLetterOfCreditModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentShippingLetterOfCreditModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<int> UpdateAsync(int id, GarmentShippingLetterOfCreditModel model)
        {
            var modelToUpdate = _dbSet.First(s => s.Id == id);

            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetApplicantCode(model.ApplicantCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetApplicantId(model.ApplicantId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetApplicantName(model.ApplicantName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDocumentCreditNo(model.DocumentCreditNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetExpireDate(model.ExpireDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetExpirePlace(model.ExpirePlace, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIssuedBank(model.IssuedBank, _identityProvider.Username, UserAgent);
            modelToUpdate.SetLatestShipment(model.LatestShipment, _identityProvider.Username, UserAgent);
            modelToUpdate.SetLCCondition(model.LCCondition, _identityProvider.Username, UserAgent);
            modelToUpdate.SetQuantity(model.Quantity, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTotalAmount(model.TotalAmount, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUomId(model.UomId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetUomUnit(model.UomUnit, _identityProvider.Username, UserAgent);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
