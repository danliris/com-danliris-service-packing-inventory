using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaOutputAvalItemModel : StandardEntity
    {
        public string Type { get; private set; }
        public double Length { get; private set; }

        public int DyeingPrintingAreaOutputProductionOrderId { get; set; }
        public DyeingPrintingAreaOutputProductionOrderModel DyeingPrintingAreaOutputProductionOrder { get; set; }

        public DyeingPrintingAreaOutputAvalItemModel()
        {

        }

        public DyeingPrintingAreaOutputAvalItemModel(string type, double length)
        {
            Type = type;
            Length = length;
        }

        public void SetType(string newType, string user, string agent)
        {
            if (newType != Type)
            {
                Type = newType;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetLength(double newLength, string user, string agent)
        {
            if (newLength != Length)
            {
                Length = newLength;
                this.FlagForUpdate(user, agent);
            }
        }
    }
}
