using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingCostStructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingCostStructure;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingCostStructure
{
    public class GarmentShippingCostStructureService : IGarmentShippingCostStructureService
    {
        private const string UserAgent = "GarmentCostStructureService";

        protected readonly IIdentityProvider _identityProvider;
        protected readonly IGarmentShippingCostStructureRepository _garmentShippingCostStructureRepository;

        public GarmentShippingCostStructureService(IServiceProvider serviceProvider)
        {
           _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _garmentShippingCostStructureRepository = serviceProvider.GetService<IGarmentShippingCostStructureRepository>();
        }

        protected GarmentShippingCostStructureViewModel MapToViewModel(GarmentShippingCostStructureModel model)
        {
            var vm = new GarmentShippingCostStructureViewModel()
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

                InvoiceNo = model.InvoiceNo,
                PackingListId = model.PackingListId,
                Date = model.Date,
                Comodity = new Comodity
                {
                    Id = model.ComodityId,
                    Code = model.ComodityCode,
                    Name = model.ComodityName
                },
                HsCode = model.HsCode,
                Destination = model.Destination,
                //FabricTypeId = model.FabricTypeId,
                FabricType = model.FabricType,
                Amount = model.Amount,
                //Items = (model.Items ?? new List<GarmentShippingCostStructureItemModel>()).Select(i => new GarmentShippingCostStructureItemViewModel
                //{
                //    Active = i.Active,
                //    Id = i.Id,
                //    CreatedAgent = i.CreatedAgent,
                //    CreatedBy = i.CreatedBy,
                //    CreatedUtc = i.CreatedUtc,
                //    DeletedAgent = i.DeletedAgent,
                //    DeletedBy = i.DeletedBy,
                //    DeletedUtc = i.DeletedUtc,
                //    IsDeleted = i.IsDeleted,
                //    LastModifiedAgent = i.LastModifiedAgent,
                //    LastModifiedBy = i.LastModifiedBy,
                //    LastModifiedUtc = i.LastModifiedUtc,

                //    SummaryValue = i.SummaryValue,
                //    SummaryPercentage = i.SummaryPercentage,
                //    CostStructureType = i.CostStructureType,

                //    Details = (i.Details ?? new List<GarmentShippingCostStructureDetailModel>()).Select(d => new GarmentShippingCostStructureDetailViewModel
                //    {
                //        Active = d.Active,
                //        Id = d.Id,
                //        CreatedAgent = d.CreatedAgent,
                //        CreatedBy = d.CreatedBy,
                //        CreatedUtc = d.CreatedUtc,
                //        DeletedAgent = d.DeletedAgent,
                //        DeletedBy = d.DeletedBy,
                //        DeletedUtc = d.DeletedUtc,
                //        IsDeleted = d.IsDeleted,
                //        LastModifiedAgent = d.LastModifiedAgent,
                //        LastModifiedBy = d.LastModifiedBy,
                //        LastModifiedUtc = d.LastModifiedUtc,

                //        Description = d.Description,
                //        CountryFrom = d.CountryFrom,
                //        Percentage = d.Percentage,
                //        Value = d.Value
                //    }).ToList(),
                //}).ToList()
            };
            return vm;
        }

        protected GarmentShippingCostStructureModel MapToModel(GarmentShippingCostStructureViewModel viewModel)
        {
            viewModel.Comodity = viewModel.Comodity ?? new Comodity();
            GarmentShippingCostStructureModel garmentShippingCostStructureModel = new GarmentShippingCostStructureModel(viewModel.InvoiceNo, viewModel.Date, viewModel.Comodity.Id, viewModel.Comodity.Code, viewModel.Comodity.Name, viewModel.HsCode, viewModel.Destination, viewModel.FabricTypeId, viewModel.FabricType, viewModel.Amount, viewModel.PackingListId);
            return garmentShippingCostStructureModel;
        }

        public virtual async Task<int> Create(GarmentShippingCostStructureViewModel viewModel)
        {
            GarmentShippingCostStructureModel model = MapToModel(viewModel);

            int Created = await _garmentShippingCostStructureRepository.InsertAsync(model);

            return Created;
        }

        public async Task<int> Delete(int id)
        {
            return await _garmentShippingCostStructureRepository.DeleteAsync(id);
        }

        public ListResult<GarmentShippingCostStructureViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _garmentShippingCostStructureRepository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "InvoiceNo",
                "ComodityName",
                "HsCode",
                "Destination",
                "FabricType"
            };
            query = QueryHelper<GarmentShippingCostStructureModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingCostStructureModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingCostStructureModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentShippingCostStructureViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingCostStructureViewModel> ReadById(int id)
        {
            var data = await _garmentShippingCostStructureRepository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentShippingCostStructureViewModel viewModel)
        {
            GarmentShippingCostStructureModel garmentShippingCostStructure = MapToModel(viewModel);

            return await _garmentShippingCostStructureRepository.UpdateAsync(id, garmentShippingCostStructure);
        }

        public virtual async Task<MemoryStreamResult> ReadPdfById(int id)
        {
            var data = await _garmentShippingCostStructureRepository.ReadByIdAsync(id);

            var PdfTemplate = new GarmentShippingCostStructurePdfTemplate(_identityProvider);

            var viewModel = MapToViewModel(data);

            var stream = PdfTemplate.GeneratePdfTemplate(viewModel);

            return new MemoryStreamResult(stream, "Packing List " + data.InvoiceNo + ".pdf");
        }
    }
}
