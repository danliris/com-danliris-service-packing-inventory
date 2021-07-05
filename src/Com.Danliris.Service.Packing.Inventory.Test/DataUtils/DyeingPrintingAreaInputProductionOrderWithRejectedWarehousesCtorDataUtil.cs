using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaInputProductionOrderWithRejectedWarehousesCtorDataUtil : BaseDataUtil<DyeingPrintingAreaInputProductionOrderRepository, DyeingPrintingAreaInputProductionOrderModel>
    {
        public DyeingPrintingAreaInputProductionOrderWithRejectedWarehousesCtorDataUtil(DyeingPrintingAreaInputProductionOrderRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaInputProductionOrderModel GetModel()
        {
            var model = new DyeingPrintingAreaInputProductionOrderModel(123, "asdf", "asdf", "asdf", "asdf", "asdf", "asdf", "asdf", "asdf", 123, true, "asdf", "asdf", 123, "asdf", "asdf", "asdf", 123, 123, 123, "asdf", "asdf", 123, 123, "asdf", "asdf", "asdf", 123, "asdf", 123, 123, 1, 1, "a", 1, "a", "1", 1, "a", 1, "a", 1, 1, "a", false, 1, 1, "a", false, 1, 2, 2, "a", "a");
            model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel();

            return model;
        }

        public override DyeingPrintingAreaInputProductionOrderModel GetEmptyModel()
        {
            var model = new DyeingPrintingAreaInputProductionOrderModel(0, null, null, null, null, null, null, null, null, 0, false, null, null, 123, null, null, null, 123, 123, 123, null, null, 0, 0, null, null, null, 0, null, 0, 0, 0, 0, null, 0, null, "0", 0, null, 0, null, 0, 0, null, true, 0, 0, null, true, 0, 0, 0, null, null);
            model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel();

            return model;
        }
    }
}
