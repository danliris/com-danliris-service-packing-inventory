using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice
{
	public class GarmentShippingInvoiceAdjustmentModel : StandardEntity
	{
		
		public int GarmentShippingInvoiceId { get; set; }
		public string AdjustmentDescription { get; set; }
		public decimal AdjustmentValue { get; set; }
        public int AdditionalChargesId { get; set; }
        public GarmentShippingInvoiceAdjustmentModel()
		{
		}

		public GarmentShippingInvoiceAdjustmentModel(int GarmentShippingInvoiceId,string AdjustmentDescription, decimal AdjustmentValue, int AdditionalChargesId)
		{
			this.GarmentShippingInvoiceId = GarmentShippingInvoiceId;
			this.AdjustmentDescription = AdjustmentDescription;
			this.AdjustmentValue = AdjustmentValue;
            this.AdditionalChargesId = AdditionalChargesId;

            this.Id = Id;
		}


		public void SetAdjustmentDescription(string adjustmentDescription, string username, string uSER_AGENT)
		{
			if (AdjustmentDescription != adjustmentDescription)
			{
				AdjustmentDescription = adjustmentDescription;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

		public void SetAdjustmentValue(decimal adjustmentValue, string username, string uSER_AGENT)
		{
			if (AdjustmentValue != adjustmentValue)
			{
				AdjustmentValue = adjustmentValue;
				this.FlagForUpdate(username, uSER_AGENT);
			}
		}

        public void SetAdditionalChargesId(int additionalChargesId, string username, string uSER_AGENT)
        {
            if (AdditionalChargesId != additionalChargesId)
            {
                AdditionalChargesId = additionalChargesId;
                this.FlagForUpdate(username, uSER_AGENT);
            }
        }
    }
}
