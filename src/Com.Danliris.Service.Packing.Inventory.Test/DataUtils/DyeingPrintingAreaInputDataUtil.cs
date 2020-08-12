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
                new DyeingPrintingAreaInputProductionOrderModel("IM",1,"a","e","rr","1","as","test","unit","color","motif","mtr",1,false,1),
                 new DyeingPrintingAreaInputProductionOrderModel("TRANSIT",1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr",1,false,"s","s","s",1,1,1,1,"name",1,"a","1",1,"a","a",1,"a","a",1,"a",1,"a",1,1,"a",false,1,1,"a",false,1,1),
                 new DyeingPrintingAreaInputProductionOrderModel("SHIPPING",1,"A",1,"A","A",1,"a","A","A","A","A","A",1,"A",1,"A",false,1,"unit",1,1,1,"name",1,"a","1","1","1",1,"a",1,"a",1,1,"a",false,1,1,"a",false,1),
                 new DyeingPrintingAreaInputProductionOrderModel("IM",1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr",1,1,false,1,1,1,"name",1,"a","1",1,"a",1,"a",1)
            });
        }

        public override DyeingPrintingAreaInputModel GetEmptyModel()
        {
            return new DyeingPrintingAreaInputModel(DateTimeOffset.UtcNow, null, null, null, null, new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel(null,0,null,null,null,null,null,null,null,null,null,null,0,true,0),
                 new DyeingPrintingAreaInputProductionOrderModel(null,0,null,null,1,null,null,null,null,null,null,null,null,1,true,null,null,null,0,0,0,0,null,0,null,"0",0,null,null,0,null,null,0,null,0,null,0,0,null,true,0,0,null,true,0,0),
                 new DyeingPrintingAreaInputProductionOrderModel(null,0,null,0,null,null,0,null,null,null,null,null,null,0,null,0,null,true,0, null,0,0,0,null,0,null,"0",null,null,0,null,0,null,0,0,null,true,0,0,null,true,0),
                 new DyeingPrintingAreaInputProductionOrderModel(null,0,null,null,0,null,null,null,null,null,null,null,null,1,1,true,0,0,0,null,0,null,"0",0,null,0,null,0),
            });
        }

        public DyeingPrintingAreaInputModel GetAvalTransformModel()
        {
            return new DyeingPrintingAreaInputModel(DateTimeOffset.UtcNow, "GUDANG AVAL", "shif", "np", "gr", "typ", true, 5, 5, new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel("GUDANG AVAL","no",1,"no","no",1,"car","cs","as","asf",11,"col","mo","sd","sf",1,false,1,1,"name",1,"a","1","a",1,"a",1,"a",1,1,"a",false,1,1,"a",false),
                new DyeingPrintingAreaInputProductionOrderModel("GUDANG AVAL","no",1,"no","no",1,"car","cs","as","asf",11,"col","mo","sd","sf",1,false,1,1,"name",1,"a","1","a",1,"a",1,"a",1,1,"a",false,1,1,"a",false)
            });
        }

        public DyeingPrintingAreaInputModel GetEmptyuAvalTransformModel()
        {
            return new DyeingPrintingAreaInputModel(DateTimeOffset.UtcNow.AddDays(-1), null, null, null, null, null, false, 0, 0, new List<DyeingPrintingAreaInputProductionOrderModel>()
            {
                new DyeingPrintingAreaInputProductionOrderModel(null,null,0,null,null,0,null,null,null,null,0,null,null,null,null,0,true,0,0,null,0,null,"0",null,0,null,0,null,0,0,null,true,0,0,null,true)
            });
        }
    }
}
