using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentCoverLetter
{
    public class GarmentCoverLetterDataUtil : BaseDataUtil<GarmentCoverLetterRepository, GarmentShippingCoverLetterModel>
    {
        public GarmentCoverLetterDataUtil(GarmentCoverLetterRepository repository) : base(repository)
        {
        }

        public override GarmentShippingCoverLetterModel GetModel()
        {
            var model = new GarmentShippingCoverLetterModel(1, 1, "", DateTimeOffset.Now, 1, "", "", "", "", "", "", "", DateTimeOffset.Now, 1, "", "", 1, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", 1, "");

            return model;
        }

        public override GarmentShippingCoverLetterModel GetEmptyModel()
        {
            var model = new GarmentShippingCoverLetterModel(0, 0, null, DateTimeOffset.MinValue, 0, null, null, null, null, null, null, null, DateTimeOffset.MinValue, 0, null, null, 0, 0, 0, 0, 0, null, null, null, null, null, null, null, null, null, null, DateTimeOffset.MinValue, null, 0, null);

            return model;
        }
    }
}
