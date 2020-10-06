using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionModel : StandardEntity
    {
        public string DispositionNo { get; set; }
        public string PolicyType { get; set; }
        public DateTimeOffset PaymentDate { get; set; }
        public string BankName { get; set; }
        public int InsuranceId { get; set; }
        public string InsuranceName { get; set; }
        public string InsuranceCode { get; set; }
        public decimal Rate { get; set; }
        public string Remark { get; set; }
        public ICollection<GarmentShippingInsuranceDispositionUnitChargeModel> UnitCharge { get; set; }
        public ICollection<GarmentShippingInsuranceDispositionItemModel> Items { get; set; }

        public GarmentShippingInsuranceDispositionModel(string dispositionNo, string policyType, DateTimeOffset paymentDate, string bankName, int insuranceId, string insuranceName, string insuranceCode, decimal rate, string remark, ICollection<GarmentShippingInsuranceDispositionUnitChargeModel> unitCharge, ICollection<GarmentShippingInsuranceDispositionItemModel> items)
        {
            DispositionNo = dispositionNo;
            PolicyType = policyType;
            PaymentDate = paymentDate;
            BankName = bankName;
            InsuranceId = insuranceId;
            InsuranceName = insuranceName;
            InsuranceCode = insuranceCode;
            Rate = rate;
            Remark = remark;
            UnitCharge = unitCharge;
            Items = items;
        }

        public GarmentShippingInsuranceDispositionModel()
        {
        }

        public void SetPaymentDate(DateTimeOffset paymentDate, string username, string uSER_AGENT)
        {
            if (this.PaymentDate != paymentDate)
            {
                this.PaymentDate = paymentDate;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetInsuranceId(int insuranceId, string username, string uSER_AGENT)
        {
            if (InsuranceId != insuranceId)
            {
                InsuranceId = insuranceId;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetInsuranceCode(string insuranceCode, string username, string uSER_AGENT)
        {
            if (InsuranceCode != insuranceCode)
            {
                InsuranceCode = insuranceCode;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetInsuranceName(string insuranceName, string username, string uSER_AGENT)
        {
            if (InsuranceName != insuranceName)
            {
                InsuranceName = insuranceName;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetBankName(string bankName, string username, string uSER_AGENT)
        {
            if (BankName != bankName)
            {
                BankName = bankName;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetRate(decimal rate, string username, string uSER_AGENT)
        {
            if (Rate != rate)
            {
                Rate = rate;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetRemark(string remark, string username, string uSER_AGENT)
        {
            if (Remark != remark)
            {
                Remark = remark;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

    }
}
