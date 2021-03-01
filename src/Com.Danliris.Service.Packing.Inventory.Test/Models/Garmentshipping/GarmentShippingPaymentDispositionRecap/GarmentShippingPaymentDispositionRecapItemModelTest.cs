using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDispositionRecap;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.Garmentshipping.GarmentShippingPaymentDispositionRecap
{
    public class GarmentShippingPaymentDispositionRecapItemModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            GarmentShippingPaymentDispositionRecapItemModel model = new GarmentShippingPaymentDispositionRecapItemModel();
            Assert.NotNull(model);
        }
    }
}
