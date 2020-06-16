using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.IPWidthType;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;

using System.Linq;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWidthType
{
    public class IPWidthService : IIPWidthTypeService
    {
        private readonly IIPWidthTypeRepository _repository;

        public IPWidthService(IServiceProvider _serviceProvider)
        {
            _repository = _serviceProvider.GetService<IIPWidthTypeRepository>();
        }
        /// <summary>
        /// Used for viewmodel to dbmodel
        /// </summary>
        private IPWidthTypeModel MappingDbModel(IPWidthTypeViewModel viewModel)
        {
            return new IPWidthTypeModel(viewModel.Code, viewModel.WidthType);
        }
        /// <summary>
        /// Used for dbmodel to viewmodel(indexViewModel)
        /// </summary>
        private IndexViewModel MappingIndexViewModel(IPWidthTypeModel modelDb)
        {
            return new IndexViewModel
            {
                Code = modelDb.Code,
                WidthType = modelDb.WidthType,
            };
        }
        /// <summary>
        /// Used for listDbModel to listViewModel(indexViewModel)
        /// </summary>
        private ListResult<IndexViewModel> MappingIndexViewModel (List<IPWidthTypeModel> modelDb,int page,int size,int totalAllRow)
        {
            var listIndexViewModel = new List<IndexViewModel>();
            foreach(var model in modelDb)
            {
                listIndexViewModel.Add(MappingIndexViewModel(model));
            }
            return new ListResult<IndexViewModel>(listIndexViewModel, page, size, totalAllRow);
        }
        /// <summary>
        /// Used For dbModelto viewmodel
        /// </summary>
        private IPWidthTypeViewModel MappingViewModel(IPWidthTypeModel modelDb)
        {
            return new IPWidthTypeViewModel
            {
                Code = modelDb.Code,
                WidthType = modelDb.WidthType
            };
        }

        public Task<int> Create(IPWidthTypeViewModel model)
        {
            var mappingModel = MappingDbModel(model);
            return _repository.InsertAsync(mappingModel);
        }

        public Task<int> Delete(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public ListResult<IndexViewModel> ReadAll()
        {
            var data = _repository.ReadAll();
            var dataCount = data.Count();
            
            return MappingIndexViewModel(data.ToList(),
                            1,
                            dataCount,
                            dataCount);
        }

        public ListResult<IndexViewModel> ReadByPage(string keyword, string order, int page, int size)
        {
            var data = _repository.ReadAll().Skip((page - 1) * size).Take(size);
            var dataCount = data.Count();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(entity => entity.WidthType.Contains(keyword) || entity.Code.Contains(keyword));
            }

            if (string.IsNullOrWhiteSpace(order))
            {
                order = "{}";
            }
            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            data = QueryHelper<IPWidthTypeModel>.Order(data, orderDictionary);


            return MappingIndexViewModel(data.ToList(),
                            1,
                            dataCount,
                            dataCount);
        }

        public async Task<IPWidthTypeViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            return MappingViewModel(data);
        }

        public Task<int> Update(int id, IPWidthTypeViewModel model)
        {
            return _repository.UpdateAsync(id,
                               MappingDbModel(model));
        }
    }
}
