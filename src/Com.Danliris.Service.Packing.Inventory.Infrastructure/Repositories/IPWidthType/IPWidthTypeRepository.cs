using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Com.Moonlay.Models;


namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWidthType
{
    public class IPWidthTypeRepository : IIPWidthTypeRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<IPWidthTypeModel> _iPWidthTypeDbSet;
        private readonly IIdentityProvider _identityProvider;

        public IPWidthTypeRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _iPWidthTypeDbSet = dbContext.Set<IPWidthTypeModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(IPWidthTypeModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            _iPWidthTypeDbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<IPWidthTypeModel> ReadAll()
        {
            return _iPWidthTypeDbSet.AsNoTracking();
        }

        public Task<IPWidthTypeModel> ReadByIdAsync(int id)
        {
            return _iPWidthTypeDbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateAsync(int id, IPWidthTypeModel newModel)
        {
            var modelToUpdate = _iPWidthTypeDbSet.Where(s => s.Id == id);
            foreach(var model in modelToUpdate)
            {
                model.SetCode(newModel.Code,_identityProvider.Username,UserAgent);
                model.SetWidthType(newModel.WidthType, _identityProvider.Username, UserAgent);
                _iPWidthTypeDbSet.Update(model);
            }
            return _dbContext.SaveChangesAsync();
        }
    }
}
