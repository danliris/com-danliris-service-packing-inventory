using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDebiturBalance;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentDebiturBalance;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentDebiturBalance
{
    public class GarmentDebiturBalanceDataUtil : BaseDataUtil<GarmentDebiturBalanceRepository, GarmentDebiturBalanceModel>
    {
        public GarmentDebiturBalanceDataUtil(GarmentDebiturBalanceRepository repository) : base(repository)
        {
        }

        public override GarmentDebiturBalanceModel GetModel()
        {
            var model = new GarmentDebiturBalanceModel(DateTimeOffset.Now, 1, "", "",1);

            return model;
        }

        public override GarmentDebiturBalanceModel GetEmptyModel()
        {
            var model = new GarmentDebiturBalanceModel(DateTimeOffset.MinValue, 0, null, null, 0);

            return model;
        }
    }
}
