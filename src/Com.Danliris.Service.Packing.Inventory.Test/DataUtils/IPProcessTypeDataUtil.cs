using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPProcessType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class IPProcessTypeDataUtil : BaseDataUtil<IPProcessTypeRepository, IPProcessTypeModel>
    {
        public IPProcessTypeDataUtil(IPProcessTypeRepository repository) : base(repository)
        {

        }
        public override IPProcessTypeModel GetModel()
        {
            return new IPProcessTypeModel("1","testing");
        }

    }
}
