using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWarpType;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWarpType
{
    public class IPWarpTypeService : IIPWarpTypeService
    {
        private readonly IIPWarpTypeRepository _repository;

        public IPWarpTypeService(IServiceProvider _serviceProvider)
        {
            _repository = _serviceProvider.GetService<IIPWarpTypeRepository>();
        }

        /// <summary>
        /// Used for viewmodel to dbmodel
        /// </summary>
        private IPWarpTypeModel MappingDbModel(IPWarpTypeViewModel viewModel)
        {
            return new IPWarpTypeModel(viewModel.Code, viewModel.WarpType);
        }
        /// <summary>
        /// Used for dbmodel to viewmodel(indexViewModel)
        /// </summary>
        private IPWarpTypeViewModel MappingIndexViewModel(IPWarpTypeModel modelDb)
        {
            return new IPWarpTypeViewModel
            {
                Code = modelDb.Code,
                WarpType = modelDb.WarpType,
            };
        }
        /// <summary>
        /// Used for listDbModel to listViewModel(indexViewModel)
        /// </summary>
        private ListResult<IPWarpTypeViewModel> MappingIndexViewModel(List<IPWarpTypeModel> modelDb, int page, int size, int totalAllRow)
        {
            var listIndexViewModel = new List<IPWarpTypeViewModel>();
            foreach (var model in modelDb)
            {
                listIndexViewModel.Add(MappingIndexViewModel(model));
            }
            return new ListResult<IPWarpTypeViewModel>(listIndexViewModel, page, size, totalAllRow);
        }
        /// <summary>
        /// Used For dbModelto viewmodel
        /// </summary>
        private IPWarpTypeViewModel MappingViewModel(IPWarpTypeModel modelDb)
        {
            return new IPWarpTypeViewModel
            {
                Code = modelDb.Code,
                WarpType = modelDb.WarpType
            };
        }
        public Task<int> Create(IPWarpTypeViewModel model)
        {
            var mappingModel = MappingDbModel(model);
            return _repository.InsertAsync(mappingModel);
        }

        public Task<int> Delete(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public ListResult<IPWarpTypeViewModel> ReadAll()
        {
            var data = _repository.ReadAll();
            var dataCount = data.Count();

            return MappingIndexViewModel(data.ToList(),
                            1,
                            dataCount,
                            dataCount);
        }

        public async Task<IPWarpTypeViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            return MappingViewModel(data);
        }

        public ListResult<IPWarpTypeViewModel> ReadByPage(string keyword, string order, int page, int size)
        {
            var data = _repository.ReadAll().Skip((page - 1) * size).Take(size);
            var dataCount = data.Count();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(entity => entity.WarpType.Contains(keyword) || entity.Code.Contains(keyword));
            }

            if (string.IsNullOrWhiteSpace(order))
            {
                order = "{}";
            }
            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            data = QueryHelper<IPWarpTypeModel>.Order(data, orderDictionary);


            return MappingIndexViewModel(data.ToList(),
                            1,
                            dataCount,
                            dataCount);
        }

        public Task<int> Update(int id, IPWarpTypeViewModel model)
        {
            return _repository.UpdateAsync(id,
                               MappingDbModel(model));
        }
    }
}
