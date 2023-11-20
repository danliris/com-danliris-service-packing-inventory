using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ExportSalesDO
{
    public class GarmentShippingExportSalesDOService : IGarmentShippingExportSalesDOService
    {
        private readonly IGarmentShippingExportSalesDORepository _repository;
        protected readonly ILogHistoryRepository logHistoryRepository;
        public GarmentShippingExportSalesDOService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentShippingExportSalesDORepository>();
            logHistoryRepository = serviceProvider.GetService<ILogHistoryRepository>();
        }

        private GarmentShippingExportSalesDOViewModel MapToViewModel(GarmentShippingExportSalesDOModel model)
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

                invoiceNo = model.InvoiceNo,
                buyerAgent = new Buyer
                {
                    Id = model.BuyerAgentId,
                    Code = model.BuyerAgentCode,
                    Name = model.BuyerAgentName,
                },
                date=model.Date,
                exportSalesDONo=model.ExportSalesDONo,
                packingListId=model.PackingListId,
                to=model.To,
                shipmentMode=model.ShipmentMode,
                deliverTo=model.DeliverTo,
                remark=model.Remark,
                unit=new Unit
                {
                    Id = model.UnitId,
                    Code = model.UnitCode,
                    Name = model.UnitName,
                },
                items = model.Items.Select(i => new GarmentShippingExportSalesDOItemViewModel
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

                    comodity = new Comodity
                    {
                        Id = i.ComodityId,
                        Code = i.ComodityCode,
                        Name = i.ComodityName
                    },
                    description = i.Description,
                    quantity = i.Quantity,
                    uom = new UnitOfMeasurement
                    {
                        Id = i.UomId,
                        Unit = i.UomUnit
                    },
                    cartonQuantity=i.CartonQuantity,
                    exportSalesDOId=i.ExportSalesDOId,
                    grossWeight=i.GrossWeight,
                    nettWeight=i.NettWeight,
                    volume=i.Volume
                    
                }).ToList()
            };
            return vm;
        }

        private GarmentShippingExportSalesDOModel MapToModel(GarmentShippingExportSalesDOViewModel viewModel)
        {
            var items = (viewModel.items ?? new List<GarmentShippingExportSalesDOItemViewModel>()).Select(i =>
            {
                i.uom = i.uom ?? new UnitOfMeasurement();
                i.comodity = i.comodity ?? new Comodity();
                return new GarmentShippingExportSalesDOItemModel(i.comodity.Id,i.comodity.Code, i.comodity.Name,i.description, i.quantity,i.uom.Id.GetValueOrDefault(),i.uom.Unit,i.cartonQuantity,i.grossWeight,i.nettWeight,i.volume)
                {
                    Id = i.Id
                };
            }).ToList();

            viewModel.unit = viewModel.unit ?? new Unit();
            viewModel.buyerAgent = viewModel.buyerAgent ?? new Buyer();
            viewModel.exportSalesDONo = GenerateNo(viewModel);
            GarmentShippingExportSalesDOModel garmentPackingListModel = new GarmentShippingExportSalesDOModel(viewModel.exportSalesDONo,viewModel.invoiceNo, viewModel.packingListId, viewModel.date, viewModel.buyerAgent.Id, viewModel.buyerAgent.Code, viewModel.buyerAgent.Name, viewModel.to, viewModel.unit.Name, viewModel.unit.Id, viewModel.unit.Code,viewModel.shipmentMode,viewModel.deliverTo,viewModel.remark, items);

            return garmentPackingListModel;
        }

        private string GenerateNo(GarmentShippingExportSalesDOViewModel viewModel)
        {
            var year = DateTime.Now.ToString("yy");

            var prefix = $"DL/DOPE/{year}";

            var lastNo = _repository.ReadAll().Where(w => w.ExportSalesDONo.StartsWith(prefix))
                .OrderByDescending(o => o.ExportSalesDONo)
                .Select(s => int.Parse(s.ExportSalesDONo.Replace(prefix, "")))
                .FirstOrDefault();
            var invoiceNo = $"{prefix}{(lastNo + 1).ToString("D5")}";

            return invoiceNo;
        }

        public async Task<int> Create(GarmentShippingExportSalesDOViewModel viewModel)
        {
            GarmentShippingExportSalesDOModel garmentShippingExportSalesDOModel = MapToModel(viewModel);

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

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "InvoiceNo", "BuyerAgentName", "UnitName", "ExportSalesDONo", "To"
            };
            query = QueryHelper<GarmentShippingExportSalesDOModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingExportSalesDOModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingExportSalesDOModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => new IndexViewModel
                {
                    exportSalesDONo=model.ExportSalesDONo,
                    buyerAgent = new Buyer
                    {
                        Id = model.BuyerAgentId,
                        Code = model.BuyerAgentCode,
                        Name = model.BuyerAgentName,
                    },
                    date = model.Date,
                    to = model.To,
                    unit = new Unit
                    {
                        Id = model.UnitId,
                        Code = model.UnitCode,
                        Name = model.UnitName,
                    },
                    id=model.Id,
                    invoiceNo=model.InvoiceNo
                })
                .ToList();

            return new ListResult<IndexViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingExportSalesDOViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentShippingExportSalesDOViewModel viewModel)
        {
            GarmentShippingExportSalesDOModel garmentShippingExportSalesDOModel = MapToModel(viewModel);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Update DO Penjualan Export - " + garmentShippingExportSalesDOModel.ExportSalesDONo);

            return await _repository.UpdateAsync(id, garmentShippingExportSalesDOModel);
        }
    }
}
