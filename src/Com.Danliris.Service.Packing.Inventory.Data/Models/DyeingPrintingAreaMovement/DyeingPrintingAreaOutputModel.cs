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

        public string Type { get; private set; }


        public ICollection<DyeingPrintingAreaOutputProductionOrderModel> DyeingPrintingAreaOutputProductionOrders { get; set; }

        public DyeingPrintingAreaOutputModel()
        {
            DyeingPrintingAreaOutputProductionOrders = new HashSet<DyeingPrintingAreaOutputProductionOrderModel>();
        }

        public DyeingPrintingAreaOutputModel(DateTimeOffset date, string area, string shift, string bonNo, bool hasNextAreaDocument, 
            string destinationArea,string group, ICollection<DyeingPrintingAreaOutputProductionOrderModel> dyeingPrintingAreaOutputProductionOrders)
        {
            Date = date;
            Area = area;
            Shift = shift;
            BonNo = bonNo;
            Group = group;
            HasNextAreaDocument = hasNextAreaDocument;
            DestinationArea = destinationArea;
            DyeingPrintingAreaOutputProductionOrders = dyeingPrintingAreaOutputProductionOrders;
        }

        /// <summary>
        /// Adjustment Data
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
            : this(date, area, shift, bonNo, hasNextAreaDocument, destinationArea, group, dyeingPrintingAreaOutputProductionOrders)
        {
            Type = type;
        }

        public DyeingPrintingAreaOutputModel(DateTimeOffset date, string area, string shift, string bonNo, string donNo, int doId, bool hasNextAreaDocument,
            string destinationArea, string group, ICollection<DyeingPrintingAreaOutputProductionOrderModel> dyeingPrintingAreaOutputProductionOrders)
        {
            Date = date;
            Area = area;
            Shift = shift;
            BonNo = bonNo;
            Group = group;
            HasNextAreaDocument = hasNextAreaDocument;
            DestinationArea = destinationArea;
            DeliveryOrderSalesId = doId;
            DeliveryOrderSalesNo = donNo;
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
        public DyeingPrintingAreaOutputModel(DateTimeOffset date, string area, string shift, string bonNo, bool hasNextAreaDocument,
            string destinationArea, string group, long deliveryOrderId, string deliveryOrderNo, bool hasSalesInvoice, ICollection<DyeingPrintingAreaOutputProductionOrderModel> dyeingPrintingAreaOutputProductionOrders)
        {
            Date = date;
            Area = area;
            Shift = shift;
            BonNo = bonNo;
            Group = group;
            HasNextAreaDocument = hasNextAreaDocument;
            DestinationArea = destinationArea;
            DyeingPrintingAreaOutputProductionOrders = dyeingPrintingAreaOutputProductionOrders;

            DeliveryOrderSalesId = deliveryOrderId;
            DeliveryOrderSalesNo = deliveryOrderNo;

            HasSalesInvoice = hasSalesInvoice;
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

        public void SetGroup(string newGroup, string user, string agent)
        {
            if(newGroup != Group)
            {
                Group = newGroup;
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
    }
}
