using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Master
{
    public class WarpTypeDataUtil : BaseDataUtil<WarpTypeRepository, WarpTypeModel>
    {
        public WarpTypeDataUtil(WarpTypeRepository repository) : base(repository)
        {
        }

        public override WarpTypeModel GetModel()
        {
            return new WarpTypeModel("Type", "01");
        }
    }
}
