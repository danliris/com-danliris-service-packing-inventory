using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaMovementModel : StandardEntity
    {
        public DateTimeOffset Date { get; private set; }
        public string Area { get; private set; }
        public string Type { get; private set; }
        public int DyeingPrintingAreaDocumentId { get; private set; }
        public int DyeingPrintingAreaProductionOrderDocumentId { get; private set; }
        public string DyeingPrintingAreaDocumentBonNo { get; private set; }


        public long ProductionOrderId { get; private set; }
        public string ProductionOrderNo { get; private set; }
        public string CartNo { get; private set; }
        public string Buyer { get; private set; }
        public string Construction { get; private set; }
        public string Unit { get; private set; }
        public string Color { get; private set; }
        public string Motif { get; private set; }
        public string UomUnit { get; private set; }
        public double Balance { get; private set; }

        public DyeingPrintingAreaMovementModel()
        {

        }

        public DyeingPrintingAreaMovementModel(DateTimeOffset date, string area, string type, int dyeingPrintingAreaDocumentId, string dyeingPrintingAreaDocumentBonNo,
            long productionOrderId, string productionOrderNo, string cartNo, string buyer, string construction, string unit, string color,
            string motif, string uomUnit, double balance, int dyeingPrintingAreaProductionOrderDocumentId) : this(date, area, type, dyeingPrintingAreaDocumentId, 
                dyeingPrintingAreaDocumentBonNo, productionOrderId, productionOrderNo, cartNo, buyer, construction, unit, color, motif, uomUnit, balance)
        {
            DyeingPrintingAreaProductionOrderDocumentId = dyeingPrintingAreaProductionOrderDocumentId;
        }

        public DyeingPrintingAreaMovementModel(DateTimeOffset date, string area, string type, int dyeingPrintingAreaDocumentId, string dyeingPrintingAreaDocumentBonNo,
            long productionOrderId, string productionOrderNo, string cartNo, string buyer, string construction, string unit, string color,
            string motif, string uomUnit, double balance)
        {
            Date = date;
            Area = area;
            Type = type;
            DyeingPrintingAreaDocumentId = dyeingPrintingAreaDocumentId;
            DyeingPrintingAreaDocumentBonNo = dyeingPrintingAreaDocumentBonNo;
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            CartNo = cartNo;
            Buyer = buyer;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Balance = balance;
        }

        public DyeingPrintingAreaMovementModel(DateTimeOffset date,
                                               string area,
                                               string type,
                                               int dyeingPrintingAreaDocumentId,
                                               string dyeingPrintingAreaDocumentBonNo,
                                               string cartNo,
                                               string uomUnit,
                                               double balance)
        {
            Date = date;
            Area = area;
            Type = type;
            DyeingPrintingAreaDocumentId = dyeingPrintingAreaDocumentId;
            DyeingPrintingAreaDocumentBonNo = dyeingPrintingAreaDocumentBonNo;
            CartNo = cartNo;
            UomUnit = uomUnit;
            Balance = balance;
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

        public void SetType(string newType, string user, string agent)
        {
            if (newType != Type)
            {
                Type = newType;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetDyeingPrintingAreaDocument(int newId, string newBonNo, string user, string agent)
        {
            if (newId != DyeingPrintingAreaDocumentId)
            {
                DyeingPrintingAreaDocumentId = newId;
                this.FlagForUpdate(user, agent);
            }

            if (newBonNo != DyeingPrintingAreaDocumentBonNo)
            {
                DyeingPrintingAreaDocumentBonNo = newBonNo;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetProductionOrder(long newProductionOrderId, string newProductionOrderNo, string user, string agent)
        {
            if (newProductionOrderId != ProductionOrderId)
            {
                ProductionOrderId = newProductionOrderId;
                this.FlagForUpdate(user, agent);
            }

            if (newProductionOrderNo != ProductionOrderNo)
            {
                ProductionOrderNo = newProductionOrderNo;
                this.FlagForUpdate(user, agent);
            }

        }

        public void SetBuyer(string newBuyer, string user, string agent)
        {
            if (newBuyer != Buyer)
            {
                Buyer = newBuyer;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetCartNo(string newCartNo, string user, string agent)
        {
            if (newCartNo != CartNo)
            {
                CartNo = newCartNo;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetConstruction(string newConstruction, string user, string agent)
        {
            if (newConstruction != Construction)
            {
                Construction = newConstruction;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetUnit(string newUnit, string user, string agent)
        {

            if (newUnit != Unit)
            {
                Unit = newUnit;
                this.FlagForUpdate(user, agent);
            }

        }

        public void SetColor(string newColor, string user, string agent)
        {
            if (newColor != Color)
            {
                Color = newColor;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetMotif(string newMotif, string user, string agent)
        {
            if (newMotif != Motif)
            {
                Motif = newMotif;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetUomUnit(string newUomUnit, string user, string agent)
        {
            if (newUomUnit != UomUnit)
            {
                UomUnit = newUomUnit;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetBalance(double newBalance, string user, string agent)
        {
            if (newBalance != Balance)
            {
                Balance = newBalance;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetDyeingPrintingAreaProductionOrderDocumentId(int newId, string user, string agent)
        {
            if (newId != DyeingPrintingAreaProductionOrderDocumentId)
            {
                DyeingPrintingAreaProductionOrderDocumentId = newId;
                this.FlagForUpdate(user, agent);
            }
        }
    }
}
