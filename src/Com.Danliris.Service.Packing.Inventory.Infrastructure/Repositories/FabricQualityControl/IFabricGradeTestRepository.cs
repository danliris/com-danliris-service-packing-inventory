using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.FabricQualityControl
{
    public interface IFabricGradeTestRepository : IRepository<FabricGradeTestModel>
    {
        IQueryable<FabricGradeTestModel> GetDbSet();
        IQueryable<FabricGradeTestModel> ReadAllIgnoreQueryFilter();
    }
}
