using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.AcceptingPackaging
{
    public class AcceptingPackagingService : IAcceptingPackagingService
    {
        private readonly IDyeingPrintingAreaMovementRepository _repository;

        public Task<int> Create(AcceptingPackagingViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        private IQueryable<AcceptingPackagingViewModel> MappingListAsQueryable(List<DyeingPrintingAreaMovementModel> indexModel)
        {
            List<AcceptingPackagingViewModel> result = new List<AcceptingPackagingViewModel>();
            foreach(var item in indexModel)
            {
                result.Add(new AcceptingPackagingViewModel(item));
            }
            return result.AsQueryable();
        }
        private IQueryable<IndexViewModel> MappingIndexViewModelAsQueryable(List<AcceptingPackagingViewModel> indexModel)
        {
            List<IndexViewModel> result = new List<IndexViewModel>();
            foreach (var item in indexModel)
            {
                result.Add(new IndexViewModel(item));
            }
            return result.AsQueryable();
        }
        private IQueryable<IndexViewModel> MappingIndexViewModelAsQueryable(List<DyeingPrintingAreaMovementModel> indexModel)
        {
            List<IndexViewModel> result = new List<IndexViewModel>();
            foreach (var item in indexModel)
            {
                result.Add(new IndexViewModel(item));
            }
            return result.AsQueryable();
        }
               
        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = MappingIndexViewModelAsQueryable(_repository.ReadAll().ToList());

            List<string> SearchAttributes = new List<string>()
            {
                "BonNo", "NoSpp"
            };

            query = QueryHelper<IndexViewModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<IndexViewModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<IndexViewModel>.Order(query, OrderDictionary);

            var data = query.Skip((page - 1) * size).Take(size).Select(s=> s);

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<AcceptingPackagingViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            AcceptingPackagingViewModel vm = new AcceptingPackagingViewModel(model);

            return vm;
        }

        public Task<int> Update(int id, AcceptingPackagingViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
