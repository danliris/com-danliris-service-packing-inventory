using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNote;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using System.Linq.Dynamic.Core;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote
{
    public class MaterialDeliveryNoteService : IMaterialDeliveryNoteService
    {
        private readonly IMaterialDeliveryNoteRepository _MaterialDeliveryNoteRepository;
        private readonly IItemsRepository _ItemsRepository;


        public MaterialDeliveryNoteService(IServiceProvider serviceProvider)
        {
            _MaterialDeliveryNoteRepository = serviceProvider.GetService<IMaterialDeliveryNoteRepository>();
            _ItemsRepository = serviceProvider.GetService<IItemsRepository>();
        }

        private MaterialDeliveryNoteViewModel MapToViewModel(Data.MaterialDeliveryNoteModel model)
        {
            var vm = new MaterialDeliveryNoteViewModel()
            {
                Code = model.Code,
                DateSJ = model.DateSJ,
                BonCode = model.BonCode,
                DateFrom = (DateTimeOffset)model.DateFrom,
                DateTo = (DateTimeOffset)model.DateTo,
                DONumber = model.DONumber,
                FONumber = model.FONumber,
                Receiver = model.Receiver,
                Remark = model.Remark,
                SCNumber = model.SCNumber,
                Sender = model.Sender,
                StorageNumber = model.StorageNumber,
                Items = model.Items.Select(d => new ItemsViewModel()
                {
                    NoSPP = d.NoSPP,
                    MaterialName = d.MaterialName,
                    InputLot = d.InputLot,
                    WeightBruto = d.WeightBruto,
                    WeightDOS = d.WeightDOS,
                    WeightCone = d.WeightCone,
                    WeightBale = d.WeightBale,
                    GetTotal = d.GetTotal
                }).ToList()
            };

            return vm;
        }

            public async Task Create(MaterialDeliveryNoteViewModel viewModel)
        {
            var model = new Data.MaterialDeliveryNoteModel(null, viewModel.DateSJ, viewModel.BonCode, viewModel.DateFrom, viewModel.DateTo, viewModel.DONumber, viewModel.FONumber, viewModel.Receiver, viewModel.Remark,
                                                      viewModel.SCNumber, viewModel.Sender, viewModel.StorageNumber,
                                                      viewModel.Items.Select(s => new ItemsModel(s.NoSPP, s.MaterialName, s.InputLot, s.WeightBruto, s.WeightDOS, s.WeightCone, s.WeightBale, s.GetTotal)).ToList());

            foreach(var itm in viewModel.Items)
            {
                var modelItem = new ItemsModel(itm.NoSPP, itm.MaterialName, itm.InputLot, itm.WeightBruto, itm.WeightDOS, itm.WeightCone, itm.WeightBale, itm.GetTotal);

                await _ItemsRepository.InsertAsync(modelItem);
            }

            await _MaterialDeliveryNoteRepository.InsertAsync(model);
        }

        public Task Delete(int id)
        {
            return _MaterialDeliveryNoteRepository.DeleteAsync(id);
        }

        public async Task<MaterialDeliveryNoteViewModel> ReadById(int id)
        {
            var model = await _MaterialDeliveryNoteRepository.ReadByIdAsync(id);

            if (model == null)
                return null;
            //return _MaterialDeliveryNoteRepository.ReadByIdAsync(id);
            MaterialDeliveryNoteViewModel vm = MapToViewModel(model);
            return vm;
        }

        public Task Update(int id, MaterialDeliveryNoteViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public ListResult<MaterialDeliveryNoteViewModel> ReadByKeyword(string keyword, string order, int page, int size, string filter)
        {

            var query = _MaterialDeliveryNoteRepository.ReadAll();

            List<string> SearchAttributes = new List<string>()
            {
                "BonCode"
            };

            query = QueryHelper<Data.MaterialDeliveryNoteModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<Data.MaterialDeliveryNoteModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<Data.MaterialDeliveryNoteModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new MaterialDeliveryNoteViewModel()
            {
                Code = s.Code,
                DateSJ = s.DateSJ,
                BonCode = s.BonCode,
                DateFrom = (DateTimeOffset)s.DateFrom,
                DateTo = (DateTimeOffset)s.DateTo,
                DONumber = s.DONumber,
                FONumber = s.FONumber,
                Receiver = s.Receiver,
                Remark = s.Remark,
                SCNumber = s.SCNumber,
                Sender = s.Sender,
                StorageNumber = s.StorageNumber,
                Items = s.Items.Select(d => new ItemsViewModel()
                {
                    NoSPP = d.NoSPP,
                    MaterialName = d.MaterialName,
                    InputLot = d.InputLot,
                    WeightBruto= d.WeightBruto,
                    WeightDOS=d.WeightDOS,
                    WeightCone=d.WeightCone,
                    WeightBale=d.WeightBale,
                    GetTotal=d.GetTotal
                }).ToList()

            });

            return new ListResult<MaterialDeliveryNoteViewModel>(data.ToList(), page, size, query.Count());

        }
    }
}
