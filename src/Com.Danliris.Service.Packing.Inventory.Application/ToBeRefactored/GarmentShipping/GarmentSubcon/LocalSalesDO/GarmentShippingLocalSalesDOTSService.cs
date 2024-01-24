using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesDOTS;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesDOTS;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesDOTS
{
    public class GarmentShippingLocalSalesDOTSService : IGarmentShippingLocalSalesDOTSService
    {
        private readonly IGarmentShippingLocalSalesDOTSRepository _repository;
        protected readonly ILogHistoryRepository logHistoryRepository;

        private readonly PackingInventoryDbContext dbContext;
        public GarmentShippingLocalSalesDOTSService(IServiceProvider serviceProvider, PackingInventoryDbContext dbContext)
        {
            _repository = serviceProvider.GetService<IGarmentShippingLocalSalesDOTSRepository>();
            logHistoryRepository = serviceProvider.GetService<ILogHistoryRepository>();

            this.dbContext = dbContext;
        }

        private GarmentShippingLocalSalesDOTSViewModel MapToViewModel(GarmentShippingLocalSalesDOTSModel model)
        {
            var vm = new GarmentShippingLocalSalesDOTSViewModel()
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
                items = model.Items == null ? new List<GarmentShippingLocalSalesDOTSItemViewModel>() : model.Items.Select(i => new GarmentShippingLocalSalesDOTSItemViewModel
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

                    invoiceNo = i.InvoiceNo,
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

        private GarmentShippingLocalSalesDOTSModel MapToModel(GarmentShippingLocalSalesDOTSViewModel viewModel)
        {
            var items = (viewModel.items ?? new List<GarmentShippingLocalSalesDOTSItemViewModel>()).Select(i =>
            {
                i.uom = i.uom ?? new UnitOfMeasurement();
                return new GarmentShippingLocalSalesDOTSItemModel(i.localSalesDOId, i.localSalesNoteItemId, /*i.product.id, i.product.code, i.product.name,*/ i.description, i.quantity, i.uom.Id.GetValueOrDefault(), i.uom.Unit, i.packQuantity, i.packUom.Id.GetValueOrDefault(), i.packUom.Unit, i.grossWeight, i.nettWeight,i.invoiceNo)
                {
                    Id = i.Id
                };
            }).ToList();

            viewModel.buyer = viewModel.buyer ?? new Buyer();
            viewModel.localSalesDONo = GenerateNo(viewModel);
            GarmentShippingLocalSalesDOTSModel garmentPackingListModel = new GarmentShippingLocalSalesDOTSModel(viewModel.localSalesDONo, viewModel.localSalesNoteNo, viewModel.localSalesNoteId, viewModel.date, viewModel.buyer.Id, viewModel.buyer.Code, viewModel.buyer.Name, viewModel.to, viewModel.storageDivision, viewModel.remark, items);

            return garmentPackingListModel;
        }

        private string GenerateNo(GarmentShippingLocalSalesDOTSViewModel viewModel)
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

        public async Task<int> Create(GarmentShippingLocalSalesDOTSViewModel viewModel)
        {
            var Created = 0;
            using (var transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    GarmentShippingLocalSalesDOTSModel GarmentShippingLocalSalesDOTSModel = MapToModel(viewModel);

                    //Add Log History
                    Created += await logHistoryRepository.InsertAsync("SHIPPING", "Create DO Penjualan Lokal Terima Subkon - " + GarmentShippingLocalSalesDOTSModel.LocalSalesDONo);

                    Created += await _repository.InsertAsync(GarmentShippingLocalSalesDOTSModel);

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return Created;

           
        }

        public async Task<int> Delete(int id)
        {
            var Deleted = 0;
            using (var transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    var data = await _repository.ReadByIdAsync(id);

                    //Add Log History
                    Deleted += await logHistoryRepository.InsertAsync("SHIPPING", "Delete DO Penjualan Lokal Terima Subkon  - " + data.LocalSalesDONo);

                    Deleted += await _repository.DeleteAsync(id);

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return Deleted;

           
        }

        public ListResult<GarmentShippingLocalSalesDOTSViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "LocalSalesDONo", "BuyerName", "StorageDivision", "To","LocalSalesNoteNo", "BuyerCode"
            };
            query = QueryHelper<GarmentShippingLocalSalesDOTSModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingLocalSalesDOTSModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingLocalSalesDOTSModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentShippingLocalSalesDOTSViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingLocalSalesDOTSViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentShippingLocalSalesDOTSViewModel viewModel)
        {
            var Updated = 0;
            using (var transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    GarmentShippingLocalSalesDOTSModel GarmentShippingLocalSalesDOTSModel = MapToModel(viewModel);

                    //Add Log History
                    Updated +=  await logHistoryRepository.InsertAsync("SHIPPING", "Update DO Penjualan Lokal Terima Subkon  - " + GarmentShippingLocalSalesDOTSModel.LocalSalesDONo);

                    Updated += await _repository.UpdateAsync(id, GarmentShippingLocalSalesDOTSModel); 
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return Updated;

           
        }
    }
}
