using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Master;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.UpsertMaster
{
    public class UpsertMasterService : IUpsertMasterService
    {
        private readonly IWeftTypeRepository _weftTypeRepository;

        public UpsertMasterService(IServiceProvider serviceProvider)
        {
            _weftTypeRepository = serviceProvider.GetService<IWeftTypeRepository>();
        }

        public Task<WeftTypeModel> UpsertWeftType(string weftType)
        {
            throw new NotImplementedException();
        }
    }
}
