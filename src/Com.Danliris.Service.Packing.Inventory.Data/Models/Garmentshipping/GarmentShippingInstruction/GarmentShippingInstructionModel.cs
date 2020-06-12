using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionModel : StandardEntity
    {
        public string InvoiceNo { get; private set; }
        public int PackingListId { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public int EMKLId { get; private set; }
        public string EMKLCode { get; private set; }
        public string EMKLName { get; private set; }
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
        #endregion
        

        public GarmentShippingInstructionModel(string invoiceNo, int packingListId, DateTimeOffset date, int emklId, string emklCode, string emklName, string attn, string fax, string cc,int shippingStaffId, string shippingStaffName,string phone, string shippedBy, DateTimeOffset truckingDate, string cartonNo, string portOfDischarge,string placeOfDelivery,string feederVessel, string oceanVessel, string carrier, string flight, string transit, int bankAccountId, string bankAccountName,int buyerAgentId, string buyerAgentCode, string buyerAgentName, string buyerAgentAddress, string notify, string specialInstruction)
        {
            InvoiceNo = invoiceNo;
            PackingListId = packingListId;
            Date = date;
            EMKLCode = emklCode;
            EMKLId = emklId;
            EMKLName = emklName;
            ATTN = attn;
            Fax = fax;
            CC = cc;
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
            BuyerAgentAddress = buyerAgentAddress;
            BuyerAgentCode = buyerAgentAddress;
            BuyerAgentName = buyerAgentName;
            BuyerAgentId = buyerAgentId;
            Notify = notify;
            SpecialInstruction = specialInstruction;
        }

        public GarmentShippingInstructionModel()
        {
        }

        public void SetDate(DateTimeOffset date, string userName, string userAgent)
        {
            if (Date != date)
            {
                Date = date;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetEMKLId(int emklId, string userName, string userAgent)
        {
            if (EMKLId != emklId)
            {
                EMKLId = emklId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetEMKLName(string emklName, string userName, string userAgent)
        {
            if (EMKLName != emklName)
            {
                EMKLName = emklName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetEMKLCode(string emklCode, string userName, string userAgent)
        {
            if (EMKLCode != emklCode)
            {
                EMKLCode = emklCode;
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
    }
}
