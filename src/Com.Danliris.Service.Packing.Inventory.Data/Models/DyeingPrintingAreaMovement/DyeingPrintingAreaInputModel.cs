using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaInputModel : StandardEntity
    {
        public DateTimeOffset Date { get; private set; }
        public string Area { get; private set; }
        public string Shift { get; private set; }
        public string BonNo { get; private set; }
        public ICollection<DyeingPrintingAreaInputProductionOrderModel> DyeingPrintingAreaInputProductionOrders { get; private set; }

        public DyeingPrintingAreaInputModel()
        {
            DyeingPrintingAreaInputProductionOrders = new HashSet<DyeingPrintingAreaInputProductionOrderModel>();
        }

        public DyeingPrintingAreaInputModel(DateTimeOffset date, string area, string shift, string bonNo,
            ICollection<DyeingPrintingAreaInputProductionOrderModel> dyeingPrintingAreaInputProductionOrders)
        {
            Date = date;
            Area = area;
            Shift = shift;
            BonNo = bonNo;
            DyeingPrintingAreaInputProductionOrders = dyeingPrintingAreaInputProductionOrders;
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

        public void SetShift(string newShift, string user, string agent)
        {
            if (newShift != Shift)
            {
                Shift = newShift;
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
