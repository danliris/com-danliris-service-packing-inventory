using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWarpType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class IPWarpTypeDataUtil : BaseDataUtil<IPWarpTypeRepository, IPWarpTypeModel>
    {
        public IPWarpTypeDataUtil(IPWarpTypeRepository repository) : base(repository)
        {

        }
        public override IPWarpTypeModel GetModel()
        {
            return new IPWarpTypeModel("1","testing");
        }

    }
}
