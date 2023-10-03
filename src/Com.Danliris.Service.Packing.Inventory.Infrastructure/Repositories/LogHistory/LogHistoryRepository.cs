using Com.Danliris.Service.Packing.Inventory.Data.Models.LogHistory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory
{
    public class LogHistoryRepository : ILogHistoryRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<LogHistoryModel> _dbSet;

        public LogHistoryRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<LogHistoryModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> InsertAsync(string division,string activity)
        {
            LogHistoryModel model = new LogHistoryModel
            {
                Division = division,
                Activity = activity,
                CreatedDate = DateTime.Now,
                CreatedBy = _identityProvider.Username,
            };

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }
    }
}
