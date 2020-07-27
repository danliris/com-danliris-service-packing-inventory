using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalReturnNote;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalReturnNote
{
    public class GarmentShippingLocalReturnNoteService : IGarmentShippingLocalReturnNoteService
    {
        private readonly IGarmentShippingLocalReturnNoteRepository _repository;
        private readonly IServiceProvider _serviceProvider;

        public GarmentShippingLocalReturnNoteService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentShippingLocalReturnNoteRepository>();
            _serviceProvider = serviceProvider;
        }

        private GarmentShippingLocalReturnNoteViewModel MapToViewModel(GarmentShippingLocalReturnNoteModel model)
        {
            GarmentShippingLocalReturnNoteViewModel viewModel = new GarmentShippingLocalReturnNoteViewModel
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

                returnNoteNo = model.ReturnNoteNo,
                returnDate = model.ReturnDate,
                description=model.Description,
                salesNote = new GarmentShippingLocalSalesNoteViewModel
                {
                    Active = model.SalesNote.Active,
                    Id = model.SalesNote.Id,
                    CreatedAgent = model.SalesNote.CreatedAgent,
                    CreatedBy = model.SalesNote.CreatedBy,
                    CreatedUtc = model.SalesNote.CreatedUtc,
                    DeletedAgent = model.SalesNote.DeletedAgent,
                    DeletedBy = model.SalesNote.DeletedBy,
                    DeletedUtc = model.SalesNote.DeletedUtc,
                    IsDeleted = model.SalesNote.IsDeleted,
                    LastModifiedAgent = model.SalesNote.LastModifiedAgent,
                    LastModifiedBy = model.SalesNote.LastModifiedBy,
                    LastModifiedUtc = model.SalesNote.LastModifiedUtc,

                    noteNo = model.SalesNote.NoteNo,
                    date = model.SalesNote.Date,
                    transactionType = new TransactionType
                    {
                        id = model.SalesNote.TransactionTypeId,
                        code = model.SalesNote.TransactionTypeCode,
                        name = model.SalesNote.TransactionTypeName
                    },
                    buyer = new Buyer
                    {
                        Id = model.SalesNote.BuyerId,
                        Code = model.SalesNote.BuyerCode,
                        Name = model.SalesNote.BuyerName,
                        npwp = model.SalesNote.BuyerNPWP
                    },
                    tempo = model.SalesNote.Tempo,
                    dispositionNo = model.SalesNote.DispositionNo,
                    useVat = model.SalesNote.UseVat,
                    remark = model.SalesNote.Remark,
                    isUsed = model.SalesNote.IsUsed,
                },

                items = (model.Items ?? new List<GarmentShippingLocalReturnNoteItemModel>()).Select(i => new GarmentShippingLocalReturnNoteItemViewModel
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

                    isChecked = true,
                    returnQuantity = i.ReturnQuantity,
                    salesNoteItem = new GarmentShippingLocalSalesNoteItemViewModel
                    {
                        Active = i.SalesNoteItem.Active,
                        Id = i.SalesNoteItem.Id,
                        CreatedAgent = i.SalesNoteItem.CreatedAgent,
                        CreatedBy = i.SalesNoteItem.CreatedBy,
                        CreatedUtc = i.SalesNoteItem.CreatedUtc,
                        DeletedAgent = i.SalesNoteItem.DeletedAgent,
                        DeletedBy = i.SalesNoteItem.DeletedBy,
                        DeletedUtc = i.SalesNoteItem.DeletedUtc,
                        IsDeleted = i.SalesNoteItem.IsDeleted,
                        LastModifiedAgent = i.SalesNoteItem.LastModifiedAgent,
                        LastModifiedBy = i.SalesNoteItem.LastModifiedBy,
                        LastModifiedUtc = i.SalesNoteItem.LastModifiedUtc,

                        product = new ProductViewModel
                        {
                            id = i.SalesNoteItem.ProductId,
                            code = i.SalesNoteItem.ProductCode,
                            name = i.SalesNoteItem.ProductName
                        },
                        quantity = i.SalesNoteItem.Quantity,
                        uom = new UnitOfMeasurement
                        {
                            Id = i.SalesNoteItem.UomId,
                            Unit = i.SalesNoteItem.UomUnit
                        },
                        price = i.SalesNoteItem.Price
                    }

                }).ToList()
            };

            return viewModel;
        }

        private GarmentShippingLocalReturnNoteModel MapToModel(GarmentShippingLocalReturnNoteViewModel vm)
        {
            var items = (vm.items ?? new List<GarmentShippingLocalReturnNoteItemViewModel>())
                .Where(w => w.isChecked)
                .Select(i =>
                {
                    i.salesNoteItem = i.salesNoteItem ?? new GarmentShippingLocalSalesNoteItemViewModel();
                    return new GarmentShippingLocalReturnNoteItemModel(i.salesNoteItem.Id,null, i.returnQuantity) { Id = i.Id };
                }).ToList();

            vm.salesNote = vm.salesNote ?? new GarmentShippingLocalSalesNoteViewModel();
            return new GarmentShippingLocalReturnNoteModel(GenerateNo(), vm.salesNote.Id, vm.returnDate.GetValueOrDefault(),vm.description,null, items) { Id = vm.Id };
        }

        private string GenerateNo()
        {
            var year = DateTime.Now.ToString("yy");

            var prefix = $"{year}/RTR/";

            var lastInvoiceNo = _repository.ReadAll().Where(w => w.ReturnNoteNo.StartsWith(prefix))
                .OrderByDescending(o => o.ReturnNoteNo)
                .Select(s => int.Parse(s.ReturnNoteNo.Replace(prefix, "")))
                .FirstOrDefault();
            var invoiceNo = $"{prefix}{(lastInvoiceNo + 1).ToString("D5")}";

            return invoiceNo;
        }

        public async Task<int> Create(GarmentShippingLocalReturnNoteViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            int Created = await _repository.InsertAsync(model);

            return Created;
        }

        public async Task<int> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "ReturnNoteNo","SalesNote.NoteNo", "SalesNote.BuyerCode", "SalesNote.BuyerName","SalesNote.DispositionNo"
            };
            query = QueryHelper<GarmentShippingLocalReturnNoteModel>.Search(query, SearchAttributes, keyword, ignoreDot: true);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingLocalReturnNoteModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingLocalReturnNoteModel>.Order(query, OrderDictionary, ignoreDot: true);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => new IndexViewModel
                {
                    id = model.Id,
                    returnNoteNo = model.ReturnNoteNo,
                    returnDate=model.ReturnDate,
                    salesNote = new SalesNote
                    {
                        id = model.SalesNote.Id,
                        noteNo = model.SalesNote.NoteNo,
                        buyer = new Buyer
                        {
                            Code = model.SalesNote.BuyerCode,
                            Name = model.SalesNote.BuyerName
                        },
                        date = model.SalesNote.Date,
                        tempo = model.SalesNote.Tempo,
                        dispositionNo = model.SalesNote.DispositionNo
                    }
                })
                .ToList();

            return new ListResult<IndexViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingLocalReturnNoteViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public Buyer GetBuyer(int id)
        {
            string buyerUri = "master/garment-leftover-warehouse-buyers";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));

            var response = httpClient.GetAsync($"{APIEndpoint.Core}{buyerUri}/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                Buyer viewModel = JsonConvert.DeserializeObject<Buyer>(result.GetValueOrDefault("data").ToString());
                return viewModel;
            }
            else
            {
                return null;
            }
        }
    }
}
