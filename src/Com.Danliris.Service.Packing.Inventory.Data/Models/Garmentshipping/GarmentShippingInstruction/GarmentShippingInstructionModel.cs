using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionModel : StandardEntity
    {
        public string InvoiceNo { get; private set; }
        public int InvoiceId { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public int ForwarderId { get; private set; }
        public string ForwarderCode { get; private set; }
        public string ForwarderName { get; private set; }
        public string ForwarderAddress { get; private set; }
        public string ForwarderPhone { get; private set; }
        public string ATTN { get; private set; }
        public string Fax { get; private set; }
        public string CC { get; private set; }
        public int ShippingStaffId { get; private set; }
        public string ShippingStaffName { get; private set; }
        public string Phone { get; private set; }

        #region Detail Instruction
        public string ShippedBy { get; private set; }
        public DateTimeOffset TruckingDate { get; private set; }
        public string CartonNo { get; private set; }
        public string PortOfDischarge { get; private set; }
        public string PlaceOfDelivery { get; private set; }
        public string FeederVessel { get; private set; }
        public string OceanVessel { get; private set; }
        public string Carrier { get; private set; }
        public string Flight { get; private set; }
        public string Transit { get; private set; }
        public int BankAccountId { get; private set; }
        public string BankAccountName { get; private set; }
        public int BuyerAgentId { get; private set; }
        public string BuyerAgentCode { get; private set; }
        public string BuyerAgentName { get; private set; }
        public string BuyerAgentAddress { get; private set; }
        public string Notify { get; private set; }
        public string SpecialInstruction { get; private set; }
        public DateTimeOffset LadingDate { get; private set; }
        public string LadingBill { get; private set; }
        public string Freight { get; private set; }
        public string Marks { get; private set; }
        #endregion

        public GarmentShippingInstructionModel()
        {
        }


        public GarmentShippingInstructionModel(string invoiceNo, int invoiceId, DateTimeOffset date, int forwarderId, string forwarderCode, string forwarderName, string forwarderAddress, string forwarderPhone, string aTTN, string fax, string cC, int shippingStaffId, string shippingStaffName, string phone, string shippedBy, DateTimeOffset truckingDate, string cartonNo, string portOfDischarge, string placeOfDelivery, string feederVessel, string oceanVessel, string carrier, string flight, string transit, int bankAccountId, string bankAccountName, int buyerAgentId, string buyerAgentCode, string buyerAgentName, string buyerAgentAddress, string notify, string specialInstruction, DateTimeOffset ladingDate, string ladingBill, string freight, string marks)
        {
            InvoiceNo = invoiceNo;
            InvoiceId = invoiceId;
            Date = date;
            ForwarderId = forwarderId;
            ForwarderCode = forwarderCode;
            ForwarderName = forwarderName;
            ForwarderAddress = forwarderAddress;
            ForwarderPhone = forwarderPhone;
            ATTN = aTTN;
            Fax = fax;
            CC = cC;
            ShippingStaffId = shippingStaffId;
            ShippingStaffName = shippingStaffName;
            Phone = phone;
            ShippedBy = shippedBy;
            TruckingDate = truckingDate;
            CartonNo = cartonNo;
            PortOfDischarge = portOfDischarge;
            PlaceOfDelivery = placeOfDelivery;
            FeederVessel = feederVessel;
            OceanVessel = oceanVessel;
            Carrier = carrier;
            Flight = flight;
            Transit = transit;
            BankAccountId = bankAccountId;
            BankAccountName = bankAccountName;
            BuyerAgentId = buyerAgentId;
            BuyerAgentCode = buyerAgentCode;
            BuyerAgentName = buyerAgentName;
            BuyerAgentAddress = buyerAgentAddress;
            Notify = notify;
            SpecialInstruction = specialInstruction;
            LadingDate = ladingDate;
            LadingBill = ladingBill;
            Freight = freight;
            Marks = marks;
        }

        public void SetInvoiceId(int invoiceId, string userName, string userAgent)
        {
            if (InvoiceId != invoiceId)
            {
                InvoiceId = invoiceId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetDate(DateTimeOffset date, string userName, string userAgent)
        {
            if (Date != date)
            {
                Date = date;
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

        public void SetForwarderName(string forwarderName, string userName, string userAgent)
        {
            if (ForwarderName != forwarderName)
            {
                ForwarderName = forwarderName;
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

        public void SetForwarderAddress(string forwarderAddress, string userName, string userAgent)
        {
            if (ForwarderAddress != forwarderAddress)
            {
                ForwarderAddress = forwarderAddress;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetForwarderPhone(string forwarderPhone, string userName, string userAgent)
        {
            if (ForwarderPhone != forwarderPhone)
            {
                ForwarderPhone = forwarderPhone;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetShippedBy(string shippedBy, string userName, string userAgent)
        {
            if (ShippedBy != shippedBy)
            {
                ShippedBy = shippedBy;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCartonNo(string cartonNo, string userName, string userAgent)
        {
            if (CartonNo != cartonNo)
            {
                CartonNo = cartonNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPortOfDischarge(string portOfDischarge, string userName, string userAgent)
        {
            if (PortOfDischarge != portOfDischarge)
            {
                PortOfDischarge = portOfDischarge;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPlaceOfDelivery(string placeOfDelivery, string userName, string userAgent)
        {
            if (PlaceOfDelivery != placeOfDelivery)
            {
                PlaceOfDelivery = placeOfDelivery;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetFeederVessel(string feederVessel, string userName, string userAgent)
        {
            if (FeederVessel != feederVessel)
            {
                FeederVessel = feederVessel;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetOceanVessel(string oceanVessel, string userName, string userAgent)
        {
            if (OceanVessel != oceanVessel)
            {
                OceanVessel = oceanVessel;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCarrier(string carrier, string userName, string userAgent)
        {
            if (Carrier != carrier)
            {
                Carrier = carrier;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetFlight(string flight, string userName, string userAgent)
        {
            if (Flight != flight)
            {
                Flight = flight;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetTransit(string transit, string userName, string userAgent)
        {
            if (Transit != transit)
            {
                Transit = transit;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetNotify(string notify, string userName, string userAgent)
        {
            if (Notify != notify)
            {
                Notify = notify;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetSpecialInstruction(string specialInstruction, string userName, string userAgent)
        {
            if (SpecialInstruction != specialInstruction)
            {
                SpecialInstruction = specialInstruction;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetFax(string fax, string userName, string userAgent)
        {
            if (Fax != fax)
            {
                Fax = fax;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetATTN(string attn, string userName, string userAgent)
        {
            if (ATTN != attn)
            {
                ATTN = attn;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCC(string cc, string userName, string userAgent)
        {
            if (CC != cc)
            {
                CC = cc;
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

        public void SetLadingDate(DateTimeOffset ladingDate, string userName, string userAgent)
        {
            if (LadingDate != ladingDate)
            {
                LadingDate = ladingDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetLadingBill(string ladingBill, string userName, string userAgent)
        {
            if (LadingBill != ladingBill)
            {
                LadingBill = ladingBill;
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

        public void SetMarks(string marks, string userName, string userAgent)
        {
            if (Marks != marks)
            {
                Marks = marks;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
