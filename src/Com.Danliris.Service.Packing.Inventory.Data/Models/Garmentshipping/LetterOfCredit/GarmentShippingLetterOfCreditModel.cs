using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LetterOfCredit
{
    public class GarmentShippingLetterOfCreditModel : StandardEntity
    {
        public string DocumentCreditNo { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public string IssuedBank { get; private set; }
        public int ApplicantId { get; private set; }
        public string ApplicantCode { get; private set; }
        public string ApplicantName { get; private set; }
        public DateTimeOffset ExpireDate { get; private set; }
        public string ExpirePlace { get; private set; }
        public DateTimeOffset LatestShipment { get; private set; }
        public string LCCondition { get; private set; }
        public double Quantity { get; private set; }
        public int UomId { get; private set; }
        public string UomUnit { get; private set; }
        public double TotalAmount { get; private set; }

        public GarmentShippingLetterOfCreditModel(string documentCreditNo, DateTimeOffset date, string issuedBank, int applicantId, string applicantCode, string applicantName, DateTimeOffset expireDate, string expirePlace, DateTimeOffset latestShipment, string lCCondition, double quantity, int uomId, string uomUnit, double totalAmount)
        {
            DocumentCreditNo = documentCreditNo;
            Date = date;
            IssuedBank = issuedBank;
            ApplicantId = applicantId;
            ApplicantCode = applicantCode;
            ApplicantName = applicantName;
            ExpireDate = expireDate;
            ExpirePlace = expirePlace;
            LatestShipment = latestShipment;
            LCCondition = lCCondition;
            Quantity = quantity;
            UomId = uomId;
            UomUnit = uomUnit;
            TotalAmount = totalAmount;
        }

        public void SetExpireDate(DateTimeOffset expireDate, string userName, string userAgent)
        {
            if (ExpireDate != expireDate)
            {
                ExpireDate = expireDate;
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

        public void SetDocumentCreditNo(string documentCreditNo, string userName, string userAgent)
        {
            if (DocumentCreditNo != documentCreditNo)
            {
                DocumentCreditNo = documentCreditNo;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetIssuedBank(string issuedBank, string userName, string userAgent)
        {
            if (IssuedBank != issuedBank)
            {
                IssuedBank = issuedBank;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetApplicantId(int applicantId, string userName, string userAgent)
        {
            if (ApplicantId != applicantId)
            {
                ApplicantId = applicantId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetApplicantCode(string applicantCode, string userName, string userAgent)
        {
            if (ApplicantCode != applicantCode)
            {
                ApplicantCode = applicantCode;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetApplicantName(string applicantName, string userName, string userAgent)
        {
            if (ApplicantName != applicantName)
            {
                ApplicantName = applicantName;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetLatestShipment(DateTimeOffset latestShipment, string userName, string userAgent)
        {
            if (LatestShipment != latestShipment)
            {
                LatestShipment = latestShipment;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetLCCondition(string lcCondition, string userName, string userAgent)
        {
            if (LCCondition != lcCondition)
            {
                LCCondition = lcCondition;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetQuantity(double quantity, string userName, string userAgent)
        {
            if (Quantity != quantity)
            {
                Quantity = quantity;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetTotalAmount(double totalAmount, string userName, string userAgent)
        {
            if (TotalAmount != totalAmount)
            {
                TotalAmount = totalAmount;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUomUnit(string uomUnit, string userName, string userAgent)
        {
            if (UomUnit != uomUnit)
            {
                UomUnit = uomUnit;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetUomId(int uomId, string userName, string userAgent)
        {
            if (UomId != uomId)
            {
                UomId = uomId;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetExpirePlace(string expirePlace, string userName, string userAgent)
        {
            if (ExpirePlace != expirePlace)
            {
                ExpirePlace = expirePlace;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
