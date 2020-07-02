using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWovenType
{
    public class IPWovenTypeRepository : IIPWovenTypeRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<IPWovenTypeModel> _iPWarpTypeDbSet;
        private readonly IIdentityProvider _identityProvider;

        public IPWovenTypeRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _iPWarpTypeDbSet = dbContext.Set<IPWovenTypeModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }
        
        public Task<int> DeleteAsync(int id)
        {
            var modelToDelete = _iPWarpTypeDbSet.FirstOrDefault(s => s.Id == id);
            modelToDelete.FlagForDelete(_identityProvider.Username, UserAgent);

            _iPWarpTypeDbSet.Update(modelToDelete);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(IPWovenTypeModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _iPWarpTypeDbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<IPWovenTypeModel> ReadAll()
        {
            return _iPWarpTypeDbSet.AsNoTracking();
        }

        public Task<IPWovenTypeModel> ReadByIdAsync(int id)
        {
            return _iPWarpTypeDbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateAsync(int id, IPWovenTypeModel newModel)
        {
            var modelToUpdate = _iPWarpTypeDbSet.Where(s => s.Id == id);
            foreach (var model in modelToUpdate)
            {
                model.SetCode(newModel.Code, _identityProvider.Username, UserAgent);
                model.SetWarpType(newModel.WovenType, _identityProvider.Username, UserAgent);
                _iPWarpTypeDbSet.Update(model);
            }
            return _dbContext.SaveChangesAsync();
        }
    }
}
