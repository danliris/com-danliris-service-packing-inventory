using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Master
{
    public class MaterialConstructionDataUtil : BaseDataUtil<MaterialConstructionRepository, MaterialConstructionModel>
    {
        public MaterialConstructionDataUtil(MaterialConstructionRepository repository) : base(repository)
        {
        }

        public override MaterialConstructionModel GetModel()
        {
            return new MaterialConstructionModel("Type", "01");
        }
    }
}
