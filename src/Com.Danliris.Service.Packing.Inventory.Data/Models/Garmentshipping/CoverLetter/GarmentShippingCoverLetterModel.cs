using Com.Moonlay.Models;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter
{
    public class GarmentShippingCoverLetterModel : StandardEntity
    {
        public int PackingListId { get; private set; }
        public int InvoiceId { get; private set; }
        public string InvoiceNo { get; private set; }

        public DateTimeOffset Date { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string ATTN { get; private set; }
        public string Phone { get; private set; }
        public DateTimeOffset BookingDate { get; private set; }

        public int OrderId { get; private set; }
        public string OrderCode { get; private set; }
        public string OrderName { get; private set; }

        public double PCSQuantity { get; private set; }
        public double SETSQuantity { get; private set; }
        public double PACKQuantity { get; private set; }
        public double CartoonQuantity { get; private set; }

        public int ForwarderId { get; private set; }
        public string ForwarderCode { get; private set; }
        public string ForwarderName { get; private set; }

        public string Truck { get; private set; }
        public string PlateNumber { get; private set; }
        public string Driver { get; private set; }
        public string ContainerNo { get; private set; }
        public string Freight { get; private set; }
        public string ShippingSeal { get; private set; }
        public string DLSeal { get; private set; }
        public string EMKLSeal { get; private set; }
        public DateTimeOffset ExportEstimationDate { get; private set; }
        public string Unit { get; private set; }
        public int ShippingStaffId { get; private set; }
        public string ShippingStaffName { get; private set; }

        public GarmentShippingCoverLetterModel(int packingListId, int invoiceId, string invoiceNo, DateTimeOffset date, string name, string address, string aTTN, string phone, DateTimeOffset bookingDate, int orderId, string orderCode, string orderName, double pCSQuantity, double sETSQuantity, double pACKQuantity, double cartoonQuantity, int forwarderId, string forwarderCode, string forwarderName, string truck, string plateNumber, string driver, string containerNo, string freight, string shippingSeal, string dLSeal, string eMKLSeal, DateTimeOffset exportEstimationDate, string unit, int shippingStaffId, string shippingStaffName)
        {
            PackingListId = packingListId;
            InvoiceId = invoiceId;
            InvoiceNo = invoiceNo;
            Date = date;
            Name = name;
            Address = address;
            ATTN = aTTN;
            Phone = phone;
            BookingDate = bookingDate;
            OrderId = orderId;
            OrderCode = orderCode;
            OrderName = orderName;
            PCSQuantity = pCSQuantity;
            SETSQuantity = sETSQuantity;
            PACKQuantity = pACKQuantity;
            CartoonQuantity = cartoonQuantity;
            ForwarderId = forwarderId;
            ForwarderCode = forwarderCode;
            ForwarderName = forwarderName;
            Truck = truck;
            PlateNumber = plateNumber;
            Driver = driver;
            ContainerNo = containerNo;
            Freight = freight;
            ShippingSeal = shippingSeal;
            DLSeal = dLSeal;
            EMKLSeal = eMKLSeal;
            ExportEstimationDate = exportEstimationDate;
            Unit = unit;
            ShippingStaffId = shippingStaffId;
            ShippingStaffName = shippingStaffName;
        }

        public void SetDate(DateTimeOffset date, string userName, string userAgent)
        {
            if (Date != date)
            {
                Date = date;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetName(string name, string userName, string userAgent)
        {
            if (Name != name)
            {
                Name = name;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetAddress(string address, string userName, string userAgent)
        {
            if (Address != address)
            {
                Address = address;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetATTN(string aTTN, string userName, string userAgent)
        {
            if (ATTN != aTTN)
            {
                ATTN = aTTN;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPhone(string phone, string userName, string userAgent)
        {
            if (Phone != phone)
            {
                Phone = phone;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBookingDate(DateTimeOffset bookingDate, string userName, string userAgent)
        {
            if (BookingDate != bookingDate)
            {
                BookingDate = bookingDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetOrderId(int orderId, string userName, string userAgent)
        {
            if (OrderId != orderId)
            {
                OrderId = orderId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetOrderCode(string orderCode, string userName, string userAgent)
        {
            if (OrderCode != orderCode)
            {
                OrderCode = orderCode;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetOrderName(string orderName, string userName, string userAgent)
        {
            if (OrderName != orderName)
            {
                OrderName = orderName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPCSQuantity(double pCSQuantity, string userName, string userAgent)
        {
            if (PCSQuantity != pCSQuantity)
            {
                PCSQuantity = pCSQuantity;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetSETSQuantity(double sETSQuantity, string userName, string userAgent)
        {
            if (SETSQuantity != sETSQuantity)
            {
                SETSQuantity = sETSQuantity;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPACKQuantity(double pACKQuantity, string userName, string userAgent)
        {
            if (PACKQuantity != pACKQuantity)
            {
                PACKQuantity = pACKQuantity;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCartoonQuantity(double cartoonQuantity, string userName, string userAgent)
        {
            if (CartoonQuantity != cartoonQuantity)
            {
                CartoonQuantity = cartoonQuantity;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetForwarderId(int forwarderId, string userName, string userAgent)
        {
            if (ForwarderId != forwarderId)
            {
                ForwarderId = forwarderId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetForwarderCode(string forwarderCode, string userName, string userAgent)
        {
            if (ForwarderCode != forwarderCode)
            {
                ForwarderCode = forwarderCode;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetForwarderName(string forwarderName, string userName, string userAgent)
        {
            if (ForwarderName != forwarderName)
            {
                ForwarderName = forwarderName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetTruck(string truck, string userName, string userAgent)
        {
            if (Truck != truck)
            {
                Truck = truck;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPlateNumber(string plateNumber, string userName, string userAgent)
        {
            if (PlateNumber != plateNumber)
            {
                PlateNumber = plateNumber;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetDriver(string driver, string userName, string userAgent)
        {
            if (Driver != driver)
            {
                Driver = driver;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetContainerNo(string containerNo, string userName, string userAgent)
        {
            if (ContainerNo != containerNo)
            {
                ContainerNo = containerNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetFreight(string freight, string userName, string userAgent)
        {
            if (Freight != freight)
            {
                Freight = freight;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetShippingSeal(string shippingSeal, string userName, string userAgent)
        {
            if (ShippingSeal != shippingSeal)
            {
                ShippingSeal = shippingSeal;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetDLSeal(string dLSeal, string userName, string userAgent)
        {
            if (DLSeal != dLSeal)
            {
                DLSeal = dLSeal;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetEMKLSeal(string eMKLSeal, string userName, string userAgent)
        {
            if (EMKLSeal != eMKLSeal)
            {
                EMKLSeal = eMKLSeal;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetExportEstimationDate(DateTimeOffset exportEstimationDate, string userName, string userAgent)
        {
            if (ExportEstimationDate != exportEstimationDate)
            {
                ExportEstimationDate = exportEstimationDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUnit(string unit, string userName, string userAgent)
        {
            if (Unit != unit)
            {
                Unit = unit;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetShippingStaffId(int shippingStaffId, string userName, string userAgent)
        {
            if (ShippingStaffId != shippingStaffId)
            {
                ShippingStaffId = shippingStaffId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetShippingStaffName(string shippingStaffName, string userName, string userAgent)
        {
            if (ShippingStaffName != shippingStaffName)
            {
                ShippingStaffName = shippingStaffName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

    }
}
