using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.SalesExport;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.SalesExport;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.SalesExport
{
    public class GarmentShippingExportSalesDOService : IGarmentShippingExportSalesDOService
    {
        private readonly IGarmentShippingLeftOverExportSalesDORepository _repository;
        protected readonly ILogHistoryRepository logHistoryRepository;
        public GarmentShippingExportSalesDOService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentShippingLeftOverExportSalesDORepository>();
            logHistoryRepository = serviceProvider.GetService<ILogHistoryRepository>();
        }

        private GarmentShippingExportSalesDOViewModel MapToViewModel(GarmentShippingLeftOverExportSalesDOModel model)
        {
            var vm = new GarmentShippingExportSalesDOViewModel()
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

                exportSalesDONo = model.ExportSalesDONo,

                buyer = new Buyer
                {
                    Id = model.BuyerId,
                    Code = model.BuyerCode,
                    Name = model.BuyerName,
                },
                date = model.Date,
                exportSalesNoteNo = model.ExportSalesNoteNo,
                exportSalesNoteId = model.ExportSalesNoteId,
                to = model.To,
                storageDivision = model.StorageDivision,
                remark = model.Remark,
                items = model.Items == null ? new List<GarmentShippingExportSalesDOItemViewModel>() : model.Items.Select(i => new GarmentShippingExportSalesDOItemViewModel
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
                    exportSalesDOId = i.ExportSalesDOId,
                    grossWeight = i.GrossWeight,
                    nettWeight = i.NettWeight,
                    exportSalesNoteItemId=i.ExportSalesNoteItemId

                }).ToList()
            };
            return vm;
        }

        private GarmentShippingLeftOverExportSalesDOModel MapToModel(GarmentShippingExportSalesDOViewModel viewModel)
        {
            var items = (viewModel.items ?? new List<GarmentShippingExportSalesDOItemViewModel>()).Select(i =>
            {
                i.uom = i.uom ?? new UnitOfMeasurement();
                i.product = i.product ?? new ProductViewModel();
                return new GarmentShippingLeftOverExportSalesDOItemModel(i.exportSalesDOId, i.exportSalesNoteItemId, i.product.id, i.product.code, i.product.name, i.description, i.quantity, i.uom.Id.GetValueOrDefault(), i.uom.Unit, i.packQuantity, i.packUom.Id.GetValueOrDefault(), i.packUom.Unit, i.grossWeight, i.nettWeight)
                {
                    Id = i.Id
                };
            }).ToList();

            viewModel.buyer = viewModel.buyer ?? new Buyer();
            viewModel.exportSalesDONo = GenerateNo(viewModel);
            GarmentShippingLeftOverExportSalesDOModel garmentPackingListModel = new GarmentShippingLeftOverExportSalesDOModel(viewModel.exportSalesDONo, viewModel.exportSalesNoteNo, viewModel.exportSalesNoteId, viewModel.date, viewModel.buyer.Id, viewModel.buyer.Code, viewModel.buyer.Name, viewModel.to, viewModel.storageDivision, viewModel.remark, items);

            return garmentPackingListModel;
        }

        private string GenerateNo(GarmentShippingExportSalesDOViewModel viewModel)
        {
            var year = DateTime.Now.ToString("yy");

            var prefix = $"DL/DOPL/{year}";

            var lastNo = _repository.ReadAll().Where(w => w.ExportSalesDONo.StartsWith(prefix))
                .OrderByDescending(o => o.ExportSalesDONo)
                .Select(s => int.Parse(s.ExportSalesDONo.Replace(prefix, "")))
                .FirstOrDefault();
            var invoiceNo = $"{prefix}{(lastNo + 1).ToString("D5")}";

            return invoiceNo;
        }

        public async Task<int> Create(GarmentShippingExportSalesDOViewModel viewModel)
        {
            GarmentShippingLeftOverExportSalesDOModel garmentShippingExportSalesDOModel = MapToModel(viewModel);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Create DO Penjualan Export - " + garmentShippingExportSalesDOModel.ExportSalesDONo);

            int Created = await _repository.InsertAsync(garmentShippingExportSalesDOModel);

            return Created;
        }

        public async Task<int> Delete(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Delete DO Penjualan Export - " + data.ExportSalesDONo);

            return await _repository.DeleteAsync(id);
        }

        public ListResult<GarmentShippingExportSalesDOViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "ExportSalesDONo", "BuyerName", "StorageDivision", "To","ExportSalesNoteNo", "BuyerCode"
            };
            query = QueryHelper<GarmentShippingLeftOverExportSalesDOModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingLeftOverExportSalesDOModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingLeftOverExportSalesDOModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentShippingExportSalesDOViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingExportSalesDOViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentShippingExportSalesDOViewModel viewModel)
        {
            GarmentShippingLeftOverExportSalesDOModel garmentShippingExportSalesDOModel = MapToModel(viewModel);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Update DO Penjualan Export - " + garmentShippingExportSalesDOModel.ExportSalesDONo);

            return await _repository.UpdateAsync(id, garmentShippingExportSalesDOModel);
        }
    }
}
