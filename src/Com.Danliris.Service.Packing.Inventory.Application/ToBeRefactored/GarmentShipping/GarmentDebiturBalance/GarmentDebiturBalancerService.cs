using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDebiturBalance;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentDebiturBalance;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDebiturBalance
{
    public class GarmentDebiturBalanceService : IGarmentDebiturBalanceService
    {
        private const string UserAgent = "GarmentCoverLetterService";

        private readonly IGarmentDebiturBalanceRepository _repository;
    
        private readonly IIdentityProvider _identityProvider;

        public GarmentDebiturBalanceService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentDebiturBalanceRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        private GarmentDebiturBalanceViewModel MapToViewModel(GarmentDebiturBalanceModel model)
        {
            GarmentDebiturBalanceViewModel viewModel = new GarmentDebiturBalanceViewModel
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

                balanceDate = model.BalanceDate,
                buyerAgent= new BuyerAgent
                {                   
                    Id=model.BuyerAgentId,
                    Code=model.BuyerAgentCode,
                    Name = model.BuyerAgentName,
                },
                balanceAmount = model.BalanceAmount,                
            };

            return viewModel;
        }

        public async Task<int> Create(GarmentDebiturBalanceViewModel viewModel)
        {
            viewModel.buyerAgent = viewModel.buyerAgent ?? new BuyerAgent();

            GarmentDebiturBalanceModel model = new GarmentDebiturBalanceModel(viewModel.balanceDate.GetValueOrDefault(), viewModel.buyerAgent.Id, viewModel.buyerAgent.Code, viewModel.buyerAgent.Name, viewModel.balanceAmount);

            return await _repository.InsertAsync(model);
        }

        public async Task<int> Delete(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
           
            return await _repository.DeleteAsync(id);
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();

            List<string> SearchAttributes = new List<string>() { "BuyerAgentCode", "BuyerAgentName" };
            query = QueryHelper<GarmentDebiturBalanceModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentDebiturBalanceModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentDebiturBalanceModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => new IndexViewModel
                {
                    id = model.Id,
                    buyerAgentId = model.BuyerAgentId,
                    buyerAgentCode = model.BuyerAgentCode,
                    buyerAgentName = model.BuyerAgentName,
                    balanceAmount = model.BalanceAmount,
                    balanceDate = model.BalanceDate,
                })
                .ToList();

            return new ListResult<IndexViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentDebiturBalanceViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentDebiturBalanceViewModel viewModel)
        {
            viewModel.buyerAgent = viewModel.buyerAgent ?? new BuyerAgent();
            GarmentDebiturBalanceModel model = new GarmentDebiturBalanceModel(viewModel.balanceDate.GetValueOrDefault(), viewModel.buyerAgent.Id, viewModel.buyerAgent.Code, viewModel.buyerAgent.Name, viewModel.balanceAmount);

            return await _repository.UpdateAsync(id, model);
        }
    }
}
