using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaInputProductionOrderWithBonId : BaseDataUtil<DyeingPrintingAreaInputProductionOrderRepository, DyeingPrintingAreaInputProductionOrderModel>
    {
        public DyeingPrintingAreaInputProductionOrderWithBonId (DyeingPrintingAreaInputProductionOrderRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaInputProductionOrderModel GetModel()
        {
            var model = new DyeingPrintingAreaInputProductionOrderModel("GUDANGJADI", 1, "asdf", "sadf","packingInstruction","cartNo","buyyer","Construction","Unit","color","motif","uomUnit", 1,true, 12, "asdf", 1);
            model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel();

            return model;
        }

        public override DyeingPrintingAreaInputProductionOrderModel GetEmptyModel()
        {
            var model = new DyeingPrintingAreaInputProductionOrderModel(null, 0, null, null,null,null,null,null,null,null,null,null, 0,true, 0, null, 0);
            model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel();

            return model;
        }
    }
}
