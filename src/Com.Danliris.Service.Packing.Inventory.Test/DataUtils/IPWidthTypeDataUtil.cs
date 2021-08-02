using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWidthType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class IPWidthTypeDataUtil : BaseDataUtil<IPWidthTypeRepository, IPWidthTypeModel>
    {
        public IPWidthTypeDataUtil(IPWidthTypeRepository repository) : base(repository)
        {

        }
        public override IPWidthTypeModel GetModel()
        {
            return new IPWidthTypeModel("1","testing");
        }

    }
}
