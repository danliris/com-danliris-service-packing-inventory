using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionInvoiceDetailModel : StandardEntity
    {

        public int PaymentDispositionId { get; set; }
        public string InvoiceNo { get; set; }
        public int InvoiceId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Volume { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal ChargeableWeight { get; set; }
        public decimal TotalCarton { get; set; }

        public int? BuyerAgentId { get; set; }
        public string BuyerAgentCode { get; set; }
        public string BuyerAgentName { get; set; }
        public GarmentShippingPaymentDispositionInvoiceDetailModel(string invoiceNo, int invoiceId, decimal quantity, decimal amount, decimal volume, decimal grossWeight, decimal chargeableWeight, decimal totalCarton, int buyerAgentId, string buyerAgentCode, string buyerAgentName)
        {
            InvoiceNo = invoiceNo;
            InvoiceId = invoiceId;
            Quantity = quantity;
            Amount = amount;
            Volume = volume;
            GrossWeight = grossWeight;
            ChargeableWeight = chargeableWeight;
            TotalCarton = totalCarton;
            BuyerAgentId = buyerAgentId;
            BuyerAgentCode = buyerAgentCode;
            BuyerAgentName = buyerAgentName;
        }

        public GarmentShippingPaymentDispositionInvoiceDetailModel()
        {
        }

        public void SetInvoiceId(int InvoiceId, string username, string uSER_AGENT)
        {
            if (this.InvoiceId != InvoiceId)
            {
                this.InvoiceId = InvoiceId;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetInvoiceNo(string InvoiceNo, string username, string uSER_AGENT)
        {
            if (this.InvoiceNo != InvoiceNo)
            {
                this.InvoiceNo = InvoiceNo;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }
        public void SetAmount(decimal Amount, string username, string uSER_AGENT)
        {
            if (this.Amount != Amount)
            {
                this.Amount = Amount;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }
        public void SetQuantity(decimal Quantity, string username, string uSER_AGENT)
        {
            if (this.Quantity != Quantity)
            {
                this.Quantity = Quantity;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }
        public void SetGrossWeight(decimal GrossWeight, string username, string uSER_AGENT)
        {
            if (this.GrossWeight != GrossWeight)
            {
                this.GrossWeight = GrossWeight;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }
        public void SetTotalCarton(decimal TotalCarton, string username, string uSER_AGENT)
        {
            if (this.TotalCarton != TotalCarton)
            {
                this.TotalCarton = TotalCarton;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }
        public void SetVolume(decimal Volume, string username, string uSER_AGENT)
        {
            if (this.Volume != Volume)
            {
                this.Volume = Volume;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }
        public void SetChargeableWeight(decimal ChargeableWeight, string username, string uSER_AGENT)
        {
            if (this.ChargeableWeight != ChargeableWeight)
            {
                this.ChargeableWeight = ChargeableWeight;
                this.FlagForUpdate(username, uSER_AGENT);
            }
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
    }
}
