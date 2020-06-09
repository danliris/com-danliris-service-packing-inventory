using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentCoverLetter
{
    public class GarmentCoverLetterDataUtil : BaseDataUtil<GarmentCoverLetterRepository, GarmentCoverLetterModel>
    {
        public GarmentCoverLetterDataUtil(GarmentCoverLetterRepository repository) : base(repository)
        {
        }

        public override GarmentCoverLetterModel GetModel()
        {
            var model = new GarmentCoverLetterModel(1, "", DateTimeOffset.Now, "", "", "", "", DateTimeOffset.Now, "", 1, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", "");

            return model;
        }

        public override GarmentCoverLetterModel GetEmptyModel()
        {
            var model = new GarmentCoverLetterModel(0, null, DateTimeOffset.MinValue, null, null, null, null, DateTimeOffset.MinValue, null, 0, 0, 0, 0, 0, null, null, null, null, null, null, null, null, null, null, DateTimeOffset.MinValue, null, null);

            return model;
        }
    }
}
