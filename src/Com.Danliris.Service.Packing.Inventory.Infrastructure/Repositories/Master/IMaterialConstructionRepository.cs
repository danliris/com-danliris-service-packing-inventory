using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Master
{
    public interface IMaterialConstructionRepository : IRepository<MaterialConstructionModel>
    {
        IQueryable<MaterialConstructionModel> GetDbSet();
        Task<int> MultipleInsertAsync(IEnumerable<MaterialConstructionModel> models);
    }
}
