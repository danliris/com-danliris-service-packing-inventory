using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice
{
	public class GarmentShippingInvoiceModel : StandardEntity
	{

		public int PackingListId { get; set; }
		public string InvoiceNo { get; set; }
		public DateTimeOffset InvoiceDate { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public int BuyerAgentId { get; set; }
		public string BuyerAgentCode { get; set; }
		public string BuyerAgentName { get; set; }
		public string Consignee { get; set; }
        public string ConsigneeAddress { get; set; }
        public string LCNo { get; set; }
		public string IssuedBy { get; set; }
		public int SectionId { get; set; }
		public string SectionCode { get; set; }
		public string ShippingPer { get; set; }
		public DateTimeOffset SailingDate { get; set; }
		public string ConfirmationOfOrderNo { get; set; }
		public int ShippingStaffId { get; set; }
		public string ShippingStaff { get; set; }
		public int FabricTypeId { get; set; }
		public string FabricType { get; set; }
		public int BankAccountId { get; set; }
		public string BankAccount { get; set; }
		public int PaymentDue { get; set; }
		public string PEBNo { get; set; }
		public DateTimeOffset PEBDate { get; set; }
		public string NPENo { get; set; }
		public DateTimeOffset NPEDate { get; set; }
		public string Description { get; set; }
        public string Remark { get; set; }
        public decimal TotalAmount { get; set; }
		public decimal LessFabricCost { get; private set; }
		public decimal DHLCharges { get; private set; }
		public decimal AmountToBePaid { get; set; }
        public decimal AmountCA { get; set; }
        public string CPrice { get; set; }
		public string Memo { get; set; }
		public bool IsUsed { get; set; }
		public string BL { get; set; }
		public DateTimeOffset BLDate { get; set; }
		public string CO { get; set; }
		public DateTimeOffset CODate { get; set; }
		public string COTP { get; set; }
        public string DeliverTo { get; set; }
        public DateTimeOffset COTPDate { get; set; }
		public ICollection<GarmentShippingInvoiceItemModel> Items { get; set; }
		public ICollection<GarmentShippingInvoiceAdjustmentModel> GarmentShippingInvoiceAdjustment { get; private set; }
        public ICollection<GarmentShippingInvoiceUnitModel> GarmentShippingInvoiceUnit { get; private set; }

        public GarmentShippingInvoiceModel()
		{
			Items = new HashSet<GarmentShippingInvoiceItemModel>();
			GarmentShippingInvoiceAdjustment = new HashSet<GarmentShippingInvoiceAdjustmentModel>();
            GarmentShippingInvoiceUnit = new HashSet<GarmentShippingInvoiceUnitModel>();

        }

		public GarmentShippingInvoiceModel(int PackingListId,string InvoiceNo, DateTimeOffset InvoiceDate, string From, string To,int BuyerAgentId, string BuyerAgentCode,string BuyerAgentName, string Consignee, string LCNo, string IssuedBy, int SectionId,string SectionCode, string ShippingPer, DateTimeOffset SailingDate, string ConfirmationOfOrderNo, int ShippingStaffId,string ShippingStaff,int FabricTypeId, string FabricType, int BankAccountId,string BankAccount, int PaymentDue, string PEBNo, DateTimeOffset PEBDate, string NPENo, DateTimeOffset NPEDate, string Description, string Remark, ICollection<GarmentShippingInvoiceItemModel> Items, decimal AmountToBePaid, decimal AmountCA, string CPrice, string Say, string Memo,bool IsUsed,string BL,DateTimeOffset BLDate, string CO, DateTimeOffset CODate, string COTP, DateTimeOffset COTPDate, ICollection<GarmentShippingInvoiceAdjustmentModel> GarmentShippingInvoiceAdjustment,decimal TotalAmount, string consigneeAddress, string deliverTo, ICollection<GarmentShippingInvoiceUnitModel> GarmentShippingInvoiceUnit, decimal LessFabricCost, decimal DHLCharges )
		{
			this.PackingListId = PackingListId;
			this.InvoiceNo = InvoiceNo;
			this.InvoiceDate = InvoiceDate;
			this.From = From;
			this.To = To;
			this.BuyerAgentId = BuyerAgentId;
			this.BuyerAgentCode = BuyerAgentCode;
			this.BuyerAgentName = BuyerAgentName;
			this.Consignee = Consignee;
			this.LCNo = LCNo;
			this.IssuedBy = IssuedBy;
			this.SectionId = SectionId;
			this.SectionCode = SectionCode;
			this.ShippingPer = ShippingPer;
			this.SailingDate = SailingDate;
			this.ConfirmationOfOrderNo = ConfirmationOfOrderNo;
			this.ShippingStaffId = ShippingStaffId;
			this.ShippingStaff = ShippingStaff;
			this.FabricTypeId = FabricTypeId;
			this.FabricType = FabricType;
			this.BankAccountId = BankAccountId;
			this.BankAccount = BankAccount;
			this.PaymentDue = PaymentDue;
			this.PEBNo = PEBNo;
			this.PEBDate = PEBDate;
			this.NPENo = NPENo;
			this.NPEDate = NPEDate;
			this.Description = Description;
			this.Remark = Remark;
			this.AmountToBePaid = AmountToBePaid;
            this.AmountCA = AmountCA;
            this.CPrice = CPrice;
			 
			this.Memo = Memo;
			this.IsUsed = IsUsed;
			this.BL = BL;
			this.BLDate = BLDate;
			this.CO = CO;
			this.CODate = CODate;
			this.COTP = COTP;
			this.COTPDate = COTPDate;
			this.GarmentShippingInvoiceAdjustment = GarmentShippingInvoiceAdjustment;
			this.Items = Items;
			this.TotalAmount = TotalAmount;
            this.ConsigneeAddress = consigneeAddress;
            this.GarmentShippingInvoiceUnit = GarmentShippingInvoiceUnit;
            this.DeliverTo = deliverTo;
			this.LessFabricCost = LessFabricCost;
			this.DHLCharges = DHLCharges;

        }

		public void SetConsignee(string consignee, string username, string uSER_AGENT)
		{
			if (this.Consignee != consignee)
			{
				this.Consignee = consignee;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

        public void SetConsigneeAddress(string consigneeAddress, string username, string uSER_AGENT)
        {
            if (this.ConsigneeAddress != consigneeAddress)
            {
                this.ConsigneeAddress = consigneeAddress;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetPaymentDue(int paymentDue, string username, string uSER_AGENT)
		{
			if (this.PaymentDue != paymentDue)
			{
				this.PaymentDue = paymentDue;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetPEBNo(string pEBNo, string username, string uSER_AGENT)
		{
			if (this.PEBNo != pEBNo)
			{
				this.PEBNo = pEBNo;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetShippingPer(string shippingPer, string username, string uSER_AGENT)
		{
			if (this.ShippingPer != shippingPer)
			{
				this.ShippingPer = shippingPer;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetBLDate(DateTimeOffset bLDate, string username, string uSER_AGENT)
		{

			if (this.BLDate != bLDate)
			{
				this.BLDate = bLDate;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetCPrice(string cPrice, string username, string uSER_AGENT)
		{
			if (this.CPrice != cPrice)
			{
				this.CPrice = cPrice;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetCO(string cO, string username, string uSER_AGENT)
		{
			if (this.CO != cO)
			{
				this.CO = cO;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetCOTP(string cOTP, string username, string uSER_AGENT)
		{
			if (this.COTP != cOTP)
			{
				this.COTP = cOTP;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetCOTPDate(DateTimeOffset cOTPDate, string username, string uSER_AGENT)
		{
			if (this.COTPDate != cOTPDate)
			{
				this.COTPDate = cOTPDate;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}


		public void SetAmountToBePaid(decimal amountToBePaid, string username, string uSER_AGENT)
		{
			if (this.AmountToBePaid != amountToBePaid)
			{
				this.AmountToBePaid = amountToBePaid;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

        public void SetAmountCA(decimal amountCA, string username, string uSER_AGENT)
        {
            if (this.AmountCA != amountCA)
            {
                this.AmountCA = amountCA;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetTotalAmount(decimal totalAmount, string username, string uSER_AGENT)
		{
			if (this.TotalAmount != totalAmount)
			{
				this.TotalAmount = totalAmount;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetLessFabricCost(decimal LessFabricCost, string username, string uSER_AGENT)
		{
			if (this.LessFabricCost != LessFabricCost)
			{
				this.LessFabricCost = LessFabricCost;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetDHLCharges(decimal DHLCharges, string username, string uSER_AGENT)
		{
			if (this.DHLCharges != DHLCharges)
			{
				this.DHLCharges = DHLCharges;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetMemo(string memo, string username, string uSER_AGENT)
		{
			if (this.Memo != memo)
			{
				this.Memo = memo;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetDescription(string description, string username, string uSER_AGENT)
		{
			if (this.Description != description)
			{
				this.Description = description;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetRemark(string remark, string username, string uSER_AGENT)
		{
			if (this.Remark != remark)
			{
				this.Remark = remark;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetCODate(DateTimeOffset cODate, string username, string uSER_AGENT)
		{
			if (this.CODate != cODate)
			{
				this.CODate = cODate;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetBL(string bL, string username, string uSER_AGENT)
		{
			if (this.BL != bL)
			{
				this.BL = bL;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetPEBDate(DateTimeOffset pEBDate, string username, string uSER_AGENT)
		{
			if (this.PEBDate != pEBDate)
			{
				this.PEBDate = pEBDate;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetSailingDate(DateTimeOffset sailingDate, string username, string uSER_AGENT)
		{
			if (this.SailingDate != sailingDate)
			{
				this.SailingDate = sailingDate;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetNPENo(string nPENo, string username, string uSER_AGENT)
		{
			if (this.NPENo != nPENo)
			{
				this.NPENo = nPENo;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetNPEDate(DateTimeOffset nPEDate, string username, string uSER_AGENT)
		{
			if (this.NPEDate != nPEDate)
			{
				this.NPEDate = nPEDate;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetConfirmationOfOrderNo(string confirmationOfOrderNo, string username, string uSER_AGENT)
		{
			if (this.ConfirmationOfOrderNo != confirmationOfOrderNo)
			{
				this.ConfirmationOfOrderNo = confirmationOfOrderNo;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetShippingStaffId(int shippingStaffId, string username, string uSER_AGENT)
		{
			if (this.ShippingStaffId != shippingStaffId)
			{
				this.ShippingStaffId = shippingStaffId;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetShippingStaff(string shippingStaff, string username, string uSER_AGENT)
		{
			if (this.ShippingStaff != shippingStaff)
			{
				this.ShippingStaff = shippingStaff;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetFabricTypeId(int fabricTypeId, string username, string uSER_AGENT)
		{
			if (this.FabricTypeId != fabricTypeId)
			{
				this.FabricTypeId = fabricTypeId;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetFabricType(string fabricType, string username, string uSER_AGENT)
		{
			if (this.FabricType != fabricType)
			{
				this.FabricType = fabricType;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetBankAccountId(int bankAccountId, string username, string uSER_AGENT)
		{
			if (this.BankAccountId != bankAccountId)
			{
				this.BankAccountId = bankAccountId;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetBankAccount(string bankAccount, string username, string uSER_AGENT)
		{
			if (this.BankAccount != bankAccount)
			{
				this.BankAccount = bankAccount;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetFrom(string from, string username, string uSER_AGENT)
		{
			if (this.From != from)
			{
				this.From = from;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}
		public void SetTo(string to, string username, string uSER_AGENT)
		{
			if (this.To != to)
			{
				this.To = to;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

        public void SetIsUsed(bool isUsed, string username, string uSER_AGENT)
        {
            if (IsUsed != isUsed)
            {
                IsUsed = isUsed;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }

        public void SetDeliverTo(string DeliverTo, string username, string uSER_AGENT)
        {
            if (this.DeliverTo != DeliverTo)
            {
                this.DeliverTo = DeliverTo;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }
		public void SetInvoiceDate(DateTimeOffset invoiceDate, string username, string uSER_AGENT)
		{
			if (this.InvoiceDate != invoiceDate)
			{
				this.InvoiceDate = invoiceDate;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}
	}
}
