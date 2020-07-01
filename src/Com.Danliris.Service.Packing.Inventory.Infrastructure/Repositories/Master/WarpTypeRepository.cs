using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Master
{
    public class WarpTypeRepository : IWarpTypeRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<WarpTypeModel> _dbSet;

        public WarpTypeRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<WarpTypeModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.FirstOrDefault(s => s.Id == id);
            model.FlagForDelete(_identityProvider.Username, UserAgent);
            _dbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public int GetCodeByType(string type)
        {
            var data = _dbSet.FirstOrDefault(s => s.Type == type);

            if (data == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(data.Code);
            }
        }

        public IQueryable<WarpTypeModel> GetDbSet()
        {
            return _dbSet;
        }

        public Task<int> InsertAsync(WarpTypeModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> MultipleInsertAsync(IEnumerable<WarpTypeModel> models)
        {
            foreach (var model in models)
            {
                model.FlagForCreate(_identityProvider.Username, UserAgent);
                _dbSet.Add(model);
            }

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<WarpTypeModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<WarpTypeModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, WarpTypeModel model)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);
            modelToUpdate.SetType(model.Type, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCode(model.Code, _identityProvider.Username, UserAgent);

            return _dbContext.SaveChangesAsync();
        }
    }
}
