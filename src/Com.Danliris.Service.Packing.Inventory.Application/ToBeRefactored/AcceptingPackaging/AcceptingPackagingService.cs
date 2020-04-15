using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AcceptingPackaging;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.AcceptingPackaging;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Application.AcceptingPackaging
{
    public class AcceptingPackagingService : IAcceptingPackagingService
    {
        private readonly IAcceptingPackagingRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _dyeingRepository;

        public AcceptingPackagingService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IAcceptingPackagingRepository>();
            _dyeingRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
        }

        public Task<int> Create(AcceptingPackagingViewModel viewModel)
        {
            var data = MappingIndexViewModelToRepo(viewModel);
            if (data != null)
            {
                return _repository.InsertAsync(viewModel.NoBon, data);
            }
            else
            {
                return Task.FromResult(0);
            }
        }

        public Task<int> Delete(int id)
        {
            return _repository.DeleteAsync(id);
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

        private IQueryable<IndexViewModel> MappingIndexViewModelAsQueryable(List<AcceptingPackagingModel> indexModel)
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
        
        private AcceptingPackagingModel MappingIndexViewModelToRepo(AcceptingPackagingViewModel indexModel)
        {
            var result = new AcceptingPackagingModel
            {
                Active = indexModel.Active,
                BonNo = indexModel.NoBon,
                Satuan = indexModel.Satuan,
                CreatedBy = indexModel.CreatedBy,
                CreatedAgent = indexModel.CreatedAgent,
                CreatedUtc = indexModel.CreatedUtc,
                DeletedAgent = indexModel.DeletedAgent,
                DeletedBy = indexModel.DeletedBy,
                DeletedUtc = indexModel.DeletedUtc,
                Grade = indexModel.Grade,
                IdDyeingPrintingMovement = indexModel.Id,
                Id = indexModel.Id,
                IsDeleted = indexModel.IsDeleted,
                Date = new DateTimeOffset(DateTime.Now),
                LastModifiedAgent = indexModel.LastModifiedAgent,
                LastModifiedBy = indexModel.LastModifiedBy,
                LastModifiedUtc = indexModel.LastModifiedUtc,
                MaterialObject = indexModel.MaterialObject.Name,
                Motif = indexModel.Motif,
                Mtr = indexModel.Mtr,
                OrderNo = indexModel.NoBon,
                Saldo = indexModel.Saldo,
                Shift = indexModel.Shift,
                Unit = indexModel.Unit.Name,
                Warna = indexModel.Warna,
                Yds = indexModel.Yds
            };
            return result;
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = MappingIndexViewModelAsQueryable(_repository.ReadAll().ToList());


            List<string> SearchAttributes = new List<string>()
            {
                "NoBon", "NoSpp"
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
            var model = await _dyeingRepository.ReadByIdAsync(id);
            if (model == null)
                return null;

            AcceptingPackagingViewModel vm = new AcceptingPackagingViewModel(model);

            return vm;
        }

        public Task<int> Update(int id, AcceptingPackagingViewModel viewModel)
        {
            return _repository.UpdateAsync(viewModel.NoBon, MappingIndexViewModelToRepo(viewModel));
        }

        public AcceptingPackagingViewModel ReadByBonNo(string bonNo)
        {
            return new AcceptingPackagingViewModel(_dyeingRepository.ReadAll().Where(s=>s.BonNo == bonNo).FirstOrDefault());
        }

        public List<string> ReadAllBonNo()
        {
            return _dyeingRepository.ReadAll().GroupBy(x => x.BonNo).Select(x => x.Key).ToList();
        }

        public List<string> ReadContainBonNo(string bonNo)
        {
            //return _dyeingRepository.ReadAll().(new AcceptingPackagingViewModel { NoBon = bonNo })
            return (from x in _dyeingRepository.ReadAll()
                    where x.BonNo.Contains(bonNo)
                    group x by x.BonNo).Select(x => x.Key).ToList();
        }

        public ListResult<AcceptingPackagingViewModel> ReadDetails(int page, int size, string filter, string order, string keyword)
        {
            var query = MappingListAsQueryable(_dyeingRepository.ReadAll().ToList());

            List<string> SearchAttributes = new List<string>()
            {
                "NoBon"
            };

            query = QueryHelper<AcceptingPackagingViewModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<AcceptingPackagingViewModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<AcceptingPackagingViewModel>.Order(query, OrderDictionary);

            var data = query.Skip((page - 1) * size).Take(size).Select(s => s);

            return new ListResult<AcceptingPackagingViewModel>(data.ToList(), page, size, query.Count());
        }
    }
}
