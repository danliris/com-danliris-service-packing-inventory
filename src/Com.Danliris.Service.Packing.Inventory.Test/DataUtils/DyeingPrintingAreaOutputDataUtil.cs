using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaOutputDataUtil : BaseDataUtil<DyeingPrintingAreaOutputRepository, DyeingPrintingAreaOutputModel>
    {
        public DyeingPrintingAreaOutputDataUtil(DyeingPrintingAreaOutputRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaOutputModel GetModel()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "IM", "pa", "1", false, "TRANSIT", "A", "OUT", new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel("TRANSIT","PACKING",false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2, 1,1, 1, "name", 1, "a", "1","a",1,"a","a",1,"a","a",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false),
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,1, "type", 1, "name", 1, "a", "1","qc","a",1,"a",1,"a",1,1,"s",false),
                new DyeingPrintingAreaOutputProductionOrderModel("SHIPPING","TRANSIT",false,1,"a",1,"a","a0,",1,"s","s","s","s","d","d","e","note",1,1, "unit","type",1,1, false,"s","s",1, 1, "name", 1, "a", "1","1","1","a",1,"a",1,"a"),
                new DyeingPrintingAreaOutputProductionOrderModel("GUDANG AVAL",true,"Aval Sambungan",1,1,"no")

            });
        }

        public DyeingPrintingAreaOutputModel GetModelShippingPenjualan()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "SHIPPING", "pa", "1", false, "PENJUALAN", "A", 1, "no", false, "OUT", "a", new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                 new DyeingPrintingAreaOutputProductionOrderModel("SHIPPING","PENJUALAN",false,1,"a",1,"a","a0,",1,"s","s","s","s","d","d","e",null,1,1, "unit","type",1,1, false,"s","s",1,1, "name", 1, "a", "1","1","1","a",1,"a",1,"a")

            });
        }

        public DyeingPrintingAreaOutputModel GetModelShippingPenjualanAfter()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "SHIPPING", "pa", "1", false, "PENJUALAN", "A", 1, "no", false, "OUT", "a", new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                 new DyeingPrintingAreaOutputProductionOrderModel("SHIPPING","PENJUALAN",false,1,"a",1,"a","a0,",1,"s","s","s","s","d","d","e","note",1,1, "unit","type",1,1, false,"s","s",1,1, "name", 1, "a", "1","1","1","a",1,"a",1,"a")

            });
        }

        public DyeingPrintingAreaOutputModel GetModelShippingBuyer()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "SHIPPING", "pa", "1", false, "BUYER", "A", 1, "no", false, "OUT", "a", new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                 new DyeingPrintingAreaOutputProductionOrderModel("SHIPPING","PENJUALAN",false,1,"a",1,"a","a0,",1,"s","s","s","s","d","d","e",null,1,1, "unit","type",1,1, false,"s","s",1,1, "name", 1, "a", "1","1","1","a",1,"a",1,"a")

            });
        }

        public DyeingPrintingAreaOutputModel GetModelShippingBuyerAfter()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "SHIPPING", "pa", "1", false, "BUYER", "A", 1, "no", false, "OUT", "a", new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                 new DyeingPrintingAreaOutputProductionOrderModel("SHIPPING","PENJUALAN",false,1,"a",1,"a","a0,",1,"s","s","s","s","d","d","e","note",1,1, "unit","type",1,1, false,"s","s",1,1, "name", 1, "a", "1","1","1","a",1,"a",1,"a")

            });
        }

        public DyeingPrintingAreaOutputModel GetModelForUpdateAfter()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "IM", "pa", "1", false, "TRANSIT", "A", "OUT", new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,1, "type",1, "name", 1, "a", "1","qc","a",1,"a",1,"a",1,1,"s",false),

            });
        }

        public DyeingPrintingAreaOutputModel GetModelForUpdateBefore()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "IM", "pa", "1", false, "TRANSIT", "A", "OUT", new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,1, "type",1, "name", 1, "a", "1","qc","a",1,"a",1,"a",1,1,"s",false),

            });
        }

        public DyeingPrintingAreaOutputModel GetModelForUpdateAfter2()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "IM", "pa", "1", false, "TRANSIT", "A", "OUT", new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,1, "type",1, "name", 1, "a", "1","qc","a",1,"a",1,"a",1,1,"s",false),

            });
        }

        public DyeingPrintingAreaOutputModel GetEmptyModelBefore()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, null, null, null, false, null, null, null, new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,0, null,0,null,0,null,"0",null,null,0,null,0,null,0,0,null,true)
            });
        }

        public override DyeingPrintingAreaOutputModel GetEmptyModel()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, null, null, null, true, null, null, null, new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,true,0,null,null,0,null,null,null,null,null,null,null,null,null,null,null,1,1,0,0,null,0,null,"0",null,0,null,null,0,null,null,0,null,0,null,0,0,null,true,0,0,null,true),
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,true,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,0, null,0,null,0,null,"0",null,null,0,null,0,null,0,0,null,true),
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,true,0,null,0,null,null,1,null,null,null,null,null,null,null,null,0,1, null, null, 0,0, true,null,null,0,0,null,0,null,"0",null,null,null,0,null,0,null),
                new DyeingPrintingAreaOutputProductionOrderModel(null,false,null, 0, 0,null)

            });
        }

        public DyeingPrintingAreaOutputModel GetWithDOModel()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "IM", "pa", "1", false, "TRANSIT", "A", 1, "Np", false, "OUT", "a", new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2, 1,1,1, "name", 1, "a", "1","a",1,"a","a",1,"a","a",1,"a",1,"a", 1, 1, "a", false, 1, 1, "a", false),
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,1, "type",1, "name", 1, "a", "1","a","a",1,"a",1,"a",1,1,"a",false),
                new DyeingPrintingAreaOutputProductionOrderModel("IM","TRANSIT",false,1,"a",1,"a","a0,",1,"s","s","s","s","d","d","e","note",1,1, "unit","type,",1,1, false,"s","s",1,1, "name", 1, "a", "1","1","1","a",1,"a",1,"a"),
                new DyeingPrintingAreaOutputProductionOrderModel("GUDANG AVAL",true,"Aval Sambungan",1,1,"no")

            });
        }

        public DyeingPrintingAreaOutputModel GetEmptyWithDOModel()
        {
            return new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, null, null, null, true, null, null, 0, null, true, null, null, new List<DyeingPrintingAreaOutputProductionOrderModel>()
            {
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,true,0,null,null,0,null,null,null,null,null,null,null,null,null,null,null,1,1,0,0,null,0,null,"0",null,0,null,null,0,null,null,0,null,0,null,0,0,null,true,0,0,null,true),
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,true,1,"a","e",1,"rr","1","as","test","unit","color","motif","mtr", "rem","a","a",2,1,0, null,0,null,0,null,"0",null,null,0,null,0,null,0,0,null,true),
                new DyeingPrintingAreaOutputProductionOrderModel(null,null,true,0,null,0,null,null,1,null,null,null,null,null,null,null,null,0,1,null,null,0,0, true,null,null,0,0,null,0,null,"0",null,null,null,0,null,0,null),
                new DyeingPrintingAreaOutputProductionOrderModel(null,false,null, 0, 0,null)

            });
        }
    }
}
