using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLetterOfCredit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentLetterOfCredit
{
    public class GarmentLetterOfCreditRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, GarmentLetterOfCreditRepository, GarmentShippingLetterOfCreditModel, GarmentLetterOfCreditDataUtil>
    {
        private const string ENTITY = "GarmentLetterOfCredit";

        public GarmentLetterOfCreditRepositoryTest() : base(ENTITY)
        {
        }
    }
}
