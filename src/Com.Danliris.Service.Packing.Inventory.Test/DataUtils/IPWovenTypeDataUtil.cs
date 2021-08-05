using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWovenType;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class IPWovenTypeDataUtil : BaseDataUtil<IPWovenTypeRepository, IPWovenTypeModel>
    {
        public IPWovenTypeDataUtil(IPWovenTypeRepository repository) : base(repository)
        {

        }
        public override IPWovenTypeModel GetModel()
        {
            return new IPWovenTypeModel("1","testing");
        }

    }
}
