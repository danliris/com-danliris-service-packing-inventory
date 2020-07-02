using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWovenType;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWovenType
{
    public class IPWovenTypeService : IIPWovenTypeService
    {
        private readonly IIPWovenTypeRepository _repository;

        public IPWovenTypeService(IServiceProvider _serviceProvider)
        {
            _repository = _serviceProvider.GetService<IIPWovenTypeRepository>();
        }

        /// <summary>
        /// Used for viewmodel to dbmodel
        /// </summary>
        private IPWovenTypeModel MappingDbModel(IPWovenTypeViewModel viewModel)
        {
            return new IPWovenTypeModel(viewModel.Code, viewModel.WovenType);
        }
        /// <summary>
        /// Used for dbmodel to viewmodel(indexViewModel)
        /// </summary>
        private IPWovenTypeViewModel MappingIndexViewModel(IPWovenTypeModel modelDb)
        {
            return new IPWovenTypeViewModel
            {
                Id = modelDb.Id,
                Code = modelDb.Code,
                WovenType = modelDb.WovenType,
            };
        }
        /// <summary>
        /// Used for listDbModel to listViewModel(indexViewModel)
        /// </summary>
        private ListResult<IPWovenTypeViewModel> MappingIndexViewModel(List<IPWovenTypeModel> modelDb, int page, int size, int totalAllRow)
        {
            var listIndexViewModel = new List<IPWovenTypeViewModel>();
            foreach (var model in modelDb)
            {
                listIndexViewModel.Add(MappingIndexViewModel(model));
            }
            return new ListResult<IPWovenTypeViewModel>(listIndexViewModel, page, size, totalAllRow);
        }
        /// <summary>
        /// Used For dbModelto viewmodel
        /// </summary>
        private IPWovenTypeViewModel MappingViewModel(IPWovenTypeModel modelDb)
        {
            return new IPWovenTypeViewModel
            {
                Id = modelDb.Id,
                Code = modelDb.Code,
                WovenType = modelDb.WovenType
            };
        }
        public Task<int> Create(IPWovenTypeViewModel model)
        {
            var mappingModel = MappingDbModel(model);
            return _repository.InsertAsync(mappingModel);
        }

        public Task<int> Delete(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public ListResult<IPWovenTypeViewModel> ReadAll()
        {
            var data = _repository.ReadAll();
            var dataCount = data.Count();

            return MappingIndexViewModel(data.ToList(),
                            1,
                            dataCount,
                            dataCount);
        }

        public async Task<IPWovenTypeViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            return MappingViewModel(data);
        }

        public ListResult<IPWovenTypeViewModel> ReadByPage(string keyword, string order, int page, int size)
        {
            var data = _repository.ReadAll().Skip((page - 1) * size).Take(size);
            var dataCount = data.Count();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(entity => entity.WovenType.Contains(keyword) || entity.Code.Contains(keyword));
            }

            if (string.IsNullOrWhiteSpace(order))
            {
                order = "{}";
            }
            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            data = QueryHelper<IPWovenTypeModel>.Order(data, orderDictionary);


            return MappingIndexViewModel(data.ToList(),
                            1,
                            dataCount,
                            dataCount);
        }

        public Task<int> Update(int id, IPWovenTypeViewModel model)
        {
            return _repository.UpdateAsync(id,
                               MappingDbModel(model));
        }
    }
}
