using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaOutputDataUtil : BaseDataUtil<DyeingPrintingAreaOutputRepository, DyeingPrintingAreaOutputModel>
    {
        public DyeingPrintingAreaOutputDataUtil(DyeingPrintingAreaOutputRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaOutputModel GetModel()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "IM", "pa", "1", false, "TRANSIT", new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel(1,"a","e","rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2),
                new DyeingPrintingAreaOutputProductionOrderModel(1,"a",1,"a","a0,","s","s","s","s","d","d","e"),
                new DyeingPrintingAreaOutputProductionOrderModel(1,"a","e","rr","1","as","test","unit","color","motif","mtr", "rem",10,"a","test","PACK",10,"Pack")

            });
        }

        public override DyeingPrintingAreaOutputModel GetEmptyModel()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, null, null, null, true, null, new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel(0,null,null,null,null,null,null,null,null,null,null,null,null,null,1),
                new DyeingPrintingAreaOutputProductionOrderModel(0,null,0,null,null,null,null,null,null,null,null,null),
                new DyeingPrintingAreaOutputProductionOrderModel(0,null,null,null,null,null,null,null,null,null,null,null,1,null,null,null,1,null)

            });
        }
    }
}
