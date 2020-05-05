using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaInputDataUtil : BaseDataUtil<DyeingPrintingAreaInputRepository, DyeingPrintingAreaInputModel>
    {
        public DyeingPrintingAreaInputDataUtil(DyeingPrintingAreaInputRepository repository) : base(repository)
        {

        }

        public override DyeingPrintingAreaInputModel GetModel()
        {
            return new DyeingPrintingAreaInputModel(DateTimeOffset.UtcNow, "IM", "pa", "1", "A", new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel("IM",1,"a","e","rr","1","as","test","unit","color","motif","mtr",1,false),
                 new DyeingPrintingAreaInputProductionOrderModel("IM",1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr",1,false,"s","s","s"),
                 new DyeingPrintingAreaInputProductionOrderModel("IM",1,"a",1,"a","a","a","a","a","a","a","a",false),
                 new DyeingPrintingAreaInputProductionOrderModel("IM", "AVAL A", "5-11", "KRG", 152, 10, false),
                 new DyeingPrintingAreaInputProductionOrderModel("IM",1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr",1,false)
            });
        }

        public override DyeingPrintingAreaInputModel GetEmptyModel()
        {
            return new DyeingPrintingAreaInputModel(DateTimeOffset.UtcNow, null, null, null, null, new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel(null,0,null,null,null,null,null,null,null,null,null,null,1,true),
                 new DyeingPrintingAreaInputProductionOrderModel(null,0,null,null,1,null,null,null,null,null,null,null,null,1,true,null,null,null),
                 new DyeingPrintingAreaInputProductionOrderModel(null,0,null,0,null,null,null,null,null,null,null,null,true),
                 new DyeingPrintingAreaInputProductionOrderModel(null, null, null, null, 0, 0, true),
                 new DyeingPrintingAreaInputProductionOrderModel(null,0,null,null,0,null,null,null,null,null,null,null,null,1,true),
            });
        }
    }
}
