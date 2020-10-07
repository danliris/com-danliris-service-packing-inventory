using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaOutputProductionOrderWithWarehouseCtorDataUtil : BaseDataUtil<DyeingPrintingAreaOutputProductionOrderRepository, DyeingPrintingAreaOutputProductionOrderModel>
    {
        public DyeingPrintingAreaOutputProductionOrderWithWarehouseCtorDataUtil(DyeingPrintingAreaOutputProductionOrderRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaOutputProductionOrderModel GetModel()
        {
            var model = new DyeingPrintingAreaOutputProductionOrderModel(11, "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", 2, "a", "a", 3, "a", 2, "s", 2, "a", true, "s", "d", 10, 1, 1, "a", 1, "a", "1", "a", 1, "a", 1, "aa", 1, 1, "a", false, 1, 1, "a", false, 1, "a");
            model.DyeingPrintingAreaOutput = new DyeingPrintingAreaOutputModel();

            return model;
        }

        public override DyeingPrintingAreaOutputProductionOrderModel GetEmptyModel()
        {
            var model = new DyeingPrintingAreaOutputProductionOrderModel(0, null, null, null, null, null, null, null, null, null, null, null, 0, null, null, 0, null, 0, null, 0, null, true, null, null, 0, 0, 0, null, 0, null, "0", null, 0, null, 0, null, 0, 0, null, true, 0, 0, null, true, 0, null);
            model.DyeingPrintingAreaOutput = new DyeingPrintingAreaOutputModel();

            return model;
        }
    }
}
