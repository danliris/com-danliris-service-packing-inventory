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
    public class GradeRepository : IGradeRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GradeModel> _dbSet;

        public GradeRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GradeModel>();
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

        public IQueryable<GradeModel> GetDbSet()
        {
            return _dbSet;
        }

        public Task<int> InsertAsync(GradeModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> MultipleInsertAsync(IEnumerable<GradeModel> models)
        {
            foreach (var model in models)
            {
                model.FlagForCreate(_identityProvider.Username, UserAgent);
                _dbSet.Add(model);
            }

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GradeModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GradeModel> ReadByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GradeModel model)
        {
            var modelToUpdate = _dbSet.FirstOrDefault(s => s.Id == id);
            modelToUpdate.SetType(model.Type, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCode(model.Code, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIsAvalGrade(model.IsAvalGrade, _identityProvider.Username, UserAgent);
            return _dbContext.SaveChangesAsync();
        }
    }
}
