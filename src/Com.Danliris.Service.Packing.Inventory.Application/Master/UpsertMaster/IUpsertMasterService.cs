using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.UpsertMaster
{
    public interface IUpsertMasterService
    {
        Task<WeftTypeModel> UpsertWeftType(string weftType);
        Task<UnitOfMeasurementModel> UpsertUOM(string uomUnit);
    }
}
