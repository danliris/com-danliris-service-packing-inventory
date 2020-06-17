using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaOutputProductionOrderWithAvalCtorDataUtil : BaseDataUtil<DyeingPrintingAreaOutputProductionOrderRepository, DyeingPrintingAreaOutputProductionOrderModel>
    {
        public DyeingPrintingAreaOutputProductionOrderWithAvalCtorDataUtil(DyeingPrintingAreaOutputProductionOrderRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaOutputProductionOrderModel GetModel()
        {
            var model = new DyeingPrintingAreaOutputProductionOrderModel("IM", "AVAL", false, 1, "test", 1, "43124", "asdf", 1, "asdf", "asdfsad", "asf", "asdf", "asdf", "asdfa", "asfas", "note", 1, 1, "unit", "type", 1, 1, false, "s", "s", 1, 1, "name", 1, "a", "1");
            model.DyeingPrintingAreaOutput = new DyeingPrintingAreaOutputModel();

            return model;
        }

        public override DyeingPrintingAreaOutputProductionOrderModel GetEmptyModel()
        {
            var model = new DyeingPrintingAreaOutputProductionOrderModel(null, null, true, 0, null, 0, null, null, 0, null, null, null, null, null, null, null, null, 0, 1, null, null, 0, 0, true, null, null, 0, 0, null, 0, null, "0");
            model.DyeingPrintingAreaOutput = new DyeingPrintingAreaOutputModel();

            return model;
        }
    }
}
