using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Master
{
    public class WeftTypeDataUtil : BaseDataUtil<WeftTypeRepository, WeftTypeModel>
    {
        public WeftTypeDataUtil(WeftTypeRepository repository) : base(repository)
        {
        }

        public override WeftTypeModel GetModel()
        {
            return new WeftTypeModel("Type", "01");
        }
    }
}
