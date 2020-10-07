using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionItemModel : StandardEntity
    {
        public int InsuranceDispositionId { get; set; }
        public DateTimeOffset PolicyDate { get; set; }
        public string PolicyNo { get; set; }
        public string InvoiceNo { get; set; }
        public int InvoiceId { get; set; }

        public int BuyerAgentId { get; set; }
        public string BuyerAgentCode { get; set; }
        public string BuyerAgentName { get; set; }
        public decimal Amount { get; set; }
        public decimal CurrencyRate { get; set; }
        public decimal Amount2A { get; set; }
        public decimal Amount2B { get; set; }
        public decimal Amount2C { get; set; }
        public decimal Amount1A { get; set; }
        public decimal Amount1B { get; set; }

        public GarmentShippingInsuranceDispositionItemModel(DateTimeOffset policyDate, string policyNo, string invoiceNo, int invoiceId, int buyerAgentId, string buyerAgentCode, string buyerAgentName, decimal amount, decimal currencyRate, decimal amount2A, decimal amount2B, decimal amount2C, decimal amount1A, decimal amount1B)
        {
            PolicyDate = policyDate;
            PolicyNo = policyNo;
            InvoiceNo = invoiceNo;
            InvoiceId = invoiceId;
            BuyerAgentId = buyerAgentId;
            BuyerAgentCode = buyerAgentCode;
            BuyerAgentName = buyerAgentName;
            Amount = amount;
            CurrencyRate = currencyRate;
            Amount2A = amount2A;
            Amount2B = amount2B;
            Amount2C = amount2C;
            Amount1A = amount1A;
            Amount1B = amount1B;
        }

        public GarmentShippingInsuranceDispositionItemModel()
        {
        }

        public void SetPolicyNo(string policyNo, string username, string uSER_AGENT)
        {
            if (PolicyNo != policyNo)
            {
                PolicyNo = policyNo;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetPolicyDate(DateTimeOffset policyDate, string username, string uSER_AGENT)
        {
            if (PolicyDate != policyDate)
            {
                PolicyDate = policyDate;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetCurrencyRate(decimal currencyRate, string username, string uSER_AGENT)
        {
            if (CurrencyRate != currencyRate)
            {
                CurrencyRate = currencyRate;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetAmount(decimal amount, string username, string uSER_AGENT)
        {
            if (Amount != amount)
            {
                Amount = amount;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }
    }
}
