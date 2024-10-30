using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionService : IGarmentShippingInsuranceDispositionService
    {
        private readonly IGarmentShippingInsuranceDispositionRepository _repository;
        private readonly IServiceProvider serviceProvider;

        public GarmentShippingInsuranceDispositionService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentShippingInsuranceDispositionRepository>();

            this.serviceProvider = serviceProvider;
        }

        private GarmentShippingInsuranceDispositionViewModel MapToViewModel(GarmentShippingInsuranceDispositionModel model)
        {
            var vm = new GarmentShippingInsuranceDispositionViewModel()
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

                bankName = model.BankName,
                dispositionNo = model.DispositionNo,
                insurance = new Insurance
                {
                    Id = model.InsuranceId,
                    Code = model.InsuranceCode,
                    Name = model.InsuranceName
                },
                paymentDate = model.PaymentDate,
                policyType = model.PolicyType,

                rate = model.Rate,
                remark = model.Remark, 

                items = (model.Items ?? new List<GarmentShippingInsuranceDispositionItemModel>()).Where(x => x.IsDeleted == false).Select(i => new GarmentShippingInsuranceDispositionItemViewModel
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
                    amount = i.Amount,
                    currencyRate = i.CurrencyRate,
                    BuyerAgent = new BuyerAgent
                    {
                        Id = i.BuyerAgentId,
                        Code = i.BuyerAgentCode,
                        Name = i.BuyerAgentName

                    },
                    insuranceDispositionId = i.InsuranceDispositionId,
                    invoiceId = i.InvoiceId,
                    invoiceNo = i.InvoiceNo,
                    policyDate = i.PolicyDate,
                    policyNo = i.PolicyNo,
                    amount1A = i.Amount1A,
                    amount1B = i.Amount1B,
                    amount2A = i.Amount2A,
                    amount2B = i.Amount2B,
                    amount2C = i.Amount2C
                }).ToList(),

            };
            return vm;
        }
        private GarmentShippingInsuranceDispositionModel MapToModel(GarmentShippingInsuranceDispositionViewModel viewModel)
        {
            var items = (viewModel.items ?? new List<GarmentShippingInsuranceDispositionItemViewModel>()).Select(i =>
            {

                i.BuyerAgent = i.BuyerAgent ?? new BuyerAgent();
                return new GarmentShippingInsuranceDispositionItemModel(i.policyDate, i.policyNo, i.invoiceNo, i.invoiceId, i.BuyerAgent.Id, i.BuyerAgent.Code, i.BuyerAgent.Name, i.amount, i.currencyRate, i.amount2A, i.amount2B, i.amount2C, i.amount1A, i.amount1B)
                {
                    Id = i.Id
                };

            }).ToList();


            viewModel.insurance = viewModel.insurance ?? new Insurance();


            GarmentShippingInsuranceDispositionModel garmentShippingInvoiceModel = new GarmentShippingInsuranceDispositionModel(GenerateNo(viewModel), viewModel.policyType, viewModel.paymentDate.GetValueOrDefault(),viewModel.bankName,viewModel.insurance.Id,viewModel.insurance.Name,viewModel.insurance.Code,viewModel.rate,viewModel.remark,items);

            return garmentShippingInvoiceModel;
        }

        private string GenerateNo(GarmentShippingInsuranceDispositionViewModel vm)
        {
            var year = DateTime.Now.ToString("yyyy");
            var month = DateTime.Now.ToString("MM");

            var prefix = $"DL/Polis Asuransi/{month}/{year}/";
            //var prefixToSearch = $"DL/Polis Asuransi/{month}/{year}/";

            //old numbering
            //var lastInvoiceNo = _repository.ReadAll().Where(w => w.DispositionNo.StartsWith(prefix))
            //    .OrderByDescending(o => o.DispositionNo)
            //    .Select(s => int.Parse(s.DispositionNo.Replace(prefix, "")))
            //    .FirstOrDefault();

            //new numbering
            var lastInvoiceNo = _repository.ReadAll().Where(w => w.DispositionNo.Substring(21, 4) == year)
                .OrderByDescending(o => o.DispositionNo)
                .Select(s => int.Parse(s.DispositionNo.Substring(s.DispositionNo.Length - 3)))
                .FirstOrDefault();
            var invoiceNo = $"{prefix}{(lastInvoiceNo + 1).ToString("D3")}";

            return invoiceNo;
        }
        public async Task<int> Create(GarmentShippingInsuranceDispositionViewModel viewModel)
        {
            GarmentShippingInsuranceDispositionModel model = MapToModel(viewModel);

            int Created = await _repository.InsertAsync(model);

            return Created;
        }

        public async Task<int> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public ListResult<GarmentShippingInsuranceDispositionViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "DispositionNo","InsuranceName","BankName","PolicyType"
            };
            query = QueryHelper<GarmentShippingInsuranceDispositionModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingInsuranceDispositionModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingInsuranceDispositionModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentShippingInsuranceDispositionViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingInsuranceDispositionViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentShippingInsuranceDispositionViewModel viewModel)
        {
            GarmentShippingInsuranceDispositionModel model = MapToModel(viewModel);

            return await _repository.UpdateAsync(id, model);
        }

        public Insurance GetInsurance(int id)
        {
            string insuranceUri = "master/garment-insurances";
            IHttpClientService httpClient = (IHttpClientService)serviceProvider.GetService(typeof(IHttpClientService));

            var response = httpClient.GetAsync($"{ApplicationSetting.CoreEndpoint}{insuranceUri}/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                Insurance viewModel = JsonConvert.DeserializeObject<Insurance>(result.GetValueOrDefault("data").ToString());
                return viewModel;
            }
            else
            {
                return null;
            }
        }
    }
}
