using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNote;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Models.MaterialDeliveryNote
{
    public class ItemsModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            ItemsModel model = new ItemsModel(
                "noSPP",
                "materialName",
                "inputLot",
                1,
                1,
                1,
                1,
                1
                );

            model.SetGetTotal(2);
            model.SetNoSPP("newNoSPP");
            model.SetMaterialName("newMaterialName");
            model.SetInputLot("NewInputLot");
            model.SetWeightBruto(2);
            model.SetWeightDOS(2);
            model.SetWeightCone(2);
            model.SetWeightBale(2);
           
        }
    }
}
