using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement
{
    public class StockOpnameReportItemRepository : IRepository<StockOpnameReportItemModel>
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<StockOpnameReportItemModel> _dbSet;

        public StockOpnameReportItemRepository(IServiceProvider serviceProvider)
        {
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _dbContext = serviceProvider.GetService<PackingInventoryDbContext>();
            _dbSet = _dbContext.Set<StockOpnameReportItemModel>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);
            model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(StockOpnameReportItemModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<StockOpnameReportItemModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<StockOpnameReportItemModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, StockOpnameReportItemModel model)
        {
            throw new NotImplementedException();
        }
    }
}
