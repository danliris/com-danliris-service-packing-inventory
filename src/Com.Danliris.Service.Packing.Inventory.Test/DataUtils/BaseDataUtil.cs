using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public abstract class BaseDataUtil<TRepository, TModel>
        where TRepository : IRepository<TModel>
        where TModel : StandardEntity
    {
        private TRepository _repo;

        public BaseDataUtil(TRepository repository)
        {
            _repo = repository;
        }

        public abstract TModel GetModel();

        public virtual TModel GetEmptyModel()
        {
            return Activator.CreateInstance(typeof(TModel)) as TModel;
        }

        public async Task<TModel> GetTestData()
        {
            TModel data;
            data = GetModel();

            await _repo.InsertAsync(data);
            return data;
        }
    }
}
