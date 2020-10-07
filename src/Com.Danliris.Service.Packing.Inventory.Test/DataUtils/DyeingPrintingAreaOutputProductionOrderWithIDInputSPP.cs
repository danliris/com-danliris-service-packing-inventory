using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaOutputProductionOrderWithIDInputSPP : BaseDataUtil<DyeingPrintingAreaOutputProductionOrderRepository, DyeingPrintingAreaOutputProductionOrderModel>
    {
        public DyeingPrintingAreaOutputProductionOrderWithIDInputSPP(DyeingPrintingAreaOutputProductionOrderRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaOutputProductionOrderModel GetModel()
        {
            var model = new DyeingPrintingAreaOutputProductionOrderModel("IM", "PACKING", false, 1, "a", "a", 1, "a", "a", "a", "c", "u", "c", "m", "a", "r", "g,", "s", 1, 1, 1, 1, "a", 1, "a", "1", "a", "a", 1, "a", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, "a");
            model.DyeingPrintingAreaOutput = new DyeingPrintingAreaOutputModel();

            return model;
        }

        public override DyeingPrintingAreaOutputProductionOrderModel GetEmptyModel()
        {
            var model = new DyeingPrintingAreaOutputProductionOrderModel(null, null, true, 0, null, null, 0, null, null, null, null, null, null, null, null, null, null, null, 0, 0, 0, 0, null, 0, null, "0", null, null, 0, null, 0, null, 0, null, 0, 0, null, true, 0, 0, null, true, 0, null);
            model.DyeingPrintingAreaOutput = new DyeingPrintingAreaOutputModel();

            return model;
        }
    }
}
