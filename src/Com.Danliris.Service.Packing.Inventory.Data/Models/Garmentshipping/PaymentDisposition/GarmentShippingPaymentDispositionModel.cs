using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionModel : StandardEntity
    {
        
        public string DispositionNo { get; private set; }
        public string PaymentType { get; private set; }
        public string PaymentMethod { get; private set; }
        public string PaidAt { get; private set; }
        public string SendBy { get; private set; }

        public int BuyerAgentId { get; set; }
        public string BuyerAgentCode { get; set; }
        public string BuyerAgentName { get; set; }

        public string PaymentTerm { get; private set; }

        public int ForwarderId { get; private set; }
        public string ForwarderCode { get; private set; }
        public string ForwarderName { get; private set; }

        public int CourierId { get; private set; }
        public string CourierCode { get; private set; }
        public string CourierName { get; private set; }

        public int EMKLId { get; private set; }
        public string EMKLCode { get; private set; }
        public string EMKLName { get; private set; }

        public string Address { get; private set; }
        public string NPWP { get; private set; }

        public string InvoiceNumber { get; private set; }
        public DateTimeOffset InvoiceDate { get; private set; }
        public string InvoiceTaxNumber { get; private set; }

        public decimal BillValue { get; private set; }
        public decimal VatValue { get; private set; }

        public int IncomeTaxId { get; private set; }
        public string IncomeTaxName { get; private set; }
        public decimal IncomeTaxRate { get; private set; }
        public decimal IncomeTaxValue { get; private set; }

        public decimal TotalBill { get; private set; }
        public DateTimeOffset PaymentDate { get; private set; }
        public string Bank { get; private set; }
        public string AccNo { get; private set; }

        public bool IsFreightCharged { get; private set; }
        public string FreightBy { get; private set; }
        public string FreightNo { get; private set; }
        public DateTimeOffset FreightDate { get; private set; }

        public string Remark { get; private set; }

        public ICollection<GarmentShippingPaymentDispositionInvoiceDetailModel> InvoiceDetails { get; set; }
        public ICollection<GarmentShippingPaymentDispositionBillDetailModel> BillDetails { get; set; }
        public ICollection<GarmentShippingPaymentDispositionUnitChargeModel> UnitCharges { get; set; }
       
        public GarmentShippingPaymentDispositionModel(string dispositionNo, string paymentType, string paymentMethod, string paidAt, string sendBy, int buyerAgentId, string buyerAgentCode, string buyerAgentName, string paymentTerm, int forwarderId, string forwarderCode, string forwarderName, int courierId, string courierCode, string courierName, int eMKLId, string eMKLCode, string eMKLName, string address, string nPWP, string invoiceNumber, DateTimeOffset invoiceDate, string invoiceTaxNumber, decimal billValue, decimal vatValue, int incomeTaxId, string incomeTaxName, decimal incomeTaxRate, decimal incomeTaxValue, decimal totalBill, DateTimeOffset paymentDate, string bank, string accNo, bool isFreightCharged, string freightBy, string freightNo, DateTimeOffset freightDate, string remark, ICollection<GarmentShippingPaymentDispositionInvoiceDetailModel> invoiceDetails, ICollection<GarmentShippingPaymentDispositionBillDetailModel> billDetails, ICollection<GarmentShippingPaymentDispositionUnitChargeModel> unitCharges)
        {
            DispositionNo = dispositionNo;
            PaymentType = paymentType;
            PaymentMethod = paymentMethod;
            PaidAt = paidAt;
            SendBy = sendBy;
            BuyerAgentId = buyerAgentId;
            BuyerAgentCode = buyerAgentCode;
            BuyerAgentName = buyerAgentName;
            PaymentTerm = paymentTerm;
            ForwarderId = forwarderId;
            ForwarderCode = forwarderCode;
            ForwarderName = forwarderName;
            CourierId = courierId;
            CourierCode = courierCode;
            CourierName = courierName;
            EMKLId = eMKLId;
            EMKLCode = eMKLCode;
            EMKLName = eMKLName;
            Address = address;
            NPWP = nPWP;
            InvoiceNumber = invoiceNumber;
            InvoiceDate = invoiceDate;
            InvoiceTaxNumber = invoiceTaxNumber;
            BillValue = billValue;
            VatValue = vatValue;
            IncomeTaxId = incomeTaxId;
            IncomeTaxName = incomeTaxName;
            IncomeTaxRate = incomeTaxRate;
            IncomeTaxValue = incomeTaxValue;
            TotalBill = totalBill;
            PaymentDate = paymentDate;
            Bank = bank;
            AccNo = accNo;
            IsFreightCharged = isFreightCharged;
            FreightBy = freightBy;
            FreightNo = freightNo;
            FreightDate = freightDate;
            Remark = remark;
            InvoiceDetails = invoiceDetails;
            BillDetails = billDetails;
            UnitCharges = unitCharges;
        }

        public GarmentShippingPaymentDispositionModel()
        {
        }

        public void SetBuyerAgentId(int buyerAgentId, string userName, string userAgent)
        {
            if (BuyerAgentId != buyerAgentId)
            {
                BuyerAgentId = buyerAgentId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerAgentCode(string buyerAgentCode, string userName, string userAgent)
        {
            if (BuyerAgentCode != buyerAgentCode)
            {
                BuyerAgentCode = buyerAgentCode;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBuyerAgentName(string buyerAgentName, string userName, string userAgent)
        {
            if (BuyerAgentName != buyerAgentName)
            {
                BuyerAgentName = buyerAgentName;
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
        public void SetCourierId(int courierId, string userName, string userAgent)
        {
            if (CourierId != courierId)
            {
                CourierId = courierId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCourierName(string courierName, string userName, string userAgent)
        {
            if (CourierName != courierName)
            {
                CourierName = courierName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCourierCode(string courierCode, string userName, string userAgent)
        {
            if (CourierCode != courierCode)
            {
                CourierCode = courierCode;
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

        public void SetAddress(string address, string userName, string userAgent)
        {
            if (Address != address)
            {
                Address = address;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetNPWP(string npwp, string userName, string userAgent)
        {
            if (NPWP != npwp)
            {
                NPWP = npwp;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPaymentMethod(string paymentMethod, string userName, string userAgent)
        {
            if (PaymentMethod != paymentMethod)
            {
                PaymentMethod = paymentMethod;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetSendBy(string sendBy, string userName, string userAgent)
        {
            if (SendBy != sendBy)
            {
                SendBy = sendBy;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetPaymentTerm(string paymentTerm, string userName, string userAgent)
        {
            if (PaymentTerm != paymentTerm)
            {
                PaymentTerm = paymentTerm;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetInvoiceNumber(string invoiceNo, string userName, string userAgent)
        {
            if (InvoiceNumber != invoiceNo)
            {
                InvoiceNumber = invoiceNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetInvoiceTaxNumber(string invoiceTaxNumber, string userName, string userAgent)
        {
            if (InvoiceTaxNumber != invoiceTaxNumber)
            {
                InvoiceTaxNumber = invoiceTaxNumber;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetInvoiceDate(DateTimeOffset invoiceDate, string userName, string userAgent)
        {
            if (InvoiceDate != invoiceDate)
            {
                InvoiceDate = invoiceDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIncomeTaxValue(decimal incomeTaxValue, string userName, string userAgent)
        {
            if (IncomeTaxValue != incomeTaxValue)
            {
                IncomeTaxValue = incomeTaxValue;
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

        public void SetIncomeTaxName(string incomeTaxName, string userName, string userAgent)
        {
            if (IncomeTaxName != incomeTaxName)
            {
                IncomeTaxName = incomeTaxName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIncomeIncomeTaxRate(decimal incomeTaxRate, string userName, string userAgent)
        {
            if (IncomeTaxRate != incomeTaxRate)
            {
                IncomeTaxRate = incomeTaxRate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetBillValue(decimal billValue, string userName, string userAgent)
        {
            if (BillValue != billValue)
            {
                BillValue = billValue;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetTotalBill(decimal totalBill, string userName, string userAgent)
        {
            if (TotalBill != totalBill)
            {
                TotalBill = totalBill;
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

        public void SetBank(string bank, string userName, string userAgent)
        {
            if (Bank != bank)
            {
                Bank = bank;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetAccNo(string accNo, string userName, string userAgent)
        {
            if (AccNo != accNo)
            {
                AccNo = accNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIsFreightCharged(bool isFreightCharged, string userName, string userAgent)
        {
            if (IsFreightCharged != isFreightCharged)
            {
                IsFreightCharged = isFreightCharged;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetFreightBy(string freightBy, string userName, string userAgent)
        {
            if (FreightBy != freightBy)
            {
                FreightBy = freightBy;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetFreightNo(string freightNo, string userName, string userAgent)
        {
            if (FreightNo != freightNo)
            {
                FreightNo = freightNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetFreightDate(DateTimeOffset freightDate, string userName, string userAgent)
        {
            if (FreightDate != freightDate)
            {
                FreightDate = freightDate;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetRemark(string remark, string userName, string userAgent)
        {
            if (Remark != remark)
            {
                Remark = remark;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
