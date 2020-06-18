using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPProcessType;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPProcessType
{
    public class IPProcessTypeService : IIPProcessTypeService
    {
        private readonly IIPProcessTypeRepository _repository;
        public IPProcessTypeService(IServiceProvider _serviceProvider)
        {
            _repository = _serviceProvider.GetService<IIPProcessTypeRepository>();
        }

        /// <summary>
        /// Used for viewmodel to dbmodel
        /// </summary>
        private IPProcessTypeModel MappingDbModel(IPProcessTypeViewModel viewModel)
        {
            return new IPProcessTypeModel(viewModel.Code, viewModel.ProcessType);
        }
        /// <summary>
        /// Used for dbmodel to viewmodel(indexViewModel)
        /// </summary>
        private IPProcessTypeViewModel MappingIndexViewModel(IPProcessTypeModel modelDb)
        {
            return new IPProcessTypeViewModel
            {
                Id = modelDb.Id,
                Code = modelDb.Code,
                ProcessType = modelDb.ProcessType,
            };
        }
        /// <summary>
        /// Used for listDbModel to listViewModel(indexViewModel)
        /// </summary>
        private ListResult<IPProcessTypeViewModel> MappingIndexViewModel(List<IPProcessTypeModel> modelDb, int page, int size, int totalAllRow)
        {
            var listIndexViewModel = new List<IPProcessTypeViewModel>();
            foreach (var model in modelDb)
            {
                listIndexViewModel.Add(MappingIndexViewModel(model));
            }
            return new ListResult<IPProcessTypeViewModel>(listIndexViewModel, page, size, totalAllRow);
        }
        /// <summary>
        /// Used For dbModelto viewmodel
        /// </summary>
        private IPProcessTypeViewModel MappingViewModel(IPProcessTypeModel modelDb)
        {
            return new IPProcessTypeViewModel
            {
                Id = modelDb.Id,
                Code = modelDb.Code,
                ProcessType = modelDb.ProcessType
            };
        }

        public Task<int> Create(IPProcessTypeViewModel model)
        {
            var mappingModel = MappingDbModel(model);
            return _repository.InsertAsync(mappingModel);
        }

        public Task<int> Delete(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public ListResult<IPProcessTypeViewModel> ReadAll()
        {
            var data = _repository.ReadAll();
            var dataCount = data.Count();

            return MappingIndexViewModel(data.ToList(),
                            1,
                            dataCount,
                            dataCount);
        }

        public async Task<IPProcessTypeViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            return MappingViewModel(data);
        }

        public ListResult<IPProcessTypeViewModel> ReadByPage(string keyword, string order, int page, int size)
        {
            var data = _repository.ReadAll().Skip((page - 1) * size).Take(size);
            var dataCount = data.Count();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(entity => entity.ProcessType.Contains(keyword) || entity.Code.Contains(keyword));
            }

            if (string.IsNullOrWhiteSpace(order))
            {
                order = "{}";
            }
            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            data = QueryHelper<IPProcessTypeModel>.Order(data, orderDictionary);


            return MappingIndexViewModel(data.ToList(),
                            1,
                            dataCount,
                            dataCount);
        }

        public Task<int> Update(int id, IPProcessTypeViewModel model)
        {
            return _repository.UpdateAsync(id,
                               MappingDbModel(model));
        }
    }
}
