using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.VBPayment
{
    public class GarmentShippingVBPaymentModel : StandardEntity
    {

        public string VBNo { get; private set; }
        public DateTimeOffset VBDate { get; private set; }
        public string PaymentType { get; private set; }
        public int BuyerId { get; private set; }
        public string BuyerCode { get; private set; }
        public string BuyerName { get; private set; }
        public int EMKLId { get; private set; }
        public string EMKLName { get; private set; }
        public string EMKLCode { get; private set; }
        public int ForwarderId { get; private set; }
        public string ForwarderCode { get; private set; }
        public string ForwarderName { get; private set; }
        public string EMKLInvoiceNo { get; private set; }
        public string ForwarderInvoiceNo{ get; private set; }

        public double BillValue { get; private set; }
        public double VatValue { get; private set; }
        public DateTimeOffset PaymentDate { get; private set; }

        public int IncomeTaxId { get; private set; }
        public string IncomeTaxName { get; private set; }
        public double IncomeTaxRate { get; private set; }

        public ICollection<GarmentShippingVBPaymentUnitModel> Units { get; private set; }
        public ICollection<GarmentShippingVBPaymentInvoiceModel> Invoices { get; private set; }


        public GarmentShippingVBPaymentModel(string vBNo, DateTimeOffset vBDate, string paymentType, int buyerId, string buyerCode, string buyerName, int eMKLId, string eMKLName, string eMKLCode, int forwarderId, string forwarderCode, string forwarderName, string eMKLInvoiceNo, string forwarderInvoiceNo, double billValue, double vatValue, DateTimeOffset paymentDate, int incomeTaxId, string incomeTaxName, double incomeTaxRate, ICollection<GarmentShippingVBPaymentUnitModel> units, ICollection<GarmentShippingVBPaymentInvoiceModel> invoices)
        {
            VBNo = vBNo;
            VBDate = vBDate;
            PaymentType = paymentType;
            BuyerId = buyerId;
            BuyerCode = buyerCode;
            BuyerName = buyerName;
            EMKLId = eMKLId;
            EMKLName = eMKLName;
            EMKLCode = eMKLCode;
            ForwarderId = forwarderId;
            ForwarderCode = forwarderCode;
            ForwarderName = forwarderName;
            EMKLInvoiceNo = eMKLInvoiceNo;
            ForwarderInvoiceNo = forwarderInvoiceNo;
            BillValue = billValue;
            VatValue = vatValue;
            PaymentDate = paymentDate;
            IncomeTaxId = incomeTaxId;
            IncomeTaxName = incomeTaxName;
            IncomeTaxRate = incomeTaxRate;
            Units = units;
            Invoices = invoices;
        }

        public GarmentShippingVBPaymentModel()
        {
        }

        public void SetVBDate(DateTimeOffset vBDate, string userName, string userAgent)
        {
            if (VBDate != vBDate)
            {
                VBDate = vBDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPaymentDate(DateTimeOffset paymentDate, string userName, string userAgent)
        {
            if (PaymentDate != paymentDate)
            {
                PaymentDate = paymentDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPaymentType(string paymentType, string userName, string userAgent)
        {
            if (PaymentType != paymentType)
            {
                PaymentType = paymentType;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerId(int buyerId, string userName, string userAgent)
        {
            if (BuyerId != buyerId)
            {
                BuyerId = buyerId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerCode(string buyerCode, string userName, string userAgent)
        {
            if (BuyerCode != buyerCode)
            {
                BuyerCode = buyerCode;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerName(string buyerName, string userName, string userAgent)
        {
            if (BuyerName != buyerName)
            {
                BuyerName = buyerName;
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

        public void SetEMKLId(int emklId, string userName, string userAgent)
        {
            if (EMKLId != emklId)
            {
                EMKLId = emklId;
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

        public void SetEMKLName(string emklName, string userName, string userAgent)
        {
            if (EMKLName != emklName)
            {
                EMKLName = emklName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetEMKLInvoiceNo(string emklInvoiceNo, string userName, string userAgent)
        {
            if (EMKLInvoiceNo != emklInvoiceNo)
            {
                EMKLInvoiceNo = emklInvoiceNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetForwarderInvoiceNo(string forwarderInvoiceNo, string userName, string userAgent)
        {
            if (ForwarderInvoiceNo != forwarderInvoiceNo)
            {
                ForwarderInvoiceNo = forwarderInvoiceNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }


        public void SetBillValue(double billValue, string userName, string userAgent)
        {
            if (BillValue != billValue)
            {
                BillValue = billValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetVatValue(double vatValue, string userName, string userAgent)
        {
            if (VatValue != vatValue)
            {
                VatValue = vatValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIncomeTaxId(int incomeTaxId, string userName, string userAgent)
        {
            if (IncomeTaxId != incomeTaxId)
            {
                IncomeTaxId = incomeTaxId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIncomeTaxRate(double incomeTaxRate, string userName, string userAgent)
        {
            if (IncomeTaxRate != incomeTaxRate)
            {
                IncomeTaxRate = incomeTaxRate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIncomeTaxName(string incomeTaxName, string userName, string userAgent)
        {
            if (IncomeTaxName != incomeTaxName)
            {
                IncomeTaxName = incomeTaxName;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
