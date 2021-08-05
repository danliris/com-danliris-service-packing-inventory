using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetter;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalCoverLetter
{
    public class GarmentLocalCoverLetterDataUtil : BaseDataUtil<GarmentLocalCoverLetterRepository, GarmentShippingLocalCoverLetterModel>
    {
        public GarmentLocalCoverLetterDataUtil(GarmentLocalCoverLetterRepository repository) : base(repository)
        {
        }

        public override GarmentShippingLocalCoverLetterModel GetModel()
        {
            var model = new GarmentShippingLocalCoverLetterModel(1, "", "", DateTimeOffset.Now, 1, "", "", "", "", "", DateTimeOffset.Now, "", "", "", 1, "");

            return model;
        }

        public override GarmentShippingLocalCoverLetterModel GetEmptyModel()
        {
            var model = new GarmentShippingLocalCoverLetterModel(0, null, null, DateTimeOffset.MinValue, 0, null, null, null, null, null, DateTimeOffset.MinValue, null, null, null, 0, null);

            return model;
        }
    }
}
