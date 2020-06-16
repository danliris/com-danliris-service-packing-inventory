using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPYarnType;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPYarnType
{
    public class IPYarnTypeService : IIPYarnTypeService
    {
        private readonly IIPYarnTypeRepository _repository;

        /// <summary>
        /// Used for viewmodel to dbmodel
        /// </summary>
        private IPYarnTypeModel MappingDbModel(IPYarnTypeViewModel viewModel)
        {
            return new IPYarnTypeModel(viewModel.Code, viewModel.YarnType);
        }
        /// <summary>
        /// Used for dbmodel to viewmodel(indexViewModel)
        /// </summary>
        private IPYarnTypeViewModel MappingIndexViewModel(IPYarnTypeModel modelDb)
        {
            return new IPYarnTypeViewModel
            {
                Code = modelDb.Code,
                YarnType = modelDb.YarnType,
            };
        }
        /// <summary>
        /// Used for listDbModel to listViewModel(indexViewModel)
        /// </summary>
        private ListResult<IPYarnTypeViewModel> MappingIndexViewModel(List<IPYarnTypeModel> modelDb, int page, int size, int totalAllRow)
        {
            var listIndexViewModel = new List<IPYarnTypeViewModel>();
            foreach (var model in modelDb)
            {
                listIndexViewModel.Add(MappingIndexViewModel(model));
            }
            return new ListResult<IPYarnTypeViewModel>(listIndexViewModel, page, size, totalAllRow);
        }
        /// <summary>
        /// Used For dbModelto viewmodel
        /// </summary>
        private IPYarnTypeViewModel MappingViewModel(IPYarnTypeModel modelDb)
        {
            return new IPYarnTypeViewModel
            {
                Code = modelDb.Code,
                YarnType = modelDb.YarnType
            };
        }

        public IPYarnTypeService(IServiceProvider _serviceProvider)
        {
            _repository = _serviceProvider.GetService<IIPYarnTypeRepository>();
        }

        public Task<int> Create(IPYarnTypeViewModel model)
        {
            var mappingModel = MappingDbModel(model);
            return _repository.InsertAsync(mappingModel);
        }

        public Task<int> Delete(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public ListResult<IPYarnTypeViewModel> ReadAll()
        {
            var data = _repository.ReadAll();
            var dataCount = data.Count();

            return MappingIndexViewModel(data.ToList(),
                            1,
                            dataCount,
                            dataCount);
        }

        public async Task<IPYarnTypeViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            return MappingViewModel(data);
        }

        public ListResult<IPYarnTypeViewModel> ReadByPage(string keyword, string order, int page, int size)
        {
            var data = _repository.ReadAll().Skip((page - 1) * size).Take(size);
            var dataCount = data.Count();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(entity => entity.YarnType.Contains(keyword) || entity.Code.Contains(keyword));
            }

            if (string.IsNullOrWhiteSpace(order))
            {
                order = "{}";
            }
            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            data = QueryHelper<IPYarnTypeModel>.Order(data, orderDictionary);


            return MappingIndexViewModel(data.ToList(),
                            1,
                            dataCount,
                            dataCount);
        }

        public Task<int> Update(int id, IPYarnTypeViewModel model)
        {
            return _repository.UpdateAsync(id,
                               MappingDbModel(model));
        }
    }
}
