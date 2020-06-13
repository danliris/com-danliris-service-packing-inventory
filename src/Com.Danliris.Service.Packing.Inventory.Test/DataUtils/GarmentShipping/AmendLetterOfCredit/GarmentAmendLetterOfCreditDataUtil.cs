using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.AmendLetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.AmendLetterOfCredit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.AmendLetterOfCredit
{
    public class GarmentAmendLetterOfCreditDataUtil : BaseDataUtil<GarmentAmendLetterOfCreditRepository, GarmentShippingAmendLetterOfCreditModel>
    {
        public GarmentAmendLetterOfCreditDataUtil(GarmentAmendLetterOfCreditRepository repository) : base(repository)
        {
        }

        public override GarmentShippingAmendLetterOfCreditModel GetModel()
        {
            var model = new GarmentShippingAmendLetterOfCreditModel("no",1,1, DateTimeOffset.Now, "",  2);
            return model;
        }

        public override GarmentShippingAmendLetterOfCreditModel GetEmptyModel()
        {
            var model = new GarmentShippingAmendLetterOfCreditModel(null,0,0, DateTimeOffset.MinValue, null, 0);
            return model;
        }
    }
}
