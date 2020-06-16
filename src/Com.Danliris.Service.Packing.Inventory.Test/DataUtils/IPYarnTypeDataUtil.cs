using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPYarnType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class IPYarnTypeDataUtil : BaseDataUtil<IPYarnTypeRepository, IPYarnTypeModel>
    {
        public IPYarnTypeDataUtil(IPYarnTypeRepository repository) : base(repository)
        {

        }
        public override IPYarnTypeModel GetModel()
        {
            return new IPYarnTypeModel("1","testing");
        }

    }
}
