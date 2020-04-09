using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.FabricQualityControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class FabricGradeTestDataUtil : BaseDataUtil<FabricGradeTestRepository, FabricGradeTestModel>
    {
        public FabricGradeTestDataUtil(FabricGradeTestRepository repository) : base(repository)
        {
        }

        public override FabricGradeTestModel GetModel()
        {
            return new FabricGradeTestModel(1, 1, 1, 1, 1, 1, "grade", 1, "pcs", 1, 1, 1, 1, "t", 1, 1, 
                new List<CriteriaModel>()
                    {
                        new CriteriaModel("code", "grp",1,"name",1,1,1,1)
                    });
        }
    }
}
