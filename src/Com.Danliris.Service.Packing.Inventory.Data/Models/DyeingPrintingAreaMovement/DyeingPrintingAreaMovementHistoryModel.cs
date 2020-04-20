using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public enum AreaEnum
    {
        IM,
        PROD,
        TRANSIT,
        PACK,
        GUDANGJADI,
        SHIP,
        AVAL,
        LAB
    }

    public class DyeingPrintingAreaMovementHistoryModel : StandardEntity
    {
        public DyeingPrintingAreaMovementHistoryModel()
        {

        }

        public DyeingPrintingAreaMovementHistoryModel(DateTimeOffset date, 
                                                      string area, 
                                                      string shift, 
                                                      //string uomUnit, 
                                                      //double productionOrderQuantity, 
                                                      AreaEnum index)
        {
            Date = date;
            Area = area;
            Index = index;
            Shift = shift;
            //UOMUnit = uomUnit;
            //ProductionOrderQuantity = productionOrderQuantity;
        }
        public DyeingPrintingAreaMovementHistoryModel(DateTimeOffset date, string area, string shift, AreaEnum index,decimal balance,string grade)
        {
            Date = date;
            Area = area;
            Index = index;
            Shift = shift;
            Balance = balance;
            Grade = grade;
        }

        public DateTimeOffset Date { get; private set; }
        public string Area { get; private set; }
        public string Shift { get; private set; }
        //public string UOMUnit { get; private set; }
        public double ProductionOrderQuantity { get; private set; }
        public double QtyKg { get; private set; }
        public AreaEnum Index { get; private set; }
        public decimal Balance { get; private set; }
        public string Grade { get; private set; }
        public decimal PackagingQty { get; set; }
        public string PackagingUnit { get; set; }

        public int DyeingPrintingAreaMovementId { get; set; }
        public virtual DyeingPrintingAreaMovementModel DyeingPrintingAreaMovement { get; set; }


        public void SetDate(DateTimeOffset newDate, string user, string agent)
        {
            if(newDate != Date)
            {
                Date = newDate;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetArea(string newArea, string user, string agent)
        {
            if (newArea != Area)
            {
                Area = newArea;
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

        public void SetBalance(decimal newBalance, string user,string agent)
        {
            if(newBalance != Balance)
            {
                Balance = newBalance;
                this.FlagForUpdate(user, agent);
            }
        }
        public void SetGrade(string newGrade, string user, string agent)
        {
            if (newGrade != Grade)
            {
                Grade = newGrade;
                this.FlagForUpdate(user, agent);
            }
        }
        public void SetIndexArea(AreaEnum newArea,string user,string agent)
        {
            if(newArea!= Index)
            {
                Area = Enum.GetName(typeof(AreaEnum), newArea);
                Index = newArea;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPackagingQty(decimal packQty, string user, string agent)
        {
            if(packQty != PackagingQty)
            {
                PackagingQty = packQty;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPackagingUnit(string packUnit,string user, string agent)
        {
            if(packUnit != PackagingUnit)
            {
                PackagingUnit = packUnit;
                this.FlagForUpdate(user, agent);
            }
        }

        //public void SetUOMUnit(string newUOMUnit, string user, string agent)
        //{
        //    if (newUOMUnit != UOMUnit)
        //    {
        //        UOMUnit = newUOMUnit;
        //        this.FlagForUpdate(user, agent);
        //    }
        //}

        //public void SetProductionOrderQuantity(double newProductionOrderQuantity, string user, string agent)
        //{
        //    if (newProductionOrderQuantity != ProductionOrderQuantity)
        //    {
        //        ProductionOrderQuantity = newProductionOrderQuantity;
        //        this.FlagForUpdate(user, agent);
        //    }
        //}

        //public void SetQtyKg(double newQtyKg, string user, string agent)
        //{
        //    if (newQtyKg != QtyKg)
        //    {
        //        QtyKg = newQtyKg;
        //        this.FlagForUpdate(user, agent);
        //    }
        //}

    }
}
