using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LetterOfCredit;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LetterOfCredit
{
    public class GarmentLetterOfCreditService : IGarmentLetterOfCreditService
    {
        private readonly IGarmentLetterOfCreditRepository _repository;
        protected readonly ILogHistoryRepository logHistoryRepository;
        public GarmentLetterOfCreditService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentLetterOfCreditRepository>();
            logHistoryRepository = serviceProvider.GetService<ILogHistoryRepository>();
        }

        private GarmentLetterOfCreditViewModel MapToViewModel(GarmentShippingLetterOfCreditModel model)
        {
            GarmentLetterOfCreditViewModel viewModel = new GarmentLetterOfCreditViewModel
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

                
                Uom = new UnitOfMeasurement
                {
                    Id = model.UomId,
                    Unit = model.UomUnit,
                },
                Applicant = new Buyer
                {
                    Id = model.ApplicantId,
                    Code = model.ApplicantCode,
                    Name = model.ApplicantName,
                },
                TotalAmount=model.TotalAmount,
                Date=model.Date,
                DocumentCreditNo=model.DocumentCreditNo,
                ExpireDate=model.ExpireDate,
                ExpirePlace=model.ExpirePlace,
                IssuedBank=model.IssuedBank,
                LatestShipment=model.LatestShipment,
                LCCondition=model.LCCondition,
                Quantity=model.Quantity
            };

            return viewModel;
        }

        public async Task<int> Create(GarmentLetterOfCreditViewModel viewModel)
        {
            viewModel.Applicant  = viewModel.Applicant ?? new Buyer();
            viewModel.Uom = viewModel.Uom ?? new UnitOfMeasurement();
            GarmentShippingLetterOfCreditModel model = new GarmentShippingLetterOfCreditModel(viewModel.DocumentCreditNo, viewModel.Date.GetValueOrDefault(), viewModel.IssuedBank, viewModel.Applicant.Id, viewModel.Applicant.Code, viewModel.Applicant.Name, viewModel.ExpireDate.GetValueOrDefault(), viewModel.ExpirePlace, viewModel.LatestShipment.GetValueOrDefault(), viewModel.LCCondition, viewModel.Quantity, viewModel.Uom.Id.GetValueOrDefault(), viewModel.Uom.Unit, viewModel.TotalAmount);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Create Letter Of Credit - " + model.DocumentCreditNo);

            return await _repository.InsertAsync(model);
        }

        public async Task<int> Delete(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Delete Letter Of Credit - " + data.DocumentCreditNo);

            return await _repository.DeleteAsync(id);
        }

        public ListResult<GarmentLetterOfCreditViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();

            List<string> SearchAttributes = new List<string>() { "DocumentCreditNo", "IssuedBank", "ApplicantName", "UomUnit" };
            query = QueryHelper<GarmentShippingLetterOfCreditModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingLetterOfCreditModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingLetterOfCreditModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentLetterOfCreditViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentLetterOfCreditViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentLetterOfCreditViewModel viewModel)
        {
            viewModel.Applicant = viewModel.Applicant ?? new Buyer();
            viewModel.Uom = viewModel.Uom ?? new UnitOfMeasurement();
            GarmentShippingLetterOfCreditModel model = new GarmentShippingLetterOfCreditModel(viewModel.DocumentCreditNo, viewModel.Date.GetValueOrDefault(), viewModel.IssuedBank, viewModel.Applicant.Id, viewModel.Applicant.Code, viewModel.Applicant.Name, viewModel.ExpireDate.GetValueOrDefault(), viewModel.ExpirePlace, viewModel.LatestShipment.GetValueOrDefault(), viewModel.LCCondition, viewModel.Quantity, viewModel.Uom.Id.GetValueOrDefault(), viewModel.Uom.Unit, viewModel.TotalAmount);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Update Letter Of Credit - " + model.DocumentCreditNo);

            return await _repository.UpdateAsync(id, model);
        }
    }
}
