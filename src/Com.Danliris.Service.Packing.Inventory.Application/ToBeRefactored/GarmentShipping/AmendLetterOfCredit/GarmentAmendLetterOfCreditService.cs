using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.AmendLetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.AmendLetterOfCredit;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.AmendLetterOfCredit
{
    public class GarmentAmendLetterOfCreditService : IGarmentAmendLetterOfCreditService
    {
        private readonly IGarmentAmendLetterOfCreditRepository _repository;
        protected readonly ILogHistoryRepository logHistoryRepository;
        public GarmentAmendLetterOfCreditService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentAmendLetterOfCreditRepository>();
            logHistoryRepository = serviceProvider.GetService<ILogHistoryRepository>();
        }

        private GarmentAmendLetterOfCreditViewModel MapToViewModel(GarmentShippingAmendLetterOfCreditModel model)
        {
            GarmentAmendLetterOfCreditViewModel viewModel = new GarmentAmendLetterOfCreditViewModel
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


                amount = model.Amount,
                description = model.Description,
                amendNumber = model.AmendNumber,
                date = model.Date,
                documentCreditNo = model.DocumentCreditNo,
                letterOfCreditId = model.LetterOfCreditId
            };

            return viewModel;
        }

        public async Task<int> Create(GarmentAmendLetterOfCreditViewModel viewModel)
        {
            GarmentShippingAmendLetterOfCreditModel model = new GarmentShippingAmendLetterOfCreditModel(viewModel.documentCreditNo, viewModel.letterOfCreditId, viewModel.amendNumber, viewModel.date, viewModel.description, viewModel.amount);

            // Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Create Amend Letter Of Credit - " + model.DocumentCreditNo);

            return await _repository.InsertAsync(model);
        }

        public async Task<int> Delete(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Delete Amend Letter Of Credit - " + data.DocumentCreditNo);

            return await _repository.DeleteAsync(id);
        }

        public ListResult<GarmentAmendLetterOfCreditViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();

            List<string> SearchAttributes = new List<string>() { "DocumentCreditNo", "Description" };
            query = QueryHelper<GarmentShippingAmendLetterOfCreditModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingAmendLetterOfCreditModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingAmendLetterOfCreditModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentAmendLetterOfCreditViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentAmendLetterOfCreditViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentAmendLetterOfCreditViewModel viewModel)
        {
            GarmentShippingAmendLetterOfCreditModel model = new GarmentShippingAmendLetterOfCreditModel(viewModel.documentCreditNo,viewModel.letterOfCreditId,viewModel.amendNumber,viewModel.date,viewModel.description,viewModel.amount);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Update Amend Letter Of Credit - " + model.DocumentCreditNo);

            return await _repository.UpdateAsync(id, model);
        }

    }
}
