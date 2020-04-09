using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.FabricQualityControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class FabricQualityControlDataUtil : BaseDataUtil<FabricQualityControlRepository, FabricQualityControlModel>
    {
        DyeingPrintingAreaMovementDataUtil _dpDataUtil;
        public FabricQualityControlDataUtil(FabricQualityControlRepository repository, DyeingPrintingAreaMovementDataUtil dyeingPrintingAreaMovementDataUtil) 
            : base(repository)
        {
            _dpDataUtil = dyeingPrintingAreaMovementDataUtil;
        }

        public override FabricQualityControlModel GetModel()
        {
            var dpData = _dpDataUtil.GetTestData().Result;
            return new FabricQualityControlModel("code", DateTimeOffset.UtcNow, "group", true, dpData.Id, dpData.BonNo, dpData.ProductionOrderNo, "mach",
                "oper", 1, 1, new List<FabricGradeTestModel>()
                {
                    new FabricGradeTestModel(1,1,1,1,1,1,"grade",1,"pcs",1,1,1,1,"t",1,1, new List<CriteriaModel>()
                    {
                        new CriteriaModel("code", "grp",1,"name",1,1,1,1)
                    })
                });
        }
    }
}
