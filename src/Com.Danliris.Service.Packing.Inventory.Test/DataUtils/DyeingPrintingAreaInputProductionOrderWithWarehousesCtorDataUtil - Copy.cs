using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaInputProductionOrderWithWarehousesCtorDataUtil : BaseDataUtil<DyeingPrintingAreaInputProductionOrderRepository, DyeingPrintingAreaInputProductionOrderModel>
    {
        public DyeingPrintingAreaInputProductionOrderWithWarehousesCtorDataUtil(DyeingPrintingAreaInputProductionOrderRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaInputProductionOrderModel GetModel()
        {
            var model = new DyeingPrintingAreaInputProductionOrderModel("GUDANGJADI", 1, "asdf", "sadf", 1, "asdf", "asdf", "adsf", "asdf", "asdf", "asdf", "asf", "asdf", 123, true,"remark", "zimmer", "asdf", "asdf", 123, 1, 1, 1, "name", 1, "a", "1", 1, "a", "a", 1, "a", "a", 1, "a", 1, "s", 1, 1, "a", false, 1, 1, "a", false, 1, 1, 1,DateTimeOffset.Now);
            model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel();

            return model;
        }

        public override DyeingPrintingAreaInputProductionOrderModel GetEmptyModel()
        {
            var model = new DyeingPrintingAreaInputProductionOrderModel(null, 1, null, null, null, null, null, null, null, null, null, null, 0, true, null, null, 0, null, 10, 0, 0, null, 0, 0, null, 0, null, "0", 0, null, 0, null, 1, 1, "a", false, 1, 1, "a", false, 0, 0, 0,DateTimeOffset.MinValue);
            model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel();

            return model;
        }
    }
}
