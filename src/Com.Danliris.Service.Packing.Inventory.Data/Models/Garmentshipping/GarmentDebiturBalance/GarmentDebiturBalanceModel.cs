using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDebiturBalance
{
	public class GarmentDebiturBalanceModel : StandardEntity
	{

		public DateTimeOffset BalanceDate { get; set; }
		public int BuyerAgentId { get; set; }
		public string BuyerAgentCode { get; set; }
		public string BuyerAgentName { get; set; }
		public decimal BalanceAmount { get; set; }
		
		public GarmentDebiturBalanceModel(DateTimeOffset balanceDate, int buyerAgentId, string buyerAgentCode, string buyerAgentName, decimal balanceAmount)
		{
			this.BalanceDate = balanceDate;
			this.BuyerAgentId = buyerAgentId;
			this.BuyerAgentCode = buyerAgentCode;
			this.BuyerAgentName = buyerAgentName;
			this.BalanceAmount = balanceAmount;
        }

        public void SetBalanceDate(DateTimeOffset balanceDate, string userName, string userAgent)
        {
            if (BalanceDate != balanceDate)
            {
                BalanceDate = balanceDate;
                this.FlagForUpdate(userName, userAgent);
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

        public void SetBalanceAmount(decimal balanceAmount, string userName, string userAgent)
        {
            if (BalanceAmount != balanceAmount)
            {
                BalanceAmount = balanceAmount;
                this.FlagForUpdate(userName, userAgent);
            }
        }

    }
}
