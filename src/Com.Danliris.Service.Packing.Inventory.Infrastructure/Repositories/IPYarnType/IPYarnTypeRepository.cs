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

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPYarnType
{
    public class IPYarnTypeRepository : IIPYarnTypeRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<IPYarnTypeModel> _iPYarnTypeDbSet;
        private readonly IIdentityProvider _identityProvider;

        public IPYarnTypeRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _iPYarnTypeDbSet = dbContext.Set<IPYarnTypeModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var modelToDelete = _iPYarnTypeDbSet.FirstOrDefault(s => s.Id == id);
            modelToDelete.FlagForDelete(_identityProvider.Username, UserAgent);

            _iPYarnTypeDbSet.Update(modelToDelete);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(IPYarnTypeModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _iPYarnTypeDbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<IPYarnTypeModel> ReadAll()
        {
            return _iPYarnTypeDbSet.AsNoTracking();
        }

        public Task<IPYarnTypeModel> ReadByIdAsync(int id)
        {
            return _iPYarnTypeDbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateAsync(int id, IPYarnTypeModel newModel)
        {
            var modelToUpdate = _iPYarnTypeDbSet.Where(s => s.Id == id);
            foreach (var model in modelToUpdate)
            {
                model.SetCode(newModel.Code, _identityProvider.Username, UserAgent);
                model.SetYarnType(newModel.YarnType, _identityProvider.Username, UserAgent);
                _iPYarnTypeDbSet.Update(model);
            }
            return _dbContext.SaveChangesAsync();
        }
    }
}
