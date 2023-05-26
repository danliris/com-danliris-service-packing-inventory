using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaInputProductionOrderDataUtil : BaseDataUtil<DyeingPrintingAreaInputProductionOrderRepository, DyeingPrintingAreaInputProductionOrderModel>
    {
        public DyeingPrintingAreaInputProductionOrderDataUtil(DyeingPrintingAreaInputProductionOrderRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaInputProductionOrderModel GetModel()
        {
            var model = new DyeingPrintingAreaInputProductionOrderModel("IM", 1, "no", "type", 1, "ins", "1", "bb", "ma", "unit", "col", "mot", "uuni", 2, 2, false, 1, 1, 1, "name", 1, "a", "1", 1, "a", 1, "a", 1, "a",DateTimeOffset.Now, "a", 1, "a", "a", DateTime.Now);
            //var model = new DyeingPrintingAreaInputProductionOrderModel("IM", 1, "no", "type", 1, "ins", "1", "bb", "ma", "unit", "col", "mot", "uuni", 2, 2, false, 1, 1, 1, "name", 1, "a", "1", 1, "a", 1, "a", 1, DateTimeOffset.Now);
            model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel();

            return model;
        }

        public DyeingPrintingAreaInputProductionOrderModel GetModelShipping()
        {
            var model = new DyeingPrintingAreaInputProductionOrderModel("SHIPPING", 1, "no", "type", 1, "ins", "1", "bb", "ma", "unit", "col", "mot", "uuni", 2, 2, false, 1, 1, 1, "name", 1, "a", "1", 1, "a", 1, "a", 1, "a",DateTimeOffset.Now, "a", 1, "a", "a", DateTime.Now);
            //var model = new DyeingPrintingAreaInputProductionOrderModel("SHIPPING", 1, "no", "type", 1, "ins", "1", "bb", "ma", "unit", "col", "mot", "uuni", 2, 2, false, 1, 1, 1, "name", 1, "a", "1", 1, "a", 1, "a", 1, DateTimeOffset.Now);
            model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel();

            return model;
        }

        public DyeingPrintingAreaInputProductionOrderModel GetModelGudangJadi()
        {
            var model = new DyeingPrintingAreaInputProductionOrderModel("GUDANG JADI", 1, "no", "type", 1, "ins", "1", "bb", "ma", "unit", "col", "mot", "uuni", 2, 2, false, 1, 1, 1, "name", 1, "a", "1", 1, "a", 1, "a", 1, "a",DateTimeOffset.Now, "a", 1, "a", "a", DateTime.Now);
          //  var model = new DyeingPrintingAreaInputProductionOrderModel("GUDANG JADI", 1, "no", "type", 1, "ins", "1", "bb", "ma", "unit", "col", "mot", "uuni", 2, 2, false, 1, 1, 1, "name", 1, "a", "1", 1, "a", 1, "a", 1, DateTimeOffset.Now);
            model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel();

            return model;
        }

        public DyeingPrintingAreaInputProductionOrderModel GetModelTransit()
        {
            var model = new DyeingPrintingAreaInputProductionOrderModel("TRANSIT", 1, "no", "type", 1, "ins", "1", "bb", "ma", "unit", "col", "mot", "uuni", 2, 2, false, 1, 1, 1, "name", 1, "a", "1", 1, "a", 1, "a", 1, "a", DateTimeOffset.Now, "a", 1, "a", "a", DateTime.Now);
            //var model = new DyeingPrintingAreaInputProductionOrderModel("TRANSIT", 1, "no", "type", 1, "ins", "1", "bb", "ma", "unit", "col", "mot", "uuni", 2, 2, false, 1, 1, 1, "name", 1, "a", "1", 1, "a", 1, "a", 1, DateTimeOffset.Now);
            model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel();

            return model;
        }

        public override DyeingPrintingAreaInputProductionOrderModel GetEmptyModel()
        {
            var model = new DyeingPrintingAreaInputProductionOrderModel(null, 0, null, null, 0, null, null, null, null, null, null, null, null, 1, 1, true, 0, 0, 0, null, 0, null, "0", 0, null, 0, null, 0, null,DateTimeOffset.Now, null, 0, null, null, DateTime.Now);
           // var model = new DyeingPrintingAreaInputProductionOrderModel(null, 0, null, null, 0, null, null, null, null, null, null, null, null, 1, 1, true, 0, 0, 0, null, 0, null, "0", 0, null, 0, null, 0, DateTimeOffset.Now);
            model.DyeingPrintingAreaInput = new DyeingPrintingAreaInputModel();

            return model;
        }
    }
}
