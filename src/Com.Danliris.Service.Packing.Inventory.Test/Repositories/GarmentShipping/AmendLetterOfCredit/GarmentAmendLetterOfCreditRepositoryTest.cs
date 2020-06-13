using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.AmendLetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.AmendLetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.AmendLetterOfCredit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.AmendLetterOfCredit
{
    public class GarmentAmendLetterOfCreditRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, GarmentAmendLetterOfCreditRepository, GarmentShippingAmendLetterOfCreditModel, GarmentAmendLetterOfCreditDataUtil>
    {
        private const string ENTITY = "GarmentAmendLetterOfCredit";

        public GarmentAmendLetterOfCreditRepositoryTest() : base(ENTITY)
        {
        }
    }
}
