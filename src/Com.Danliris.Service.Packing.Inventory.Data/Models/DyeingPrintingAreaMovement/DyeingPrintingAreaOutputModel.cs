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
        public string Group { get; private set; }

        public bool HasSalesInvoice { get; private set; }

        public long DeliveryOrderSalesId { get; private set; }
        public string DeliveryOrderSalesNo { get; private set; }

        public long DeliveryOrderAvalId { get; private set; }
        public string DeliveryOrderAvalNo { get; private set; }

        public string Type { get; private set; }

        public string ShippingCode { get; private set; }

        public string AdjItemCategory { get; private set; }

        public string PackingListNo { get; private set; }
        public string PackingType { get; private set; }
        public string PackingListRemark { get; private set; }
        public string PackingListAuthorized { get; private set; }
        public string PackingListLCNumber { get; private set; }
        public string PackingListIssuedBy { get; private set; }
        public string PackingListDescription { get; private set; }
        public bool UpdateBySales { get; private set; }
        public string UomUnit { get; private set; }
        


        public ICollection<DyeingPrintingAreaOutputProductionOrderModel> DyeingPrintingAreaOutputProductionOrders { get; set; }

        public DyeingPrintingAreaOutputModel()
        {
            DyeingPrintingAreaOutputProductionOrders = new HashSet<DyeingPrintingAreaOutputProductionOrderModel>();
        }

        /// <summary>
        /// New Constructor Output
        /// </summary>
        /// <param name="date"></param>
        /// <param name="area"></param>
        /// <param name="shift"></param>
        /// <param name="bonNo"></param>
        /// <param name="hasNextAreaDocument"></param>
        /// <param name="destinationArea"></param>
        /// <param name="group"></param>
        /// <param name="type"></param>
        /// <param name="dyeingPrintingAreaOutputProductionOrders"></param>
        public DyeingPrintingAreaOutputModel(DateTimeOffset date, string area, string shift, string bonNo, bool hasNextAreaDocument,
            string destinationArea, string group, string type, ICollection<DyeingPrintingAreaOutputProductionOrderModel> dyeingPrintingAreaOutputProductionOrders)
        {
            Date = date;
            Area = area;
            Shift = shift;
            BonNo = bonNo;
            Group = group;
            HasNextAreaDocument = hasNextAreaDocument;
            DestinationArea = destinationArea;
            DyeingPrintingAreaOutputProductionOrders = dyeingPrintingAreaOutputProductionOrders;
            Type = type;
        }

        /// <summary>
        /// Area Transit
        /// </summary>
        /// <param name="date"></param>
        /// <param name="area"></param>
        /// <param name="shift"></param>
        /// <param name="bonNo"></param>
        /// <param name="hasNextAreaDocument"></param>
        /// <param name="destinationArea"></param>
        /// <param name="group"></param>
        /// <param name="type"></param>
        /// <param name="adjItemCategory"></param>
        /// <param name="dyeingPrintingAreaOutputProductionOrders"></param>
        public DyeingPrintingAreaOutputModel(DateTimeOffset date, string area, string shift, string bonNo, bool hasNextAreaDocument,
            string destinationArea, string group, string type, string adjItemCategory, ICollection<DyeingPrintingAreaOutputProductionOrderModel> dyeingPrintingAreaOutputProductionOrders)
        {
            Date = date;
            Area = area;
            Shift = shift;
            BonNo = bonNo;
            Group = group;
            HasNextAreaDocument = hasNextAreaDocument;
            DestinationArea = destinationArea;
            DyeingPrintingAreaOutputProductionOrders = dyeingPrintingAreaOutputProductionOrders;
            Type = type;
            AdjItemCategory = adjItemCategory;
        }

        /// <summary>
        /// Area Aval
        /// </summary>
        /// <param name="date"></param>
        /// <param name="area"></param>
        /// <param name="shift"></param>
        /// <param name="bonNo"></param>
        /// <param name="donNo"></param>
        /// <param name="doId"></param>
        /// <param name="hasNextAreaDocument"></param>
        /// <param name="destinationArea"></param>
        /// <param name="group"></param>
        /// <param name="type"></param>
        /// <param name="dyeingPrintingAreaOutputProductionOrders"></param>
        public DyeingPrintingAreaOutputModel(DateTimeOffset date, string area, string shift, string bonNo, string donNo, long doId, bool hasNextAreaDocument,
            string destinationArea, string group, string type, ICollection<DyeingPrintingAreaOutputProductionOrderModel> dyeingPrintingAreaOutputProductionOrders)
        {
            Date = date;
            Area = area;
            Shift = shift;
            BonNo = bonNo;
            Group = group;
            HasNextAreaDocument = hasNextAreaDocument;
            DestinationArea = destinationArea;
            DeliveryOrderAvalId = doId;
            DeliveryOrderAvalNo = donNo;
            Type = type;
            DyeingPrintingAreaOutputProductionOrders = dyeingPrintingAreaOutputProductionOrders;
        }

        /// <summary>
        /// Area Shipping
        /// </summary>
        /// <param name="date"></param>
        /// <param name="area"></param>
        /// <param name="shift"></param>
        /// <param name="bonNo"></param>
        /// <param name="hasNextAreaDocument"></param>
        /// <param name="destinationArea"></param>
        /// <param name="group"></param>
        /// <param name="deliveryOrderId"></param>
        /// <param name="deliveryOrderNo"></param>
        /// <param name="hasSalesInvoice"></param>
        /// <param name="dyeingPrintingAreaOutputProductionOrders"></param>
        /// <param name="type"></param>
        /// <param name="shippingCode"></param>
        /// <param name="packingListNo"></param>
        /// <param name="packingType"></param>
        /// <param name="packingListRemark"></param>
        /// <param name="packingListAuthorized"></param>
        /// <param name="packingListLCNumber"></param>
        /// <param name="packingListIssuedBy"></param>
        /// <param name="packingListDescription"></param>
        /// <param name="updateBySales"></param>
        public DyeingPrintingAreaOutputModel(DateTimeOffset date, string area, string shift, string bonNo, bool hasNextAreaDocument,
            string destinationArea, string group, long deliveryOrderId, string deliveryOrderNo, bool hasSalesInvoice, string type, string shippingCode, string packingListNo, string packingType, string packingListRemark,
            string packingListAuthorized, string packingListLCNumber, string packingListIssuedBy, string packingListDescription, bool updateBySales, string uomUnit,
             ICollection<DyeingPrintingAreaOutputProductionOrderModel> dyeingPrintingAreaOutputProductionOrders)
        {
            Date = date;
            Area = area;
            Shift = shift;
            BonNo = bonNo;
            Group = group;
            Type = type;
            HasNextAreaDocument = hasNextAreaDocument;
            DestinationArea = destinationArea;
            DyeingPrintingAreaOutputProductionOrders = dyeingPrintingAreaOutputProductionOrders;

            DeliveryOrderSalesId = deliveryOrderId;
            DeliveryOrderSalesNo = deliveryOrderNo;

            HasSalesInvoice = hasSalesInvoice;
            ShippingCode = shippingCode;
            PackingListNo = packingListNo;
            PackingType = packingType;
            PackingListRemark = packingListRemark;
            PackingListAuthorized = packingListAuthorized;
            PackingListLCNumber = packingListLCNumber;
            PackingListIssuedBy = packingListIssuedBy;
            PackingListDescription = packingListDescription;
            UpdateBySales = updateBySales;
            UomUnit = uomUnit;  
        }

        ///// <summary>
        ///// Area Shipping To Buyer
        ///// </summary>
        ///// <param name="date"></param>
        ///// <param name="area"></param>
        ///// <param name="shift"></param>
        ///// <param name="bonNo"></param>
        ///// <param name="hasNextAreaDocument"></param>
        ///// <param name="destinationArea"></param>
        ///// <param name="group"></param>
        ///// <param name="deliveryOrderId"></param>
        ///// <param name="deliveryOrderNo"></param>
        ///// <param name="hasSalesInvoice"></param>
        ///// <param name="dyeingPrintingAreaOutputProductionOrders"></param>
        ///// <param name="type"></param>
        ///// <param name="shippingCode"></param>
        ///// <param name="packingListNo"></param>
        ///// <param name="packingType"></param>
        ///// <param name="packingListRemark"></param>
        ///// <param name="packingListAuthorized"></param>
        //public DyeingPrintingAreaOutputModel(DateTimeOffset date, string area, string shift, string bonNo, bool hasNextAreaDocument,
        //    string destinationArea, string group, long deliveryOrderId, string deliveryOrderNo, bool hasSalesInvoice, string type, string shippingCode, string packingListNo, string packingType, string packingListRemark, string packingListAuthorized,  ICollection<DyeingPrintingAreaOutputProductionOrderModel> dyeingPrintingAreaOutputProductionOrders)
        //{
        //    Date = date;
        //    Area = area;
        //    Shift = shift;
        //    BonNo = bonNo;
        //    Group = group;
        //    Type = type;
        //    HasNextAreaDocument = hasNextAreaDocument;
        //    DestinationArea = destinationArea;
        //    DyeingPrintingAreaOutputProductionOrders = dyeingPrintingAreaOutputProductionOrders;

        //    DeliveryOrderSalesId = deliveryOrderId;
        //    DeliveryOrderSalesNo = deliveryOrderNo;

        //    HasSalesInvoice = hasSalesInvoice;
        //    ShippingCode = shippingCode;
        //    PackingListNo = packingListNo;
        //    PackingType = packingType;
        //    PackingListRemark = packingListRemark;
        //    PackingListAuthorized = packingListAuthorized;
        //}


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

        public void SetGroup(string newGroup, string user, string agent)
        {
            if (newGroup != Group)
            {
                Group = newGroup;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPackingListRemark(string newPackingListRemark, string user, string agent)
        {
            if (newPackingListRemark != PackingListRemark)
            {
                PackingListRemark = newPackingListRemark;
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

        public void SetHasSalesInvoice(bool newFlagHasSalesInvoice, string user, string agent)
        {
            if (newFlagHasSalesInvoice != HasSalesInvoice)
            {
                HasSalesInvoice = newFlagHasSalesInvoice;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetAdjItemCategory(string newAdjItemCategory, string user, string agent)
        {
            if (newAdjItemCategory != AdjItemCategory)
            {
                AdjItemCategory = newAdjItemCategory;
                this.FlagForUpdate(user, agent);
            }
        }
        public void SetPackingLCNumber(string newPackingListLCNumber, string user, string agent)
        {
            if (newPackingListLCNumber != PackingListLCNumber)
            {
                PackingListLCNumber = newPackingListLCNumber;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPackingIssuedBy(string newPackingListIssuedBy, string user, string agent)
        {
            if (newPackingListIssuedBy != PackingListIssuedBy)
            {
                PackingListIssuedBy = newPackingListIssuedBy;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPackingDescription(string newPackingListDescription, string user, string agent)
        {
            if (newPackingListDescription != PackingListDescription)
            {
                PackingListDescription = newPackingListDescription;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPackingUpdateBySales(bool newUpdateBySales, string user, string agent)
        {
            if (newUpdateBySales != UpdateBySales)
            {
                UpdateBySales = newUpdateBySales;
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

        //public void SetDeliveryOrderAval(long deliveryOrderAvalId, string deliveryOrderAvalNo, string user, string agent)
        //{
        //    if (deliveryOrderAvalId != DeliveryOrderAvalId)
        //    {
        //        DeliveryOrderAvalId = deliveryOrderAvalId;
        //        this.FlagForUpdate(user, agent);
        //    }

        //    if (deliveryOrderAvalNo != DeliveryOrderAvalNo)
        //    {
        //        DeliveryOrderAvalNo = deliveryOrderAvalNo;
        //        this.FlagForUpdate(user, agent);
        //    }
        //}
    }
}
