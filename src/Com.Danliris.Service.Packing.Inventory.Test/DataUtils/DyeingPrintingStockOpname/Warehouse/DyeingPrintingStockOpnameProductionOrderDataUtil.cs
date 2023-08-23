using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.DyeingPrintingStockOpname.Warehouse
{
 public   class DyeingPrintingStockOpnameProductionOrderDataUtil 
        : BaseDataUtil<DyeingPrintingStockOpnameProductionOrderRepository, DyeingPrintingStockOpnameProductionOrderModel>
    {
        public DyeingPrintingStockOpnameProductionOrderDataUtil(DyeingPrintingStockOpnameProductionOrderRepository repository) : base(repository)
        {

        }
        public override DyeingPrintingStockOpnameProductionOrderModel GetModel()
        {
            var model = new DyeingPrintingStockOpnameProductionOrderModel(1,1,"buyer","color", "construction","documentNo","A",1,"MaterialConstructionName",1,"MaterialName","MaterialWitdh","Motif","PackingInstruction",1,1,"PackagingType","PackagingUnit",1,"ProductionorederName","productionOrderType",1,1,"ProcessTypeName",1,"yarnMaterialName","Remark","Status","Unit","UomUnit", false, null, 1, "TrackType", "TrackName", "TrackBox", DateTime.Now, "Description", "");
            model.DyeingPrintingStockOpname = new DyeingPrintingStockOpnameModel();
            return model;
        }

        public override DyeingPrintingStockOpnameProductionOrderModel GetEmptyModel()
        {
            var model = new DyeingPrintingStockOpnameProductionOrderModel(0, 0, null, null, null, null, null, 0, null, 0, null, null, null, null, 0, 0, null, null, 0, null, null, 0, 0, null, 0, null, null, null, null, null, false, null,0, null,null, null, DateTime.Now, null, null);
            model.DyeingPrintingStockOpname = new DyeingPrintingStockOpnameModel();
            return model;
        }
    

    }
}
