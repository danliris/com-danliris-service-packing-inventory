using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.Garmentshipping.CreditAdvice
{
    public class GarmentShippingCreditAdviceModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            GarmentShippingCreditAdviceModel model = new GarmentShippingCreditAdviceModel();
            model.SetInvoiceId(2, "username", "useragent");
            Assert.NotNull(model);
        }

        [Fact]
        public void should_Success_Instantiate1()
        {
            GarmentShippingCreditAdviceModel model = new GarmentShippingCreditAdviceModel();
            model.SetAmountPaid(2000, "username", "useragent");
            Assert.NotNull(model);
        }

        [Fact]
        public void should_Success_Instantiate2()
        {
            GarmentShippingCreditAdviceModel model = new GarmentShippingCreditAdviceModel();
            model.SetBalanceAmount (1000, "username", "useragent");
            Assert.NotNull(model);
        }
    }
}
