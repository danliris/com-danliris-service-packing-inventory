using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesDO
{
    public class GarmentShippingLocalSalesDOService : IGarmentShippingLocalSalesDOService
    {
        private readonly IGarmentShippingLocalSalesDORepository _repository;

        public GarmentShippingLocalSalesDOService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentShippingLocalSalesDORepository>();

        }

        private GarmentShippingLocalSalesDOViewModel MapToViewModel(GarmentShippingLocalSalesDOModel model)
        {
            var vm = new GarmentShippingLocalSalesDOViewModel()
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

                localSalesDONo = model.LocalSalesDONo,

                buyer = new Buyer
                {
                    Id = model.BuyerId,
                    Code = model.BuyerCode,
                    Name = model.BuyerName,
                },
                date = model.Date,
                localSalesNoteNo = model.LocalSalesNoteNo,
                localSalesNoteId = model.LocalSalesNoteId,
                to = model.To,
                storageDivision = model.StorageDivision,
                remark = model.Remark,
                items = model.Items == null ? new List<GarmentShippingLocalSalesDOItemViewModel>() : model.Items.Select(i => new GarmentShippingLocalSalesDOItemViewModel
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

                    product = new ProductViewModel
                    {
                        id = i.ProductId,
                        code = i.ProductCode,
                        name = i.ProductName
                    },
                    description = i.Description,
                    quantity = i.Quantity,
                    uom = new UnitOfMeasurement
                    {
                        Id = i.UomId,
                        Unit = i.UomUnit
                    },
                    packQuantity = i.PackQuantity,
                    packUom = new UnitOfMeasurement
                    {
                        Id = i.PackUomId,
                        Unit = i.PackUomUnit
                    },
                    localSalesDOId = i.LocalSalesDOId,
                    grossWeight = i.GrossWeight,
                    nettWeight = i.NettWeight,
                    localSalesNoteItemId=i.LocalSalesNoteItemId

                }).ToList()
            };
            return vm;
        }

        private GarmentShippingLocalSalesDOModel MapToModel(GarmentShippingLocalSalesDOViewModel viewModel)
        {
            var items = (viewModel.items ?? new List<GarmentShippingLocalSalesDOItemViewModel>()).Select(i =>
            {
                i.uom = i.uom ?? new UnitOfMeasurement();
                i.product = i.product ?? new ProductViewModel();
                return new GarmentShippingLocalSalesDOItemModel(i.localSalesDOId, i.localSalesNoteItemId, i.product.id, i.product.code, i.product.name, i.description, i.quantity, i.uom.Id.GetValueOrDefault(), i.uom.Unit, i.packQuantity, i.packUom.Id.GetValueOrDefault(), i.packUom.Unit, i.grossWeight, i.nettWeight)
                {
                    Id = i.Id
                };
            }).ToList();

            viewModel.buyer = viewModel.buyer ?? new Buyer();
            viewModel.localSalesDONo = GenerateNo(viewModel);
            GarmentShippingLocalSalesDOModel garmentPackingListModel = new GarmentShippingLocalSalesDOModel(viewModel.localSalesDONo, viewModel.localSalesNoteNo, viewModel.localSalesNoteId, viewModel.date, viewModel.buyer.Id, viewModel.buyer.Code, viewModel.buyer.Name, viewModel.to, viewModel.storageDivision, viewModel.remark, items);

            return garmentPackingListModel;
        }

        private string GenerateNo(GarmentShippingLocalSalesDOViewModel viewModel)
        {
            var year = DateTime.Now.ToString("yy");

            var prefix = $"DL/DOPL/{year}";

            var lastNo = _repository.ReadAll().Where(w => w.LocalSalesDONo.StartsWith(prefix))
                .OrderByDescending(o => o.LocalSalesDONo)
                .Select(s => int.Parse(s.LocalSalesDONo.Replace(prefix, "")))
                .FirstOrDefault();
            var invoiceNo = $"{prefix}{(lastNo + 1).ToString("D5")}";

            return invoiceNo;
        }

        public async Task<int> Create(GarmentShippingLocalSalesDOViewModel viewModel)
        {
            GarmentShippingLocalSalesDOModel garmentShippingLocalSalesDOModel = MapToModel(viewModel);

            int Created = await _repository.InsertAsync(garmentShippingLocalSalesDOModel);

            return Created;
        }

        public async Task<int> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public ListResult<GarmentShippingLocalSalesDOViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "LocalSalesDONo", "BuyerName", "StorageDivision", "To","LocalSalesNoteNo", "BuyerCode"
            };
            query = QueryHelper<GarmentShippingLocalSalesDOModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingLocalSalesDOModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingLocalSalesDOModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentShippingLocalSalesDOViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingLocalSalesDOViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentShippingLocalSalesDOViewModel viewModel)
        {
            GarmentShippingLocalSalesDOModel garmentShippingLocalSalesDOModel = MapToModel(viewModel);

            return await _repository.UpdateAsync(id, garmentShippingLocalSalesDOModel);
        }
    }
}
