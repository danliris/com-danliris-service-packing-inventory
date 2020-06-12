using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNoteWeaving
{
    public class MaterialDeliveryNoteWeavingService : IMaterialDeliveryNoteWeavingService
    {

        private readonly IMaterialDeliveryNoteWeavingRepository _MaterialDeliveryNoteWeavingRepository;
        private readonly IItemsMaterialDeliveryNoteWeavingRepository _ItemsMaterialDeliveryNoteWeavingRepository;


        public MaterialDeliveryNoteWeavingService(IServiceProvider serviceProvider)
        {
            _MaterialDeliveryNoteWeavingRepository = serviceProvider.GetService<IMaterialDeliveryNoteWeavingRepository>();
            _ItemsMaterialDeliveryNoteWeavingRepository = serviceProvider.GetService<IItemsMaterialDeliveryNoteWeavingRepository>();
        }

        private MaterialDeliveryNoteWeavingViewModel MapToViewModel(Data.MaterialDeliveryNoteWeavingModel model)
        {
            var vm = new MaterialDeliveryNoteWeavingViewModel()
            {
                Id = model.Id,
                Code = model.Code,
                DateSJ = model.DateSJ,
                selectedDO = new DeliveryOrderMaterialDeliveryNoteWeaving()
                {
                    Id = model.DoSalesNumberId,
                    No = model.DoSalesNumber
                },
                SendTo = model.SendTo,
                Unit = new UnitMaterialDeliveryNoteWeaving()
                {
                    Id = model.UnitId,
                    Name = model.UnitName
                },
                Buyer = new BuyerMaterialDeliveryNoteWeaving()
                {
                    Id = model.BuyerId,
                    Code = model.Code,
                    Name = model.BuyerName
                },
                NumberBonOut = model.NumberOut,
                Storage = new StorageMaterialDeliveryNoteWeaving()
                {
                    Id = model.StorageId,
                    Code = model.StorageCode,
                    Name = model.StorageName
                },
                Remark = model.Remark,
                ItemsMaterialDeliveryNoteWeaving = model.ItemsMaterialDeliveryNoteWeaving.Select(d => new ItemsMaterialDeliveryNoteWeavingViewModel()
                {
                    itemNoSOP = d.itemNoSOP,
                    itemMaterialName = d.itemMaterialName,
                    itemGrade = d.itemGrade,
                    itemType = d.itemType,
                    itemCode = d.itemCode,
                    inputBale = d.inputBale,
                    inputPiece = d.inputPiece,
                    inputMeter = d.inputMeter,
                    inputKg = d.inputKg
                }).ToList()
            };

            return vm;
        }

        public async Task Create(MaterialDeliveryNoteWeavingViewModel viewModel)
        {
            var model = new Data.MaterialDeliveryNoteWeavingModel(null, viewModel.DateSJ.GetValueOrDefault(), viewModel.selectedDO.Id.GetValueOrDefault(), viewModel.selectedDO.No, viewModel.SendTo, viewModel.Unit.Id.GetValueOrDefault(), viewModel.Unit.Name,
                                                                viewModel.Buyer.Id.GetValueOrDefault(), viewModel.Buyer.Code, viewModel.Buyer.Name, viewModel.NumberBonOut, viewModel.Storage.Id, viewModel.Storage.Code, viewModel.Storage.Name,
                                                                viewModel.Remark,
                                                                viewModel.ItemsMaterialDeliveryNoteWeaving.Select(s => new ItemsMaterialDeliveryNoteWeavingModel(s.itemNoSOP, s.itemMaterialName, s.itemGrade, s.itemType, s.itemCode, s.inputBale.GetValueOrDefault(), s.inputPiece.GetValueOrDefault(), s.inputMeter.GetValueOrDefault(), s.inputKg.GetValueOrDefault())).ToList());

            foreach (var itm in viewModel.ItemsMaterialDeliveryNoteWeaving)
            {
                var modelItem = new ItemsMaterialDeliveryNoteWeavingModel(itm.itemNoSOP, itm.itemMaterialName, itm.itemGrade, itm.itemType, itm.itemCode, itm.inputBale.GetValueOrDefault(), itm.inputPiece.GetValueOrDefault(), itm.inputMeter.GetValueOrDefault(), itm.inputKg.GetValueOrDefault());

                await _ItemsMaterialDeliveryNoteWeavingRepository.InsertAsync(modelItem);
            }

            await _MaterialDeliveryNoteWeavingRepository.InsertAsync(model);
        }

        public Task Delete(int id)
        {
            return _MaterialDeliveryNoteWeavingRepository.DeleteAsync(id);
        }

        public async Task<MaterialDeliveryNoteWeavingViewModel> ReadById(int id)
        {
            var model = await _MaterialDeliveryNoteWeavingRepository.ReadByIdAsync(id);

            if (model == null)
                return null;
            MaterialDeliveryNoteWeavingViewModel vm = MapToViewModel(model);
            return vm;
        }

        public ListResult<MaterialDeliveryNoteWeavingViewModel> ReadByKeyword(string keyword, string order, int page, int size, string filter)
        {
            var MaterialDeliveryNoteQuery = _MaterialDeliveryNoteWeavingRepository.ReadAll();

            var joinQuery = from MaterialDeliveryNoteWeaving in MaterialDeliveryNoteQuery
                            select new MaterialDeliveryNoteWeavingViewModel()
                            {
                                Id = MaterialDeliveryNoteWeaving.Id,
                                Code = MaterialDeliveryNoteWeaving.Code,
                                DateSJ = MaterialDeliveryNoteWeaving.DateSJ,
                                Buyer = new BuyerMaterialDeliveryNoteWeaving()
                                {
                                    Name = MaterialDeliveryNoteWeaving.BuyerName,
                                },
                                SendTo = MaterialDeliveryNoteWeaving.SendTo
                            };

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                joinQuery = joinQuery.Where(entity => entity.Code.Contains(keyword));
            }

            if (string.IsNullOrWhiteSpace(order))
            {
                order = "{}";
            }
            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            joinQuery = QueryHelper<MaterialDeliveryNoteWeavingViewModel>.Order(joinQuery, orderDictionary);

            var data = joinQuery.Skip((page - 1) * size).Take(size).ToList();
            var totalRow = joinQuery.Select(entity => entity.Id).Count();

            return new ListResult<MaterialDeliveryNoteWeavingViewModel>(data, page, size, totalRow);
        }

        public async Task Update(int id, MaterialDeliveryNoteWeavingViewModel viewModel)
        {
            var model = new Data.MaterialDeliveryNoteWeavingModel(viewModel.Code, viewModel.DateSJ.GetValueOrDefault(), viewModel.selectedDO.Id.GetValueOrDefault(), viewModel.selectedDO.No, viewModel.SendTo, viewModel.Unit.Id.GetValueOrDefault(), viewModel.Unit.Name,
                                                                viewModel.Buyer.Id.GetValueOrDefault(), viewModel.Buyer.Code, viewModel.Buyer.Name, viewModel.NumberBonOut, viewModel.Storage.Id, viewModel.Storage.Code, viewModel.Storage.Name,
                                                                viewModel.Remark,
                                                                viewModel.ItemsMaterialDeliveryNoteWeaving.Select(s => new ItemsMaterialDeliveryNoteWeavingModel(s.itemNoSOP, s.itemMaterialName, s.itemGrade, s.itemType, s.itemCode, s.inputBale.GetValueOrDefault(), s.inputPiece.GetValueOrDefault(), s.inputMeter.GetValueOrDefault(), s.inputKg.GetValueOrDefault())).ToList());

            await _MaterialDeliveryNoteWeavingRepository.UpdateAsync(id, model);
        }
    }
}
