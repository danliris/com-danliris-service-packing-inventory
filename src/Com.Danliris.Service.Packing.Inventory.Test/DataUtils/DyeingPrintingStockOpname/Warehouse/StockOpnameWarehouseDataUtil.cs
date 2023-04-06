using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.DyeingPrintingStockOpname.Warehouse
{
    public class StockOpnameWarehouseDataUtil : BaseDataUtil<DyeingPrintingStockOpnameRepository, DyeingPrintingStockOpnameModel>
    {
        public StockOpnameWarehouseDataUtil(DyeingPrintingStockOpnameRepository repository) : base(repository)
        {

        }
        public override DyeingPrintingStockOpnameModel GetModel()
        {
            var stockOpnameProductionOrder = new DyeingPrintingStockOpnameProductionOrderModel(1,1,"buyer","color", "construction","documentNo","A",1,"MaterialConstructionName",1,"MaterialName","MaterialWitdh","Motif","PackingInstruction",1,1,"PackagingType","PackagingUnit",1,"ProductionorederName","productionOrderType",1,1,"ProcessTypeName",1,"yarnMaterialName","Remark","Status","Unit","UomUnit", false, null, 1, "TrackType", "TrackName", "TrackBox", DateTime.Now);
           
            var stockOpnameProductionOrders = new List<DyeingPrintingStockOpnameProductionOrderModel>();
            stockOpnameProductionOrders.Add(stockOpnameProductionOrder);

            return new DyeingPrintingStockOpnameModel(
                "GUDANG_JADI", "BON_NO",DateTimeOffset.Now, DyeingPrintingArea.STOCK_OPNAME, stockOpnameProductionOrders, false
                );
        }

        public override DyeingPrintingStockOpnameModel GetEmptyModel()
        {
            var stockOpnameProductionOrder = new DyeingPrintingStockOpnameProductionOrderModel(0, 0, null, null, null, null, null, 0, null, 0, null, null, null, null, 0, 0, null, null, 0, null, null, 0, 0, null, 0, null, null, null, null, null, false, null, 0, null, null, null, DateTime.Now);

            var stockOpnameProductionOrders = new List<DyeingPrintingStockOpnameProductionOrderModel>();
            stockOpnameProductionOrders.Add(stockOpnameProductionOrder);

            return new DyeingPrintingStockOpnameModel(
                null, null, DateTimeOffset.Now, null, stockOpnameProductionOrders, false
                );
        }


        public  DyeingPrintingStockOpnameModel GetNewModel()
        {
            var stockOpnameProductionOrder = new DyeingPrintingStockOpnameProductionOrderModel(1, 1, "buyer", "color", "construction", "documentNo", "A", 1, "MaterialConstructionName", 1, "MaterialName", "MaterialWitdh", "Motif", "PackingInstruction", 1, 1, "PackagingType", "PackagingUnit", 1, "ProductionorederName", "productionOrderType", 1, 1, "ProcessTypeName", 1, "yarnMaterialName", "Remark", "Status", "Unit", "UomUnit", false, null, 1, "TrackType", "TrackName", "TrackBox", DateTime.Now);

            var stockOpnameProductionOrders = new List<DyeingPrintingStockOpnameProductionOrderModel>();
            stockOpnameProductionOrders.Add(stockOpnameProductionOrder);

            return new DyeingPrintingStockOpnameModel(
                "GUDANG_JADI", "BON_NO", DateTimeOffset.Now, DyeingPrintingArea.STOCK_OPNAME, stockOpnameProductionOrders, false
                );
        }
    }
}
