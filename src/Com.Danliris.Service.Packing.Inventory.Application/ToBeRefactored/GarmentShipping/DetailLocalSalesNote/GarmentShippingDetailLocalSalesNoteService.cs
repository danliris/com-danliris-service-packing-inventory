using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.DetailShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.DetailShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.DetailLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.DetailLocalSalesNote
{
    public class GarmentShippingDetailLocalSalesNoteService : IGarmentShippingDetailLocalSalesNoteService
    {
        private readonly IGarmentShippingDetailLocalSalesNoteRepository _repository;
        private readonly IServiceProvider serviceProvider;
        protected readonly ILogHistoryRepository logHistoryRepository;
        public GarmentShippingDetailLocalSalesNoteService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentShippingDetailLocalSalesNoteRepository>();
            logHistoryRepository = serviceProvider.GetService<ILogHistoryRepository>();
            this.serviceProvider = serviceProvider;
        }

        private GarmentShippingDetailLocalSalesNoteViewModel MapToViewModel(GarmentShippingDetailLocalSalesNoteModel model)
        {
            GarmentShippingDetailLocalSalesNoteViewModel viewModel = new GarmentShippingDetailLocalSalesNoteViewModel
            {
                Active = model.Active,
                Id = model.Id,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,

                localSalesNoteId = model.LocalSalesNoteId,
                noteNo = model.NoteNo,
                date = model.Date,
                transactionType = new TransactionType
                {
                    id = model.TransactionTypeId,
                    code = model.TransactionTypeCode,
                    name = model.TransactionTypeName
                },              
                buyer = new Buyer
                {
                    Id = model.BuyerId,
                    Code = model.BuyerCode,
                    Name = model.BuyerName                 
                },               
                localSalesContractId=model.LocalSalesContractId,
                salesContractNo=model.SalesContractNo,
                amount = model.Amount,                
                items = (model.Items ?? new List<GarmentShippingDetailLocalSalesNoteItemModel>()).Select(i => new GarmentShippingDetailLocalSalesNoteItemViewModel
                {
                    Active = i.Active,
                    Id = i.Id,
                    CreatedAgent = i.CreatedAgent,
                    CreatedBy = i.CreatedBy,
                    CreatedUtc = i.CreatedUtc,
                    DeletedAgent = i.DeletedAgent,
                    DeletedBy = i.DeletedBy,
                    DeletedUtc = i.DeletedUtc,
                    IsDeleted = i.IsDeleted,
                    LastModifiedAgent = i.LastModifiedAgent,
                    LastModifiedBy = i.LastModifiedBy,
                    LastModifiedUtc = i.LastModifiedUtc,

                    Unit = new Unit
                    {
                        Id = i.UnitId,
                        Code = i.UnitCode,
                        Name = i.UnitName
                    },
                    quantity = i.Quantity,
                    uom = new UnitOfMeasurement
                    {
                        Id = i.UomId,
                        Unit = i.UomUnit
                    },
                    amount = i.Amount,
                }).ToList()
            };

            return viewModel;
        }

        private GarmentShippingDetailLocalSalesNoteModel MapToModel(GarmentShippingDetailLocalSalesNoteViewModel vm)
        {
            var items = (vm.items ?? new List<GarmentShippingDetailLocalSalesNoteItemViewModel>()).Select(i =>
            {
                i.Unit = i.Unit ?? new Unit();
                i.uom = i.uom ?? new UnitOfMeasurement();             
                return new GarmentShippingDetailLocalSalesNoteItemModel(i.detaiLocalSalesNoteId, i.Unit.Id, i.Unit.Code, i.Unit.Name, i.quantity, i.uom.Id.GetValueOrDefault(), i.uom.Unit, i.amount) { Id = i.Id };
            }).ToList();

            vm.transactionType = vm.transactionType ?? new TransactionType();
            vm.buyer = vm.buyer ?? new Buyer();
            
            return new GarmentShippingDetailLocalSalesNoteModel(vm.salesContractNo, vm.localSalesContractId, vm.localSalesNoteId, vm.noteNo, vm.date.GetValueOrDefault(), vm.transactionType.id, vm.transactionType.code, vm.transactionType.name, vm.buyer.Id, vm.buyer.Code, vm.buyer.Name, vm.amount, items) { Id = vm.Id };
        }

        public async Task<int> Create(GarmentShippingDetailLocalSalesNoteViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Create Detail Nota Penjualan Lokal - " + model.NoteNo);

            int Created = await _repository.InsertAsync(model);

            return Created;
        }

        public async Task<int> Delete(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Delete Detail Nota Penjualan Lokal - " + data.NoteNo);

            return await _repository.DeleteAsync(id);
        }

        public ListResult<GarmentShippingDetailLocalSalesNoteViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "NoteNo", "BuyerCode", "BuyerName"
            };
            query = QueryHelper<GarmentShippingDetailLocalSalesNoteModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingDetailLocalSalesNoteModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingDetailLocalSalesNoteModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentShippingDetailLocalSalesNoteViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingDetailLocalSalesNoteViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentShippingDetailLocalSalesNoteViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Update Detail Nota Penjualan Lokal - " + model.NoteNo);

            return await _repository.UpdateAsync(id, model);
        }

    }
}
