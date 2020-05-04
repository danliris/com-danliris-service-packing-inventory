using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaOutputModel : StandardEntity
    {
        public DateTimeOffset Date { get; private set; }
        public string Area { get; private set; }
        public string Shift { get; private set; }
        public string BonNo { get; private set; }
        public bool HasNextAreaDocument { get; private set; }
        public string DestinationArea { get; private set; }
        public ICollection<DyeingPrintingAreaOutputProductionOrderModel> DyeingPrintingAreaOutputProductionOrders { get; private set; }

        public DyeingPrintingAreaOutputModel()
        {
            DyeingPrintingAreaOutputProductionOrders = new HashSet<DyeingPrintingAreaOutputProductionOrderModel>();
        }

        public DyeingPrintingAreaOutputModel(DateTimeOffset date, string area, string shift, string bonNo, bool hasNextAreaDocument, 
            string destinationArea, ICollection<DyeingPrintingAreaOutputProductionOrderModel> dyeingPrintingAreaOutputProductionOrders)
        {
            Date = date;
            Area = area;
            Shift = shift;
            BonNo = bonNo;
            HasNextAreaDocument = hasNextAreaDocument;
            DestinationArea = destinationArea;
            DyeingPrintingAreaOutputProductionOrders = dyeingPrintingAreaOutputProductionOrders;
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

        public void SetHasNextAreaDocument(bool newFlagNextAreaDocument, string user, string agent)
        {
            if (newFlagNextAreaDocument != HasNextAreaDocument)
            {
                HasNextAreaDocument = newFlagNextAreaDocument;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetDestinationArea(string newDestinationArea, string user, string agent)
        {
            if (newDestinationArea != DestinationArea)
            {
                DestinationArea = newDestinationArea;
                this.FlagForUpdate(user, agent);
            }
        }

    }
}
