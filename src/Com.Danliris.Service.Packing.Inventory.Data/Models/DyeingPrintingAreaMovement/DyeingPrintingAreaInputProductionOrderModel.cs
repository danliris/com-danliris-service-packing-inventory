using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaInputProductionOrderModel : StandardEntity
    {
        public long ProductionOrderId { get; private set; }
        public string ProductionOrderNo { get; private set; }
        public string CartNo { get; private set; }
        public int BuyerId { get; private set; }
        public string Buyer { get; private set; }
        public string Construction { get; private set; }
        public string Unit { get; private set; }
        public string Color { get; private set; }
        public string Motif { get; private set; }
        public string UomUnit { get; private set; }
        public double Balance { get; private set; }
        public bool HasOutputDocument { get; private set; }
        public bool IsChecked { get; private set; }
        public string PackingInstruction { get; private set; }
        public string ProductionOrderType { get; private set; }
        public double ProductionOrderOrderQuantity { get; private set; }

        public string Remark { get; private set; }
        public string Grade { get; private set; }
        public string Status { get; private set; }
        public double InitLength { get; private set; }
        public double AvalALength { get; private set; }
        public double AvalBLength { get; private set; }
        public double AvalConnectionLength { get; private set; }

        public string AvalType { get; private set; }
        public string AvalCartNo { get; private set; }
        public double AvalQuantityKg { get; private set; }

        public long DeliveryOrderSalesId { get; private set; }
        public string DeliveryOrderSalesNo { get; private set; }

        public string PackagingUnit { get; set; }
        public string PackagingType { get; set; }
        public decimal PackagingQty { get; set; }

        public string Area { get; private set; }

        public double BalanceRemains { get; private set; }

        public int DyeingPrintingAreaInputId { get; set; }
        public int DyeingPrintingAreaOutputProductionOrderId { get; set; }
        public DyeingPrintingAreaInputModel DyeingPrintingAreaInput { get; set; }

        public DyeingPrintingAreaInputProductionOrderModel()
        {

        }

        //IM
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderQuantity, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, double balanceRemains, bool hasOutputDocument, int buyerId)
        {
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            ProductionOrderOrderQuantity = productionOrderQuantity;
            CartNo = cartNo;
            Buyer = buyer;
            Construction = construction;
            Unit = unit;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Balance = balance;
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;

            BalanceRemains = balanceRemains;

            Area = area;

            BuyerId = buyerId;
        }

        //FQC
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument, int buyerId)
        {
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
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;

            Area = area;

            BuyerId = buyerId;
        }
        /// <summary>
        /// Constructor using by Packaging Area 
        /// </summary>
        /// <param name="area"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="balance"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="productionOrderQty"></param>
        /// <param name="buyerId"></param>
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument, double productionOrderQty, string grade, int buyerId)
        {
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
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQty;
            Grade = grade;

            Area = area;

            BuyerId = buyerId;
        }
        /// <summary>
        /// Constructor using by Packaging Area With Balance Remains
        /// </summary>
        /// <param name="area"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="balance"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="productionOrderQty"></param>
        /// <param name="grade"></param>
        /// <param name="balanceRemains"></param>
        /// <param name="buyerId"></param>
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument, double productionOrderQty, string grade, double balanceRemains, int buyerId)
        {
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
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQty;
            Grade = grade;

            Area = area;
            BalanceRemains = balanceRemains;
            BuyerId = buyerId;
        }
        /// <summary>
        /// Constructor using by Packaging Area With Balance Remains and Output Prev SppID
        /// </summary>
        /// <param name="area"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="balance"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="productionOrderQty"></param>
        /// <param name="grade"></param>
        /// <param name="balanceRemains"></param>
        /// <param name="buyerId"></param>
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument, double productionOrderQty, string grade, double balanceRemains, int buyerId, int prevSppOutputID)
        {
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
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQty;
            Grade = grade;

            Area = area;
            BalanceRemains = balanceRemains;
            BuyerId = buyerId;
            DyeingPrintingAreaOutputProductionOrderId = prevSppOutputID;
        }
        /// <summary>
        /// Constructor using by Packaging Area with prev Bon Einty
        /// </summary>
        /// <param name="area"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="balance"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="productionOrderQty"></param>
        /// <param name="buyerId"></param>
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument, double productionOrderQty, string grade, int dyeingPrintingAreaInputId, int buyerId)
        {
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
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQty;
            Grade = grade;

            Area = area;
            DyeingPrintingAreaInputId = dyeingPrintingAreaInputId;
            BuyerId = buyerId;
        }

        /// <summary>
        /// Constructor Using By Packaging Area WIth BalanceRemain and Bon ID
        /// </summary>
        /// <param name="area"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="balance"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="productionOrderQty"></param>
        /// <param name="grade"></param>
        /// <param name="dyeingPrintingAreaInputId"></param>
        /// <param name="balanceRemains"></param>
        /// <param name="buyerId"></param>
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument, double productionOrderQty, string grade, int dyeingPrintingAreaInputId, double balanceRemains, int buyerId)
        {
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
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQty;
            Grade = grade;

            Area = area;
            DyeingPrintingAreaInputId = dyeingPrintingAreaInputId;
            BalanceRemains = balanceRemains;
            BuyerId = buyerId;
        }

        /// <summary>
        /// Constructor Using By Packaging Area WIth BalanceRemain and Bon ID and OutputSPP ID
        /// </summary>
        /// <param name="area"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="balance"></param>
        /// <param name="hasOutputDocument"></param>
        /// <param name="productionOrderQty"></param>
        /// <param name="grade"></param>
        /// <param name="dyeingPrintingAreaInputId"></param>
        /// <param name="balanceRemains"></param>
        /// <param name="buyerId"></param>
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument, double productionOrderQty, string grade, int dyeingPrintingAreaInputId, double balanceRemains, int buyerId,int dyeingPrintingAreaOutputProductionOrderId)
        {
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
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQty;
            Grade = grade;

            Area = area;
            DyeingPrintingAreaInputId = dyeingPrintingAreaInputId;
            BalanceRemains = balanceRemains;
            BuyerId = buyerId;
            DyeingPrintingAreaOutputProductionOrderId = dyeingPrintingAreaOutputProductionOrderId;
        }

        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument, string packagingUnit, string packagingType, decimal packagingQty, int buyerId)
        {
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
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            PackagingUnit = packagingUnit;
            PackagingQty = packagingQty;
            PackagingType = packagingType;

            Area = area;

            BuyerId = buyerId;
        }
        /// <summary>
        /// construtor for Gudang barang jadi
        /// </summary>
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument, string packagingUnit, string packagingType, decimal packagingQty, string grade, double productionOrderOrderQuantity, int buyerId)
        {
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
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            PackagingUnit = packagingUnit;
            PackagingQty = packagingQty;
            PackagingType = packagingType;
            Grade = grade;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;

            Area = area;

            BuyerId = buyerId;
        }

        /// <summary>
        /// construtor for Gudang barang jadi With OutputSPP ID
        /// </summary>
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument, string packagingUnit, string packagingType, decimal packagingQty, string grade, double productionOrderOrderQuantity, int buyerId, int dyeingPrintingAreaOutputProductionOrderId)
        {
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
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            PackagingUnit = packagingUnit;
            PackagingQty = packagingQty;
            PackagingType = packagingType;
            Grade = grade;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;

            Area = area;

            BuyerId = buyerId;
            DyeingPrintingAreaOutputProductionOrderId = dyeingPrintingAreaOutputProductionOrderId;
        }

        //Transit
        public DyeingPrintingAreaInputProductionOrderModel(string area, long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderQuantity, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, double balance, bool hasOutputDocument, string remark, string grade, string status, double balanceRemains, int buyerId)
        {
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
            HasOutputDocument = hasOutputDocument;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQuantity;
            Remark = remark;
            Grade = grade;
            Status = status;

            Area = area;

            BalanceRemains = balanceRemains;

            BuyerId = buyerId;
        }

        public DyeingPrintingAreaInputProductionOrderModel(string area,
                                                           string avalType,
                                                           string avalCartNo,
                                                           string uomUnit,
                                                           double quantity,
                                                           double avalQuantityKg,
                                                           bool hasOutputDocument)
        {
            AvalType = avalType;
            AvalCartNo = avalCartNo;
            UomUnit = uomUnit;
            Balance = quantity;
            AvalQuantityKg = avalQuantityKg;
            HasOutputDocument = hasOutputDocument;

            Area = area;
        }

        //Shipping
        public DyeingPrintingAreaInputProductionOrderModel(string area, long deliveryOrderSalesId, string deliveryOrderSalesNo, long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderQuantity, string buyer, string construction,
           string packingType, string color, string motif, string grade, decimal qtyPacking, string packingUnit, double qty, string uomUnit, bool hasOutputDocument, double balanceRemains, string unit, int buyerId)
        {
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            Buyer = buyer;
            Construction = construction;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            HasOutputDocument = hasOutputDocument;
            ProductionOrderType = productionOrderType;
            Unit = unit;
            DeliveryOrderSalesId = deliveryOrderSalesId;
            DeliveryOrderSalesNo = deliveryOrderSalesNo;
            Grade = grade;

            ProductionOrderOrderQuantity = productionOrderQuantity;
            PackagingType = packingType;
            PackagingQty = qtyPacking;
            PackagingUnit = packingUnit;
            Balance = qty;
            Area = area;

            BalanceRemains = balanceRemains;

            BuyerId = buyerId;
        }

        public DyeingPrintingAreaInputProductionOrderModel(long productionOrderId,
                                                           string productionOrderNo,
                                                           string cartNo,
                                                           string buyer,
                                                           string construction,
                                                           string unit,
                                                           string color,
                                                           string motif,
                                                           string uomUnit,
                                                           double balance,
                                                           bool hasOutputDocument,
                                                           //bool isChecked, 
                                                           string packingInstruction,
                                                           string productionOrderType,
                                                           double productionOrderOrderQuantity,
                                                           string remark,
                                                           string grade,
                                                           string status,
                                                           //double initLength, 
                                                           double avalALength,
                                                           double avalBLength,
                                                           double avalConnectionLength,
                                                           string avalType,
                                                           string avalCartNo,
                                                           double avalQuantityKg,
                                                           long deliveryOrderSalesId,
                                                           string deliveryOrderSalesNo,
                                                           string packagingUnit,
                                                           string packagingType,
                                                           decimal packagingQty,
                                                           string area,
                                                           double balanceRemains,
                                                           int dyeingPrintingAreaInputId,
                                                           int buyerId)
        {
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
            HasOutputDocument = hasOutputDocument;
            //IsChecked = isChecked;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            Remark = remark;
            Grade = grade;
            Status = status;
            //InitLength = initLength;
            AvalALength = avalALength;
            AvalBLength = avalBLength;
            AvalConnectionLength = avalConnectionLength;
            AvalType = avalType;
            AvalCartNo = avalCartNo;
            AvalQuantityKg = avalQuantityKg;
            DeliveryOrderSalesId = deliveryOrderSalesId;
            DeliveryOrderSalesNo = deliveryOrderSalesNo;
            PackagingUnit = packagingUnit;
            PackagingType = packagingType;
            PackagingQty = packagingQty;
            Area = area;
            BalanceRemains = balanceRemains;
            DyeingPrintingAreaInputId = dyeingPrintingAreaInputId;

            BuyerId = buyerId;
        }

        public void SetProductionOrder(long newProductionOrderId, string newProductionOrderNo, string newProductionOrderType, double newProductionOrderOrderQuantity, string user, string agent)
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

            if (newProductionOrderType != ProductionOrderType)
            {
                ProductionOrderType = newProductionOrderType;
                this.FlagForUpdate(user, agent);
            }

            if (newProductionOrderOrderQuantity != ProductionOrderOrderQuantity)
            {
                ProductionOrderOrderQuantity = newProductionOrderOrderQuantity;
                this.FlagForUpdate(user, agent);
            }

        }

        public void SetBuyer(int newBuyerId, string newBuyer, string user, string agent)
        {
            if (newBuyerId != BuyerId)
            {
                BuyerId = newBuyerId;
                this.FlagForUpdate(user, agent);
            }

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

        public void SetHasOutputDocument(bool newFlagHasOutputDocument, string user, string agent)
        {
            if (newFlagHasOutputDocument != HasOutputDocument)
            {
                HasOutputDocument = newFlagHasOutputDocument;
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

        public void SetGrade(string newGrade, string user, string agent)
        {
            if (newGrade != Grade)
            {
                Grade = newGrade;
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

        public void SetIsChecked(bool newFlagChecked, string user, string agent)
        {
            if (newFlagChecked != IsChecked)
            {
                IsChecked = newFlagChecked;
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

        public void SetDeliveryOrderSales(long deliveryOrderSalesId, string deliveryOrderSalesNo, string user, string agent)
        {
            if (deliveryOrderSalesId != DeliveryOrderSalesId)
            {
                DeliveryOrderSalesId = deliveryOrderSalesId;
                this.FlagForUpdate(user, agent);
            }

            if (deliveryOrderSalesNo != DeliveryOrderSalesNo)
            {
                DeliveryOrderSalesNo = deliveryOrderSalesNo;
                this.FlagForUpdate(user, agent);
            }
        }
        public void SetPackagingType(string newPackagingType, string user, string agent)
        {
            if (newPackagingType != PackagingType)
            {
                PackagingType = newPackagingType;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPackagingQty(decimal newPackagingQty, string user, string agent)
        {
            if (newPackagingQty != PackagingQty)
            {
                PackagingQty = newPackagingQty;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPackagingUnit(string newPackagingUnit, string user, string agent)
        {
            if (newPackagingUnit != PackagingUnit)
            {
                PackagingUnit = newPackagingUnit;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetAvalALength(double newAvalABalance, string user, string agent)
        {
            if (newAvalABalance != AvalALength)
            {
                AvalALength = newAvalABalance;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetAvalBLength(double newAvalBBalance, string user, string agent)
        {
            if (newAvalBBalance != AvalBLength)
            {
                AvalBLength = newAvalBBalance;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetAvalConnectionLength(double newAvalConnectionLength, string user, string agent)
        {
            if (newAvalConnectionLength != AvalConnectionLength)
            {
                AvalConnectionLength = newAvalConnectionLength;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetInitLength(double newInitLength, string user, string agent)
        {
            if (newInitLength != InitLength)
            {
                InitLength = newInitLength;
                this.FlagForUpdate(user, agent);
            }
        }
        public void SetBalanceRemains(double newBalanceRemains, string user, string agent)
        {
            if (newBalanceRemains != BalanceRemains)
            {
                BalanceRemains = newBalanceRemains;
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

    }
}
