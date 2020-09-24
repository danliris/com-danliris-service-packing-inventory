using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaOutputProductionOrderDataUtil : BaseDataUtil<DyeingPrintingAreaOutputProductionOrderRepository, DyeingPrintingAreaOutputProductionOrderModel>
    {
        public DyeingPrintingAreaOutputProductionOrderDataUtil(DyeingPrintingAreaOutputProductionOrderRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaOutputProductionOrderModel GetModel()
        {
            var model = new DyeingPrintingAreaOutputProductionOrderModel("IM", "AVAL", false, 1, "a", "e", 1, "rr", "1", "as", "test", "unit", "color", "motif", "mtr", "rem", "a", "a", 1, 1, 1, "type", 1, "name", 1, "a", "1", "qc1", "zimmer", "a", 1, "a", 1, "a", 1, 1, "a", false);
            model.DyeingPrintingAreaOutput = new DyeingPrintingAreaOutputModel();

            return model;
        }

        public override DyeingPrintingAreaOutputProductionOrderModel GetEmptyModel()
        {
            var model = new DyeingPrintingAreaOutputProductionOrderModel(null, null, true, 0, null, null, 0, null, null, null, null, null, null, null, null, null, null, null, 0, 1, 0, null, 0, null, 0, null, "0", null, null, null, 0, null, 0, null, 0, 0, null, true);
            model.DyeingPrintingAreaOutput = new DyeingPrintingAreaOutputModel();

            return model;
        }
    }
}
