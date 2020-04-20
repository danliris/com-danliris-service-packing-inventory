using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.FabricQualityControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class FabricQualityControlDataUtil : BaseDataUtil<FabricQualityControlRepository, FabricQualityControlModel>
    {
        public FabricQualityControlDataUtil(FabricQualityControlRepository repository)
            : base(repository)
        {
        }

        public override FabricQualityControlModel GetModel()
        {
            return new FabricQualityControlModel("code", DateTimeOffset.UtcNow, "group", true, 1, "no",1,"no", "mach",
                "oper", 1, 1, new List<FabricGradeTestModel>()
                {
                    new FabricGradeTestModel(1,1,1,1,1,1,"grade",1,"pcs",1,1,1,1,"t",1,1, new List<CriteriaModel>()
                    {
                        new CriteriaModel("code", "grp",1,"name",1,1,1,1)
                    })
                });
        }

        public override FabricQualityControlModel GetEmptyModel()
        {
            return new FabricQualityControlModel("a", DateTimeOffset.UtcNow.AddSeconds(3), null, false, 0, null,1, null, null, null, 0, 0, new List<FabricGradeTestModel>()
            {
                new FabricGradeTestModel(0,0,0,0,0,0,null,0,null,0,0,0,0,null,0,0,new List<CriteriaModel>()
                {
                    new CriteriaModel(null,null,0,null,0,0,0,0)
                })
            });
        }
    }
}
