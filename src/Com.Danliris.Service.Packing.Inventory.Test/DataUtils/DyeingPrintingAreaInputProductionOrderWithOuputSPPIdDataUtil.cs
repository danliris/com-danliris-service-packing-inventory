using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaInputProductionOrderWithOuputSPPIdDataUtil : BaseDataUtil<DyeingPrintingAreaInputProductionOrderRepository, DyeingPrintingAreaInputProductionOrderModel>
    {
        public DyeingPrintingAreaInputProductionOrderWithOuputSPPIdDataUtil(DyeingPrintingAreaInputProductionOrderRepository repository) : base(repository)
        {
        }
        public override DyeingPrintingAreaInputProductionOrderModel GetModel()
        {
            var model = new DyeingPrintingAreaInputProductionOrderModel("area", 1, "01", "SppOrderType", "packingInstruction", "cartNo", "buyer", "construction", "unit", "color", "motif", "uomUnit", 1, false, 1, "grade", 1, 1, 1, 1, "remark","zimmer", 1, "a", 1, "a", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, DateTimeOffset.Now, "a");
            
            model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel();

            return model;
        }

        public override DyeingPrintingAreaInputProductionOrderModel GetEmptyModel()
        {
            var model = new DyeingPrintingAreaInputProductionOrderModel(null, 1, null, null, null, null, null, null, null, null, null, null, 1, false, 1, null, 1, 1, 1, 1, null,null, 0, null, 0, null, "0", 0, null, 0, null, 0, 0, null, true, 0, 0, null, true, 0, DateTimeOffset.Now, null);
            
            model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel();

            return model;
        }
    }
}
