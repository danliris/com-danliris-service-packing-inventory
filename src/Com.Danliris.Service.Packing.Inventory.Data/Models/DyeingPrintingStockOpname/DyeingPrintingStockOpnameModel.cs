using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname
{
public    class DyeingPrintingStockOpnameModel : StandardEntity
    {
        public string Area { get; private set; }
        public string BonNo { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public string Type { get; private set; }
       
        public ICollection<DyeingPrintingStockOpnameProductionOrderModel> DyeingPrintingStockOpnameProductionOrders { get; set; }
        public DyeingPrintingStockOpnameModel()
        {
            DyeingPrintingStockOpnameProductionOrders = new HashSet<DyeingPrintingStockOpnameProductionOrderModel>();
        }
        public DyeingPrintingStockOpnameModel(string area, string bonNo,  DateTimeOffset date, string type, ICollection<DyeingPrintingStockOpnameProductionOrderModel> dyeingPrintingStockOpnameProductionOrders)
        {
            Area = area;
            BonNo = bonNo;
            Date = date;
            Type = type;
            DyeingPrintingStockOpnameProductionOrders = dyeingPrintingStockOpnameProductionOrders;
        }

        public void SetArea(string newArea, string user, string agent)
        {
            if (newArea != Area)
            {
                Area = newArea;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetDate(DateTimeOffset newDate, string user, string agent)
        {
            if (newDate != Date)
            {
                Date = newDate;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetBonNo(string newBonNo, string user, string agent)
        {
            if (newBonNo != BonNo)
            {
                BonNo = newBonNo;
                this.FlagForUpdate(user, agent);
            }
        }
    }
}
