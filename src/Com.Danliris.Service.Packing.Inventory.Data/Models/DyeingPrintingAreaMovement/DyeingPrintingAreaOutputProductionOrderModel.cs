using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaOutputProductionOrderModel : StandardEntity
    {
        public long ProductionOrderId { get; private set; }
        public string ProductionOrderNo { get; private set; }
        public int MaterialId { get; private set; }
        public string MaterialName { get; private set; }
        public int MaterialConstructionId { get; private set; }
        public string MaterialConstructionName { get; private set; }
        public string MaterialWidth { get; private set; }
        public string CartNo { get; private set; }
        public int BuyerId { get; private set; }
        public string Buyer { get; private set; }
        public string Construction { get; private set; }
        public string Unit { get; private set; }
        public string Color { get; private set; }
        public string Motif { get; private set; }
        public string UomUnit { get; private set; }
        public string Remark { get; private set; }
        public string Grade { get; private set; }
        public string Status { get; private set; }
        public double Balance { get; private set; }
        public string PackingInstruction { get; private set; }
        public string ProductionOrderType { get; private set; }
        public double ProductionOrderOrderQuantity { get; private set; }

        public string PackagingType { get; private set; }
        public decimal PackagingQty { get; private set; }
        public string PackagingUnit { get; private set; }

        public double AvalALength { get; private set; }
        public double AvalBLength { get; private set; }
        public double AvalConnectionLength { get; private set; }

        public long DeliveryOrderSalesId { get; private set; }
        public string DeliveryOrderSalesNo { get; private set; }

        public string AvalType { get; private set; }
        public string AvalCartNo { get; private set; }
        public double AvalQuantityKg { get; private set; }
        public string Machine { get; private set; }

        public bool HasNextAreaDocument { get; private set; }
        public string Area { get; private set; }
        public string DestinationArea { get; private set; }
        public string Description { get; set; }


        public string DeliveryNote { get; private set; }

        public bool HasSalesInvoice { get; private set; }
        public string ShippingGrade { get; private set; }
        public string ShippingRemark { get; private set; }
        public double Weight { get; private set; }


        /// <summary>
        /// ID SPP Input
        /// </summary>
        public int DyeingPrintingAreaInputProductionOrderId { get; set; }

        //public ICollection<DyeingPrintingAreaOutputAvalItemModel> DyeingPrintingAreaOutputAvalItems { get; private set; }


        public int DyeingPrintingAreaOutputId { get; set; }
        public DyeingPrintingAreaOutputModel DyeingPrintingAreaOutput { get; set; }

        public DyeingPrintingAreaOutputProductionOrderModel()
        {
            //DyeingPrintingAreaOutputAvalItems = new HashSet<DyeingPrintingAreaOutputAvalItemModel>();
        }

        //IM
        public DyeingPrintingAreaOutputProductionOrderModel(string area, string destinationArea, bool hasNextAreaDocument, long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderQuantity, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, string remark, string grade, string status, double balance, int dyeingPrintingAreaInputProductionOrderId, int buyerId, string avalType,
            int materialId, string materialName, int materialConstructionId, string materialConstructionName, string materialWidth, string machine) : this()
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
            Remark = remark;
            Status = status;
            Grade = grade;
            ProductionOrderType = productionOrderType;
            PackingInstruction = packingInstruction;

            Area = area;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;

            DyeingPrintingAreaInputProductionOrderId = dyeingPrintingAreaInputProductionOrderId;

            BuyerId = buyerId;

            AvalType = avalType;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;

            Machine = machine;
        }

        //Transit
        public DyeingPrintingAreaOutputProductionOrderModel(string area, string destinationArea, bool hasNextAreaDocument, long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderQuantity, string packingInstruction, string cartNo, string buyer, string construction,
            string unit, string color, string motif, string uomUnit, string remark, string grade, string status, double balance, int dyeingPrintingAreaInputProductonOrderId, int buyerId,
            int materialId, string materialName, int materialConstructionId, string materialConstructionName, string materialWidth) : this()
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
            Remark = remark;
            Status = status;
            Grade = grade;
            ProductionOrderType = productionOrderType;
            PackingInstruction = packingInstruction;

            Area = area;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;

            DyeingPrintingAreaInputProductionOrderId = dyeingPrintingAreaInputProductonOrderId;

            BuyerId = buyerId;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;
        }

        //Shipping
        public DyeingPrintingAreaOutputProductionOrderModel(string area, string destinationArea, bool hasNextAreaDocument, long deliveryOrderSalesId, string deliveryOrderSalesNo, long productionOrderId, string productionOrderNo, string productionOrderType, double productionOrderQuantity, string buyer, string construction,
           string unit, string color, string motif, string grade, string uomUnit, string deliveryNote, double balance, int dyeingPrintingAreaInputProductonOrderId, string packingUnit, string packingType, decimal qtyPacking, int buyerId, bool hasSalesInvoice, string shippingGrade, string shippingRemark, double weight,
           int materialId, string materialName, int materialConstructionId, string materialConstructionName, string materialWidth, string cartNo, string remark) : this()
        {
            ProductionOrderId = productionOrderId;
            ProductionOrderNo = productionOrderNo;
            ProductionOrderOrderQuantity = productionOrderQuantity;
            Buyer = buyer;
            Construction = construction;
            Color = color;
            Motif = motif;
            UomUnit = uomUnit;
            Grade = grade;
            ProductionOrderType = productionOrderType;
            DeliveryOrderSalesId = deliveryOrderSalesId;
            Balance = balance;
            DeliveryOrderSalesNo = deliveryOrderSalesNo;
            DeliveryNote = deliveryNote;
            Unit = unit;
            Area = area;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;
            PackagingQty = qtyPacking;
            PackagingType = packingType;
            PackagingUnit = packingUnit;

            DyeingPrintingAreaInputProductionOrderId = dyeingPrintingAreaInputProductonOrderId;

            BuyerId = buyerId;

            HasSalesInvoice = hasSalesInvoice;

            ShippingGrade = shippingGrade;
            ShippingRemark = shippingRemark;
            Weight = weight;

            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionName = materialConstructionName;
            MaterialConstructionId = materialConstructionId;
            MaterialWidth = materialWidth;

            CartNo = cartNo;
            Remark = remark;
        }

        public DyeingPrintingAreaOutputProductionOrderModel(string area, string destinationArea, bool hasNextAreaDocument, long productionOrderId, string productionOrderNo, string cartNo, string buyer, string construction, string unit,
            string color, string motif, string uomUnit, string remark, string grade, string status, double balance, string packingInstruction, string productionOrderType, double productionOrderQuantity,
            string packagingType, decimal packagingQty, string packagingUnit, int buyerId) : this()
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
            Remark = remark;
            Grade = grade;
            Status = status;
            Balance = balance;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQuantity;
            PackagingType = packagingType;
            PackagingQty = packagingQty;
            PackagingUnit = packagingUnit;

            Area = area;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;

            BuyerId = buyerId;
        }
        /// <summary>
        /// this Constructor for insert data OutputPackagingArea
        /// </summary>
        /// <param name="area"></param>
        /// <param name="destinationArea"></param>
        /// <param name="hasNextAreaDocument"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="remark"></param>
        /// <param name="grade"></param>
        /// <param name="status"></param>
        /// <param name="balance"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="productionOrderQuantity"></param>
        /// <param name="packagingType"></param>
        /// <param name="packagingQty"></param>
        /// <param name="packagingUnit"></param>
        /// <param name="productionOrderOrderQuantity"></param>
        /// <param name="description"></param>
        /// <param name="buyerId"></param>
        public DyeingPrintingAreaOutputProductionOrderModel(string area, string destinationArea, bool hasNextAreaDocument, long productionOrderId, string productionOrderNo, string cartNo, string buyer, string construction, string unit,
            string color, string motif, string uomUnit, string remark, string grade, string status, double balance, string packingInstruction, string productionOrderType, double productionOrderQuantity,
            string packagingType, decimal packagingQty, string packagingUnit, double productionOrderOrderQuantity, string description, int buyerId) : this()
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
            Remark = remark;
            Grade = grade;
            Status = status;
            Balance = balance;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQuantity;
            PackagingType = packagingType;
            PackagingQty = packagingQty;
            PackagingUnit = packagingUnit;

            Area = area;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            Description = description;

            BuyerId = buyerId;
        }
        /// <summary>
        /// this Constructor for insert data OutputPackagingArea if has ID dyeing Printing on it
        /// </summary>
        /// <param name="area"></param>
        /// <param name="destinationArea"></param>
        /// <param name="hasNextAreaDocument"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="remark"></param>
        /// <param name="grade"></param>
        /// <param name="status"></param>
        /// <param name="balance"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="productionOrderQuantity"></param>
        /// <param name="packagingType"></param>
        /// <param name="packagingQty"></param>
        /// <param name="packagingUnit"></param>
        /// <param name="productionOrderOrderQuantity"></param>
        /// <param name="description"></param>
        ///  <param name="buyerId"></param>
        public DyeingPrintingAreaOutputProductionOrderModel(string area, string destinationArea, bool hasNextAreaDocument, long productionOrderId, string productionOrderNo, string cartNo, string buyer, string construction, string unit,
            string color, string motif, string uomUnit, string remark, string grade, string status, double balance, string packingInstruction, string productionOrderType, double productionOrderQuantity,
            string packagingType, decimal packagingQty, string packagingUnit, double productionOrderOrderQuantity, string description, int dyeingPrintintOutputId, int buyerId) : this()
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
            Remark = remark;
            Grade = grade;
            Status = status;
            Balance = balance;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQuantity;
            PackagingType = packagingType;
            PackagingQty = packagingQty;
            PackagingUnit = packagingUnit;

            Area = area;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            Description = description;
            DyeingPrintingAreaOutputId = dyeingPrintintOutputId;

            BuyerId = buyerId;
        }
        /// <summary>
        /// Using For Packaging Area when you want to set with SPP input
        /// </summary>
        /// <param name="area"></param>
        /// <param name="destinationArea"></param>
        /// <param name="hasNextAreaDocument"></param>
        /// <param name="productionOrderId"></param>
        /// <param name="productionOrderNo"></param>
        /// <param name="cartNo"></param>
        /// <param name="buyer"></param>
        /// <param name="construction"></param>
        /// <param name="unit"></param>
        /// <param name="color"></param>
        /// <param name="motif"></param>
        /// <param name="uomUnit"></param>
        /// <param name="remark"></param>
        /// <param name="grade"></param>
        /// <param name="status"></param>
        /// <param name="balance"></param>
        /// <param name="packingInstruction"></param>
        /// <param name="productionOrderType"></param>
        /// <param name="productionOrderQuantity"></param>
        /// <param name="packagingType"></param>
        /// <param name="packagingQty"></param>
        /// <param name="packagingUnit"></param>
        /// <param name="productionOrderOrderQuantity"></param>
        /// <param name="description"></param>
        /// <param name="dyeingPrintintOutputId"></param>
        /// <param name="dyeingPrintingAreaInputProductionOrderId"></param>
        /// <param name="buyerId"></param>
        public DyeingPrintingAreaOutputProductionOrderModel(string area, string destinationArea, bool hasNextAreaDocument, long productionOrderId, string productionOrderNo, string cartNo, string buyer, string construction, string unit,
            string color, string motif, string uomUnit, string remark, string grade, string status, double balance, string packingInstruction, string productionOrderType, double productionOrderQuantity,
            string packagingType, decimal packagingQty, string packagingUnit, double productionOrderOrderQuantity, string description, int dyeingPrintintOutputId, int dyeingPrintingAreaInputProductionOrderId, int buyerId) : this()
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
            Remark = remark;
            Grade = grade;
            Status = status;
            Balance = balance;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderQuantity;
            PackagingType = packagingType;
            PackagingQty = packagingQty;
            PackagingUnit = packagingUnit;

            Area = area;
            DestinationArea = destinationArea;
            HasNextAreaDocument = hasNextAreaDocument;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            Description = description;
            DyeingPrintingAreaOutputId = dyeingPrintintOutputId;
            DyeingPrintingAreaInputProductionOrderId = dyeingPrintingAreaInputProductionOrderId;

            BuyerId = buyerId;

        }

        public DyeingPrintingAreaOutputProductionOrderModel(string avalType,
                                                            string avalCartNo,
                                                            string avalUomUnit,
                                                            double avalQuantity,
                                                            double avalQuantityKg) : this()
        {
            AvalType = avalType;
            AvalCartNo = avalCartNo;
            UomUnit = avalUomUnit;
            Balance = avalQuantity;
            AvalQuantityKg = avalQuantityKg;
        }
        /// <summary>
        /// insert aval using prev total weight
        /// </summary>
        public DyeingPrintingAreaOutputProductionOrderModel(string avalType,
                                                            string avalCartNo,
                                                            string avalUomUnit,
                                                            double avalQuantityOut,
                                                            double avalQuantityKgOut,
                                                            double avalQuantity,
                                                            double avalQuantityTotal) : this()
        {
            AvalType = avalType;
            AvalCartNo = avalCartNo;
            UomUnit = avalUomUnit;
            Balance = avalQuantityOut;
            AvalQuantityKg = avalQuantityKgOut;
            AvalALength = avalQuantity;
            AvalBLength = avalQuantityTotal;
        }
        /// <summary>
        /// Insert Aval with existing bon using prev total weight
        /// </summary>
        public DyeingPrintingAreaOutputProductionOrderModel(string avalType,
                                                            string avalCartNo,
                                                            string avalUomUnit,
                                                            double avalQuantityOut,
                                                            double avalQuantityKgOut,
                                                            double avalQuantity,
                                                            double avalQuantityTotal,
                                                            int dyeingPrintingOutputId) : this()
        {
            AvalType = avalType;
            AvalCartNo = avalCartNo;
            UomUnit = avalUomUnit;
            Balance = avalQuantityOut;
            AvalQuantityKg = avalQuantityKgOut;
            AvalALength = avalQuantity;
            AvalBLength = avalQuantityTotal;
            DyeingPrintingAreaOutputId = dyeingPrintingOutputId;
        }

        /// <summary>
        /// Insert Aval with existing bon
        /// </summary>
        public DyeingPrintingAreaOutputProductionOrderModel(string avalType,
                                                            string avalCartNo,
                                                            string avalUomUnit,
                                                            double avalQuantity,
                                                            double avalQuantityKg,
                                                            int dyeingPrintingOutputId) : this()
        {
            AvalType = avalType;
            AvalCartNo = avalCartNo;
            UomUnit = avalUomUnit;
            Balance = avalQuantity;
            AvalQuantityKg = avalQuantityKg;
            DyeingPrintingAreaOutputId = dyeingPrintingOutputId;
        }
        //Warehouse
        public DyeingPrintingAreaOutputProductionOrderModel(long productionOrderId, string productionOrderNo, string cartNo, string buyer, string construction, string unit, string color, string motif, string uomUnit, string remark, string grade, string status, double balance, string packingInstruction, string productionOrderType, double productionOrderOrderQuantity, string packagingType, decimal packagingQty, string packagingUnit, long deliveryOrderSalesId, string deliveryOrderSalesNo, bool hasNextAreaDocument, string area, string destinationArea, int dyeingPrintingAreaInputProductionOrderId, int buyerId) : this()
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
            Remark = remark;
            Grade = grade;
            Status = status;
            Balance = balance;
            PackingInstruction = packingInstruction;
            ProductionOrderType = productionOrderType;
            ProductionOrderOrderQuantity = productionOrderOrderQuantity;
            PackagingType = packagingType;
            PackagingQty = packagingQty;
            PackagingUnit = packagingUnit;
            DeliveryOrderSalesId = deliveryOrderSalesId;
            DeliveryOrderSalesNo = deliveryOrderSalesNo;
            HasNextAreaDocument = hasNextAreaDocument;
            Area = area;
            DestinationArea = destinationArea;
            DyeingPrintingAreaInputProductionOrderId = dyeingPrintingAreaInputProductionOrderId;

            BuyerId = buyerId;
        }

        //All Properties
        //public DyeingPrintingAreaOutputProductionOrderModel(long productionOrderId, 
        //                                                    string productionOrderNo, 
        //                                                    string cartNo, 
        //                                                    string buyer, 
        //                                                    string construction, 
        //                                                    string unit, 
        //                                                    string color, 
        //                                                    string motif, 
        //                                                    string uomUnit, 
        //                                                    string remark, 
        //                                                    string grade, 
        //                                                    string status, 
        //                                                    double balance, 
        //                                                    string packingInstruction, 
        //                                                    string productionOrderType, 
        //                                                    double productionOrderOrderQuantity, 
        //                                                    string packagingType, 
        //                                                    decimal packagingQty, 
        //                                                    string packagingUnit, 
        //                                                    double avalALength, 
        //                                                    double avalBLength, 
        //                                                    double avalConnectionLength, 
        //                                                    long deliveryOrderSalesId, 
        //                                                    string deliveryOrderSalesNo, 
        //                                                    string avalType, 
        //                                                    string avalCartNo, 
        //                                                    double avalQuantityKg, 
        //                                                    bool hasNextAreaDocument, 
        //                                                    string area, 
        //                                                    string destinationArea, 
        //                                                    string description, 
        //                                                    string deliveryNote, 
        //                                                    int dyeingPrintingAreaInputProductionOrderId, 
        //                                                    int dyeingPrintingAreaOutputId)
        //{
        //    ProductionOrderId = productionOrderId;
        //    ProductionOrderNo = productionOrderNo;
        //    CartNo = cartNo;
        //    Buyer = buyer;
        //    Construction = construction;
        //    Unit = unit;
        //    Color = color;
        //    Motif = motif;
        //    UomUnit = uomUnit;
        //    Remark = remark;
        //    Grade = grade;
        //    Status = status;
        //    Balance = balance;
        //    PackingInstruction = packingInstruction;
        //    ProductionOrderType = productionOrderType;
        //    ProductionOrderOrderQuantity = productionOrderOrderQuantity;
        //    PackagingType = packagingType;
        //    PackagingQty = packagingQty;
        //    PackagingUnit = packagingUnit;
        //    AvalALength = avalALength;
        //    AvalBLength = avalBLength;
        //    AvalConnectionLength = avalConnectionLength;
        //    DeliveryOrderSalesId = deliveryOrderSalesId;
        //    DeliveryOrderSalesNo = deliveryOrderSalesNo;
        //    AvalType = avalType;
        //    AvalCartNo = avalCartNo;
        //    AvalQuantityKg = avalQuantityKg;
        //    HasNextAreaDocument = hasNextAreaDocument;
        //    Area = area;
        //    DestinationArea = destinationArea;
        //    Description = description;
        //    DeliveryNote = deliveryNote;
        //    DyeingPrintingAreaInputProductionOrderId = dyeingPrintingAreaInputProductionOrderId;
        //    DyeingPrintingAreaOutputId = dyeingPrintingAreaOutputId;
        //}

        public void SetProductionOrder(long newProductionOrderId, string newProductionOrderNo, string newProductionOrderType, double newProductionOrderQuantity, string user, string agent)
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

            if (newProductionOrderQuantity != ProductionOrderOrderQuantity)
            {
                ProductionOrderOrderQuantity = newProductionOrderQuantity;
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

        //public void SetAvalALength(double newAvalABalance, string user, string agent)
        //{
        //    if (newAvalABalance != AvalALength)
        //    {
        //        AvalALength = newAvalABalance;
        //        this.FlagForUpdate(user, agent);
        //    }
        //}

        //public void SetAvalBLength(double newAvalBBalance, string user, string agent)
        //{
        //    if (newAvalBBalance != AvalBLength)
        //    {
        //        AvalBLength = newAvalBBalance;
        //        this.FlagForUpdate(user, agent);
        //    }
        //}

        //public void SetAvalConnectionLength(double newAvalConnectionLength, string user, string agent)
        //{
        //    if (newAvalConnectionLength != AvalConnectionLength)
        //    {
        //        AvalConnectionLength = newAvalConnectionLength;
        //        this.FlagForUpdate(user, agent);
        //    }
        //}

        public void SetArea(string newArea, string user, string agent)
        {
            if (newArea != Area)
            {
                Area = newArea;
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
        public void SetDescription(string newDescription, string user, string agent)
        {
            if (newDescription != Description)
            {
                Description = newDescription;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetHasSalesInvoice(bool newFlagHasSalesInvoice, string user, string agent)
        {
            if (newFlagHasSalesInvoice != HasSalesInvoice)
            {
                HasSalesInvoice = newFlagHasSalesInvoice;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetShippingGrade(string newShippingGrade, string user, string agent)
        {
            if (newShippingGrade != ShippingGrade)
            {
                ShippingGrade = newShippingGrade;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetShippingRemark(string newShippingRemark, string user, string agent)
        {
            if (newShippingRemark != ShippingRemark)
            {
                ShippingRemark = newShippingRemark;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetWeight(double newWeight, string user, string agent)
        {
            if (newWeight != Weight)
            {
                Weight = newWeight;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetDeliveryNote(string newDeliveryNote, string user, string agent)
        {
            if (newDeliveryNote != DeliveryNote)
            {
                DeliveryNote = newDeliveryNote;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetAvalType(string newAvalType, string user, string agent)
        {
            if (newAvalType != AvalType)
            {
                AvalType = newAvalType;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetMaterial(int newId, string newName, string user, string agent)
        {
            if (newId != MaterialId)
            {
                MaterialId = newId;
                this.FlagForUpdate(user, agent);
            }

            if (newName != MaterialName)
            {
                MaterialName = newName;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetMaterialConstruction(int newId, string newName, string user, string agent)
        {
            if (newId != MaterialConstructionId)
            {
                MaterialConstructionId = newId;
                this.FlagForUpdate(user, agent);
            }

            if (newName != MaterialConstructionName)
            {
                MaterialConstructionName = newName;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetMaterialWidth(string newMaterialWidth, string user, string agent)
        {

            if (newMaterialWidth != MaterialWidth)
            {
                MaterialWidth = newMaterialWidth;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetMachine(string newMachine, string user, string agent)
        {

            if (newMachine != Machine)
            {
                Machine = newMachine;
                this.FlagForUpdate(user, agent);
            }
        }
    }
}
