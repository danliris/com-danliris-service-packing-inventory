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
                selectedDO = new DeliveryOrderSales()
                {
                    Id = model.DoSalesNumberId,
                    No = model.DoSalesNumber
                },
                SendTo = model.SendTo,
                Unit = new Unit()
                {
                    Id = model.UnitId,
                    Name = model.UnitName
                },
                Buyer = new Buyer()
                {
                    Id = model.BuyerId,
                    Code = model.Code,
                    Name = model.BuyerName
                },
                NumberBonOut = model.NumberOut,
                Storage = new Storage()
                {
                    Id = model.StorageId,
                    Code = model.StorageCode,
                    Name = model.StorageName
                },
                UnitLength = model.UnitLength,
                UnitPacking = model.UnitPacking,
                Remark = model.Remark,
                ItemsMaterialDeliveryNoteWeaving = model.ItemsMaterialDeliveryNoteWeaving.Select(d => new ItemsMaterialDeliveryNoteWeavingViewModel()
                {
                    ItemNoSPP = d.ItemNoSPP,
                    ItemMaterialName = d.ItemMaterialName,
                    ItemDesign = d.ItemDesign,
                    ItemType = d.ItemType,
                    ItemCode = d.ItemCode,
                    InputPacking = d.InputPacking,
                    Length = d.Length,
                    InputConversion = d.InputConversion
                }).ToList()
            };

            return vm;
        }

        public async Task Create(MaterialDeliveryNoteWeavingViewModel viewModel)
        {
            var model = new Data.MaterialDeliveryNoteWeavingModel(null, viewModel.DateSJ, viewModel.selectedDO.Id, viewModel.selectedDO.No, viewModel.SendTo, viewModel.Unit.Id, viewModel.Unit.Name,
                                                                viewModel.Buyer.Id, viewModel.Buyer.Code, viewModel.Buyer.Name, viewModel.NumberBonOut, viewModel.Storage.Id, viewModel.Storage.Code, viewModel.Storage.Name,
                                                                viewModel.UnitLength, viewModel.UnitPacking, viewModel.Remark,
                                                                viewModel.ItemsMaterialDeliveryNoteWeaving.Select(s => new ItemsMaterialDeliveryNoteWeavingModel(s.ItemNoSPP, s.ItemMaterialName, s.ItemDesign, s.ItemType, s.ItemCode, s.InputPacking, s.Length, s.InputConversion)).ToList());

            foreach (var itm in viewModel.ItemsMaterialDeliveryNoteWeaving)
            {
                var modelItem = new ItemsMaterialDeliveryNoteWeavingModel(itm.ItemNoSPP, itm.ItemMaterialName, itm.ItemDesign, itm.ItemType, itm.ItemCode, itm.InputPacking, itm.Length, itm.InputConversion);

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
                                Buyer = new Buyer()
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
            var model = new Data.MaterialDeliveryNoteWeavingModel(viewModel.Code, viewModel.DateSJ, viewModel.selectedDO.Id, viewModel.selectedDO.No, viewModel.SendTo, viewModel.Unit.Id, viewModel.Unit.Name,
                                                                viewModel.Buyer.Id, viewModel.Buyer.Code, viewModel.Buyer.Name, viewModel.NumberBonOut, viewModel.Storage.Id, viewModel.Storage.Code, viewModel.Storage.Name,
                                                                viewModel.UnitLength, viewModel.UnitPacking, viewModel.Remark,
                                                                viewModel.ItemsMaterialDeliveryNoteWeaving.Select(s => new ItemsMaterialDeliveryNoteWeavingModel(s.ItemNoSPP, s.ItemMaterialName, s.ItemDesign, s.ItemType, s.ItemCode, s.InputPacking, s.Length, s.InputConversion)).ToList());

            await _MaterialDeliveryNoteWeavingRepository.UpdateAsync(id, model);
        }
    }
}
