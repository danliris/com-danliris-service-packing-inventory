using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Master
{
    public class GradeDataUtil : BaseDataUtil<GradeRepository, GradeModel>
    {
        public GradeDataUtil(GradeRepository repository) : base(repository)
        {
        }

        public override GradeModel GetModel()
        {
            return new GradeModel("Type", "1", true);
        }
    }
}
