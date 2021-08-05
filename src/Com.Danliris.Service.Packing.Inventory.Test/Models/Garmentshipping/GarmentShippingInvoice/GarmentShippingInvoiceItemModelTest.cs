using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.Garmentshipping.GarmentShippingInvoice
{
    public class GarmentShippingInvoiceItemModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            GarmentShippingInvoiceItemModel model = new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 1, 1, "comocode", "comoname", "comodesc", "comodesc", "comodesc", "comodesc", 1, "pcs", 10, 10, 100, "usd", 1, "unitcode", 3, 1);
            Assert.Equal(1, model.PackingListItemId);
            Assert.NotNull(model);
        }
    }
}
