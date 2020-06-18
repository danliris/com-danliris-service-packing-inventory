using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Com.Moonlay.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPProcessType
{
    public class IPProcessTypeRepository : IIPProcessTypeRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<IPProcessTypeModel> _iPProcessTypeDbSet;
        private readonly IIdentityProvider _identityProvider;

        public IPProcessTypeRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _iPProcessTypeDbSet = dbContext.Set<IPProcessTypeModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var modelToDelete = _iPProcessTypeDbSet.FirstOrDefault(s => s.Id == id);
            modelToDelete.FlagForDelete(_identityProvider.Username, UserAgent);

            _iPProcessTypeDbSet.Update(modelToDelete);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(IPProcessTypeModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _iPProcessTypeDbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<IPProcessTypeModel> ReadAll()
        {
            return _iPProcessTypeDbSet.AsNoTracking();
        }

        public Task<IPProcessTypeModel> ReadByIdAsync(int id)
        {
            return _iPProcessTypeDbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateAsync(int id, IPProcessTypeModel newModel)
        {
            var modelToUpdate = _iPProcessTypeDbSet.Where(s => s.Id == id);
            foreach (var model in modelToUpdate)
            {
                model.SetCode(newModel.Code, _identityProvider.Username, UserAgent);
                model.SetProcessType(newModel.ProcessType, _identityProvider.Username, UserAgent);
                _iPProcessTypeDbSet.Update(model);
            }
            return _dbContext.SaveChangesAsync();
        }
    }
}
