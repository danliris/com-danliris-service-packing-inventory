using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LetterOfCredit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLetterOfCredit
{
    public class GarmentLetterOfCreditDataUtil : BaseDataUtil<GarmentLetterOfCreditRepository, GarmentShippingLetterOfCreditModel>
    {
        public GarmentLetterOfCreditDataUtil(GarmentLetterOfCreditRepository repository) : base(repository)
        {
        }

        public override GarmentShippingLetterOfCreditModel GetModel()
        {
            var model = new GarmentShippingLetterOfCreditModel("no", DateTimeOffset.Now,"",1,"","",DateTimeOffset.Now,"",DateTimeOffset.Now,"",1,1,"",2);
            return model;
        }

        public override GarmentShippingLetterOfCreditModel GetEmptyModel()
        {
            var model = new GarmentShippingLetterOfCreditModel(null, DateTimeOffset.MinValue, null, 0, null, null, DateTimeOffset.MinValue, null, DateTimeOffset.MinValue, null, 0, 0, null, 0);
            return model;
        }
    }
}