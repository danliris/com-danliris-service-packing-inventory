using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentMD;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.LocalSalesDO
{
    public class GarmentMDLocalSalesDOService : IGarmentMDLocalSalesDOService
    {
        private readonly IGarmentMDLocalSalesDORepository _repository;
        private readonly IServiceProvider serviceProvider;
        protected readonly ILogHistoryRepository logHistoryRepository;

        public GarmentMDLocalSalesDOService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentMDLocalSalesDORepository>();
            logHistoryRepository = serviceProvider.GetService<ILogHistoryRepository>();
            this.serviceProvider = serviceProvider;
        }

        private string GenerateNo(GarmentMDLocalSalesDOViewModel viewModel)
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

        private GarmentMDLocalSalesDOViewModel MapToViewModel(GarmentMDLocalSalesDOModel model)
        {
            var vm = new GarmentMDLocalSalesDOViewModel()
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
                localSalesContractNo = model.LocalSalesContractNo,
                localSalesContractId = model.LocalSalesContractId,
                to = model.To,
                storageDivision = model.StorageDivision,
                remark = model.Remark,

                comodityName = model.ComodityName,
                description = model.Description,
                quantity = model.Quantity,
                uom = new UnitOfMeasurement
                {
                    Id = model.UomId,
                    Unit = model.UomUnit
                },
                packQuantity = model.PackQuantity,
                packUom = new UnitOfMeasurement
                {
                    Id = model.PackUomId,
                    Unit = model.PackUomUnit
                },
                grossWeight = model.GrossWeight,
                nettWeight = model.NettWeight,

            };

            return vm;
        }

        private GarmentMDLocalSalesDOModel MapToModel(GarmentMDLocalSalesDOViewModel viewModel)
        {
            viewModel.uom = viewModel.uom ?? new UnitOfMeasurement();
            viewModel.packUom = viewModel.packUom ?? new UnitOfMeasurement();
            viewModel.buyer = viewModel.buyer ?? new Buyer();
            viewModel.localSalesDONo = GenerateNo(viewModel);
            GarmentMDLocalSalesDOModel garmentPackingListModel = 
                new GarmentMDLocalSalesDOModel(
                    viewModel.localSalesDONo, 
                    viewModel.localSalesContractNo, 
                    viewModel.localSalesContractId, 
                    viewModel.date, 
                    viewModel.buyer.Id, 
                    viewModel.buyer.Code, 
                    viewModel.buyer.Name, 
                    viewModel.to, 
                    viewModel.storageDivision, 
                    viewModel.remark,

                    viewModel.comodityName,
                    viewModel.description,
                    viewModel.quantity,
                    viewModel.uom.Id.GetValueOrDefault(),
                    viewModel.uom.Unit,
                    viewModel.packQuantity,
                    viewModel.packUom.Id.GetValueOrDefault(),
                    viewModel.packUom.Unit,
                    viewModel.grossWeight,
                    viewModel.nettWeight
                );

            return garmentPackingListModel;
        }

        public async Task<int> Create(GarmentMDLocalSalesDOViewModel viewModel)
        {
            GarmentMDLocalSalesDOModel garmentShippingLocalSalesDOModel = MapToModel(viewModel);

            //Add Log History
            await logHistoryRepository.InsertAsync("MERCHANDISER", "Create DO Penjualan Lokal - " + garmentShippingLocalSalesDOModel.LocalSalesDONo);

            int Created = await _repository.InsertAsync(garmentShippingLocalSalesDOModel);

            return Created;
        }

        public async Task<int> Delete(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            //Add Log History
            await logHistoryRepository.InsertAsync("MERCHANDISER", "Delete DO Penjualan Lokal - " + data.LocalSalesDONo);

            return await _repository.DeleteAsync(id);
        }

        public ListResult<GarmentMDLocalSalesDOViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "LocalSalesDONo", "BuyerName", "StorageDivision", "To", "LocalSalesContractNo", "BuyerCode"
            };

            query = QueryHelper<GarmentMDLocalSalesDOModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentMDLocalSalesDOModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentMDLocalSalesDOModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentMDLocalSalesDOViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentMDLocalSalesDOViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentMDLocalSalesDOViewModel viewModel)
        {
            GarmentMDLocalSalesDOModel garmentShippingLocalSalesDOModel = MapToModel(viewModel);

            //Add Log History
            await logHistoryRepository.InsertAsync("MERCHANDISER", "Update DO Penjualan Lokal - " + garmentShippingLocalSalesDOModel.LocalSalesDONo);

            return await _repository.UpdateAsync(id, garmentShippingLocalSalesDOModel);
        }
    }
}
