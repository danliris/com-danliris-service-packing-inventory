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
        public string Group { get; private set; }

        #region isi di aval transform
        public string AvalType { get; private set; }

        public double TotalAvalQuantity { get; private set; }

        public double TotalAvalWeight { get; private set; }

        public bool IsTransformedAval { get; private set; }
        #endregion

        #region Shipping

        public string ShippingType { get; private set; }
        #endregion

        public ICollection<DyeingPrintingAreaInputProductionOrderModel> DyeingPrintingAreaInputProductionOrders { get; private set; }

        public DyeingPrintingAreaInputModel()
        {
            DyeingPrintingAreaInputProductionOrders = new HashSet<DyeingPrintingAreaInputProductionOrderModel>();
        }

        /// <summary>
        /// Main Constructor
        /// </summary>
        /// <param name="date"></param>
        /// <param name="area"></param>
        /// <param name="shift"></param>
        /// <param name="bonNo"></param>
        /// <param name="group"></param>
        /// <param name="dyeingPrintingAreaInputProductionOrders"></param>
        public DyeingPrintingAreaInputModel(DateTimeOffset date, string area, string shift, string bonNo, string group,
            ICollection<DyeingPrintingAreaInputProductionOrderModel> dyeingPrintingAreaInputProductionOrders)
        {
            Date = date;
            Area = area;
            Shift = shift;
            BonNo = bonNo;
            Group = group;
            DyeingPrintingAreaInputProductionOrders = dyeingPrintingAreaInputProductionOrders;
        }

        /// <summary>
        /// Aval Transformasi
        /// </summary>
        /// <param name="date"></param>
        /// <param name="area"></param>
        /// <param name="shift"></param>
        /// <param name="bonNo"></param>
        /// <param name="group"></param>
        /// <param name="avalType"></param>
        /// <param name="isTransformedAval"></param>
        /// <param name="totalAvalQuantity"></param>
        /// <param name="totalAvalWeight"></param>
        /// <param name="dyeingPrintingAreaInputProductionOrders"></param>
        public DyeingPrintingAreaInputModel(DateTimeOffset date, string area, string shift, string bonNo, string group, string avalType, bool isTransformedAval, double totalAvalQuantity,
            double totalAvalWeight, ICollection<DyeingPrintingAreaInputProductionOrderModel> dyeingPrintingAreaInputProductionOrders)
        {
            Date = date;
            Area = area;
            Shift = shift;
            BonNo = bonNo;
            Group = group;
            AvalType = avalType;
            IsTransformedAval = isTransformedAval;
            TotalAvalQuantity = totalAvalQuantity;
            TotalAvalWeight = totalAvalWeight;
            DyeingPrintingAreaInputProductionOrders = dyeingPrintingAreaInputProductionOrders;
        }

        /// <summary>
        /// Area Shipping
        /// </summary>
        /// <param name="date"></param>
        /// <param name="area"></param>
        /// <param name="shift"></param>
        /// <param name="bonNo"></param>
        /// <param name="group"></param>
        /// <param name="shippingType"></param>
        /// <param name="dyeingPrintingAreaInputProductionOrders"></param>
        public DyeingPrintingAreaInputModel(DateTimeOffset date, string area, string shift, string bonNo, string group, string shippingType,
            ICollection<DyeingPrintingAreaInputProductionOrderModel> dyeingPrintingAreaInputProductionOrders)
        {
            Date = date;
            Area = area;
            Shift = shift;
            BonNo = bonNo;
            Group = group;
            ShippingType = shippingType;
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

        public void SetGroup(string newGroup, string user, string agent)
        {
            if (newGroup != Group)
            {
                Group = newGroup;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetIsTransformedAval(bool newFlagIsTransformedAval, string user, string agent)
        {
            if(newFlagIsTransformedAval != IsTransformedAval)
            {
                IsTransformedAval = newFlagIsTransformedAval;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetTotalAvalQuantity(double newTotalAvalQuantity, string user, string agent)
        {
            if (newTotalAvalQuantity != TotalAvalQuantity)
            {
                TotalAvalQuantity = newTotalAvalQuantity;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetTotalAvalWeight(double newTotalAvalWeight, string user, string agent)
        {
            if (newTotalAvalWeight != TotalAvalWeight)
            {
                TotalAvalWeight = newTotalAvalWeight;
                this.FlagForUpdate(user, agent);
            }
        }

    }
}
