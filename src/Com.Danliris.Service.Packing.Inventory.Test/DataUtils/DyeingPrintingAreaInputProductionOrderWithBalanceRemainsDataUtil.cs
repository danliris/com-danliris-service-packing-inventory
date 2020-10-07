using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaInputProductionOrderWithBalanceRemainsDataUtil : BaseDataUtil<DyeingPrintingAreaInputProductionOrderRepository, DyeingPrintingAreaInputProductionOrderModel>
    {
        public DyeingPrintingAreaInputProductionOrderWithBalanceRemainsDataUtil(DyeingPrintingAreaInputProductionOrderRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaInputProductionOrderModel GetModel()
        {
            var model = new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "asdf", "sadf", "dfa", "asdf", "asdf", "adsf", "asdf", "asdf", "asdf", "asf", 10, true, 1, "daf", 1, 12, 1, "a", 1, "a", 1, "a", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, "a");
            model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel();

            return model;
        }

        public override DyeingPrintingAreaInputProductionOrderModel GetEmptyModel()
        {
            var model = new DyeingPrintingAreaInputProductionOrderModel(null, 0, null, null, null, null, null, null, null, null, null, null, 0, true, 0, null, 0, 0, 0, null, 0, null, 0, null, "0", 0, null, 0, null, 0, 0, null, true, 0, 0, null, true, 0, null);
            model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel();

            return model;
        }
    }
}
