using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class DyeingPrintingAreaMovementModel : StandardEntity
    {
        public readonly string METER = "MTR";
        public readonly string YARDS = "YDS";

        public readonly string OK = "OK";
        public readonly string NOT_OK = "NOT OK";

        public DyeingPrintingAreaMovementModel()
        {
            DyeingPrintingAreaMovementHistories = new HashSet<DyeingPrintingAreaMovementHistoryModel>();
        }

        public DyeingPrintingAreaMovementModel(string area, string bonNo, DateTimeOffset date, string shift, long productionOrderId, string productionOrderCode, string productionOrderNo,
            double productionOrderQuantity, string productionOrderType, string buyer, string packingInstruction, string cartNo, int materialId, string materialCode, string materialName, int materialConstructionId, string materialConstructionCode,
            string materialConstructionName, string materialWidth, int unitId, string unitCode, string unitName, string color, string motif, string mutation,
            double length, string uomUnit, decimal balance,  ICollection<DyeingPrintingAreaMovementHistoryModel> histories)
        {
            Area = area;
            BonNo = bonNo;
            Date = date;
            Shift = shift;
            ProductionOrderId = productionOrderId;
            ProductionOrderCode = productionOrderCode;
            ProductionOrderNo = productionOrderNo;
            ProductionOrderQuantity = productionOrderQuantity;
            Buyer = buyer;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            CartNo = cartNo;
            MaterialId = materialId;
            MaterialCode = materialCode;
            MaterialName = materialName;
            MaterialConstructionId = materialConstructionId;
            MaterialConstructionCode = materialConstructionCode;
            MaterialConstructionName = materialConstructionName;
            MaterialWidth = materialWidth;
            UnitId = unitId;
            UnitCode = unitCode;
            UnitName = unitName;
            Color = color;
            Motif = motif;
            Mutation = mutation;

            ConvertLength(length, uomUnit);

            Construction = GenerateConstruction(materialName, materialConstructionName, materialWidth);

            UOMUnit = uomUnit;
            Balance = balance;
            //Status = status;
            //Grade = grade;
            //SourceArea = sourceArea;
            //Remark = remark;
            DyeingPrintingAreaMovementHistories = histories;

            //MassKg = massKg;
        }

        public DyeingPrintingAreaMovementModel(string area, string shift, string uomUnit, double productionOrderQuantity, double qtyKg, ICollection<DyeingPrintingAreaMovementHistoryModel> histories)
        {
            Area = area;
            //BonNo = bonNo;
            Shift = shift;
            UOMUnit = uomUnit;
            ProductionOrderQuantity = productionOrderQuantity;
            QtyKg = qtyKg;
            DyeingPrintingAreaMovementHistories = histories;
        }

        public string GenerateConstruction(string materialName, string materialConstructionName, string materialWidth)
        {
            return string.Format("{0} / {1} / {2}", materialName, materialConstructionName, materialWidth);
        }

        public void ConvertLength(double length, string uomUnit)
        {
            if (uomUnit == METER)
            {
                MeterLength = length;
                YardsLength = length * 1.093613;
            }
            else if (uomUnit == YARDS)
            {
                YardsLength = length;
                MeterLength = length * 0.9144000;
            }
        }


        public string Area { get; private set; }
        public string BonNo { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public string Shift { get; private set; }
        public long ProductionOrderId { get; private set; }
        public string ProductionOrderCode { get; private set; }
        public string ProductionOrderNo { get; private set; }
        public double ProductionOrderQuantity { get; private set; }
        public string Buyer { get; private set; }
        public string PackingInstruction { get; private set; }
        public string ProductionOrderType { get; private set; }
        public string CartNo { get; private set; }
        public string Construction { get; private set; }
        public int MaterialId { get; private set; }
        public string MaterialCode { get; private set; }
        public string MaterialName { get; private set; }
        public int MaterialConstructionId { get; private set; }
        public string MaterialConstructionCode { get; private set; }
        public string MaterialConstructionName { get; private set; }
        public string MaterialWidth { get; private set; }
        public int UnitId { get; private set; }
        public string UnitCode { get; private set; }
        public string UnitName { get; private set; }
        public string Color { get; private set; }
        public string Motif { get; private set; }
        public string Mutation { get; private set; }
        public double MeterLength { get; private set; }
        public double YardsLength { get; private set; }
        public string UOMUnit { get; private set; }
        public decimal Balance { get; private set; }
        public string Status { get; private set; }
        public string Grade { get; private set; }
        public string SourceArea { get; private set; }
        public string Remark { get; private set; }
        public bool IsChecked { get; private set; }
        public double QtyKg { get; private set; }
        public ICollection<DyeingPrintingAreaMovementHistoryModel> DyeingPrintingAreaMovementHistories { get; private set; }


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

        public void SetProductionOrder(long newProductionOrderId, string newProductionOrderCode, string newProductionOrderNo,
            string newProductionOrderType, string user, string agent)
        {
            if (newProductionOrderId != ProductionOrderId)
            {
                ProductionOrderId = newProductionOrderId;
                this.FlagForUpdate(user, agent);
            }

            if (newProductionOrderCode != ProductionOrderCode)
            {
                ProductionOrderCode = newProductionOrderCode;
                this.FlagForUpdate(user, agent);
            }

            if (newProductionOrderNo != ProductionOrderNo)
            {
                ProductionOrderNo = newProductionOrderNo;
                this.FlagForUpdate(user, agent);
            }

            if (newProductionOrderType != ProductionOrderType)
            {
                ProductionOrderType = newProductionOrderType;
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

        public void SetPackingInstruction(string newPackingInstruction, string user, string agent)
        {
            if (newPackingInstruction != PackingInstruction)
            {
                PackingInstruction = newPackingInstruction;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetProductionOrderQuantity(double newProductionOrderQuantity, string user, string agent)
        {
            if (newProductionOrderQuantity != ProductionOrderQuantity)
            {
                ProductionOrderQuantity = newProductionOrderQuantity;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetCartNumber(string newCartNumber, string user, string agent)
        {
            if (newCartNumber != CartNo)
            {
                CartNo = newCartNumber;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetConstructionData(int newMaterialId, string newMaterialCode, string newMaterialName,
            int newMaterialConstructionId, string newMaterialConstructionCode, string newMaterialConstructionName,
            string newMaterialWidth, string user, string agent)
        {
            string newConstruction = GenerateConstruction(newMaterialName, newMaterialConstructionName, newMaterialWidth);

            if (newMaterialId != MaterialId)
            {
                MaterialId = newMaterialId;
                this.FlagForUpdate(user, agent);
            }

            if (newMaterialCode != MaterialCode)
            {
                MaterialCode = newMaterialCode;
                this.FlagForUpdate(user, agent);
            }

            if (newMaterialName != MaterialName)
            {
                MaterialName = newMaterialName;
                this.FlagForUpdate(user, agent);
            }

            if (newMaterialConstructionId != MaterialConstructionId)
            {
                MaterialConstructionId = newMaterialConstructionId;
                this.FlagForUpdate(user, agent);
            }

            if (newMaterialConstructionCode != MaterialConstructionCode)
            {
                MaterialConstructionCode = newMaterialConstructionCode;
                this.FlagForUpdate(user, agent);
            }

            if (newMaterialConstructionName != MaterialConstructionName)
            {
                MaterialConstructionName = newMaterialConstructionName;
                this.FlagForUpdate(user, agent);
            }

            if (newMaterialWidth != MaterialWidth)
            {
                MaterialWidth = newMaterialWidth;
                this.FlagForUpdate(user, agent);
            }

            if (newConstruction != Construction)
            {
                Construction = newConstruction;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetUnit(int newUnitId, string newUnitCode, string newUnitName, string user, string agent)
        {
            if (newUnitId != UnitId)
            {
                UnitId = newUnitId;
                this.FlagForUpdate(user, agent);
            }

            if (newUnitCode != UnitCode)
            {
                UnitCode = newUnitCode;
                this.FlagForUpdate(user, agent);
            }

            if (newUnitName != UnitName)
            {
                UnitName = newUnitName;
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

        public void SetMutation(string newMutation, string user, string agent)
        {
            if (newMutation != Mutation)
            {
                Mutation = newMutation;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetLength(double newMeterLength, double newYardsLength, string newUomUnit, string user, string agent)
        {
            if (newMeterLength != MeterLength)
            {
                MeterLength = newMeterLength;
                this.FlagForUpdate(user, agent);
            }

            if (newYardsLength != YardsLength)
            {
                YardsLength = newYardsLength;
                this.FlagForUpdate(user, agent);
            }

            if (newUomUnit != UOMUnit)
            {
                UOMUnit = newUomUnit;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetBalance(decimal newBalance, string user, string agent)
        {
            if (newBalance != Balance)
            {
                Balance = newBalance;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetStatus(string newStatus, string user, string agent)
        {
            if (newStatus != Status)
            {
                Status = newStatus;
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

        public void SetSourceArea(string newSourceArea, string user, string agent)
        {
            if (newSourceArea != SourceArea)
            {
                SourceArea = newSourceArea;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetIsChecked(bool newIsChecked, string user, string agent)
        {
            if (newIsChecked != IsChecked)
            {
                IsChecked = newIsChecked;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetRemark(string newRemark, string user, string agent)
        {
            if (newRemark != Remark)
            {
                Remark = newRemark;
                this.FlagForUpdate(user, agent);
            }
        }
    }
}
