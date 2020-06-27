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
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;

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
                Active = model.Active,
                Id = model.Id,
                Code = model.Code,
                DateSJ = (DateTimeOffset)model.DateSJ,
                BonCode = model.BonCode,
                DateFrom = (DateTimeOffset)model.DateFrom,
                DateTo = (DateTimeOffset)model.DateTo,
                DONumber = new DeliveryOrderMaterialDeliveryNoteWeaving()
                {
                    Id = model.DoNumberId,
                    DOSalesNo = model.DONumber
                },
                FONumber = model.FONumber,
                buyer = new BuyerMaterialDeliveryNoteWeaving()
                {
                    Id = model.ReceiverId,
                    Code = model.ReceiverCode,
                    Name = model.ReceiverName
                },
                Remark = model.Remark,
                salesContract = new SalesContract() 
                {
                    SalesContractNo = model.SCNumber,
                    SalesContractId = model.SCNumberId
                },
                unit = new UnitMaterialDeliveryNoteWeaving() 
                {
                    Id = model.SenderId,
                    Code = model.SenderCode,
                    Name = model.SenderName
                },
                storage = new StorageMaterialDeliveryNoteWeaving() 
                {
                    Id = model.StorageId,
                    Code = model.StorageCode,
                    Name = model.StorageName
                },
                Items = model.Items.Select(d => new ItemsViewModel()
                {
                    IdSOP = d.IdSOP,
                    NoSOP = d.NoSOP,
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
            var model = new Data.MaterialDeliveryNoteModel(null, viewModel.DateSJ, viewModel.BonCode, viewModel.DateFrom, viewModel.DateTo, viewModel.DONumber.Id.GetValueOrDefault(), viewModel.DONumber.DOSalesNo, viewModel.FONumber, viewModel.buyer.Id.GetValueOrDefault(), viewModel.buyer.Code, 
                viewModel.buyer.Name,viewModel.Remark, viewModel.salesContract.SalesContractId.GetValueOrDefault(), viewModel.salesContract.SalesContractNo, viewModel.unit.Id.GetValueOrDefault(),viewModel.unit.Code, viewModel.unit.Name, 
                viewModel.storage.Id.GetValueOrDefault(), viewModel.storage.Code,viewModel.storage.Name,
                viewModel.Items.Select(s => new ItemsModel(s.IdSOP,s.NoSOP, s.MaterialName, s.InputLot, s.WeightBruto, s.WeightDOS, s.WeightCone, s.WeightBale, s.GetTotal)).ToList());

            foreach (var itm in viewModel.Items)
            {
                var modelItem = new ItemsModel(itm.IdSOP, itm.NoSOP, itm.MaterialName, itm.InputLot, itm.WeightBruto, itm.WeightDOS, itm.WeightCone, itm.WeightBale, itm.GetTotal);

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
            MaterialDeliveryNoteViewModel vm = MapToViewModel(model);
            return vm;
        }

        public async Task Update(int id, MaterialDeliveryNoteViewModel viewModel)
        {
            var model = new Data.MaterialDeliveryNoteModel(null, viewModel.DateSJ, viewModel.BonCode, viewModel.DateFrom, viewModel.DateTo, viewModel.DONumber.Id.GetValueOrDefault(), viewModel.DONumber.DOSalesNo, viewModel.FONumber, viewModel.buyer.Id.GetValueOrDefault(), viewModel.buyer.Code,
                viewModel.buyer.Name, viewModel.Remark, viewModel.salesContract.SalesContractId.GetValueOrDefault(), viewModel.salesContract.SalesContractNo, viewModel.unit.Id.GetValueOrDefault(), viewModel.unit.Code, viewModel.unit.Name,
                viewModel.storage.Id.GetValueOrDefault(), viewModel.storage.Code, viewModel.storage.Name,
                viewModel.Items.Select(s => new ItemsModel(s.IdSOP, s.NoSOP, s.MaterialName, s.InputLot, s.WeightBruto, s.WeightDOS, s.WeightCone, s.WeightBale, s.GetTotal)).ToList());

            await _MaterialDeliveryNoteRepository.UpdateAsync(id, model);
        }

        public ListResult<MaterialDeliveryNoteViewModel> ReadByKeyword(string keyword, string order, int page, int size, string filter)
        {

            var MaterialDeliveryNoteQuery = _MaterialDeliveryNoteRepository.ReadAll();

            var joinQuery = from MaterialDeliveryNote in MaterialDeliveryNoteQuery
                            select new MaterialDeliveryNoteViewModel()
                            {
                                Id = MaterialDeliveryNote.Id,
                                Code = MaterialDeliveryNote.Code,
                                BonCode = MaterialDeliveryNote.BonCode,
                                DateSJ = (DateTimeOffset)MaterialDeliveryNote.DateSJ,
                                buyer = new BuyerMaterialDeliveryNoteWeaving()
                                {
                                    Name = MaterialDeliveryNote.ReceiverName
                                },
                                unit = new UnitMaterialDeliveryNoteWeaving()
                                {
                                    Name = MaterialDeliveryNote.SenderName
                                }
                            };

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                joinQuery = joinQuery.Where(entity => entity.BonCode.Contains(keyword));
            }

            if (string.IsNullOrWhiteSpace(order))
            {
                order = "{}";
            }
            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            joinQuery = QueryHelper<MaterialDeliveryNoteViewModel>.Order(joinQuery, orderDictionary);

            var data = joinQuery.Skip((page - 1) * size).Take(size).ToList();
            var totalRow = joinQuery.Select(entity => entity.Id).Count();

            return new ListResult<MaterialDeliveryNoteViewModel>(data, page, size, totalRow);

        }
    }
}
