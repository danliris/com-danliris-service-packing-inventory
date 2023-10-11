using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShipingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNoteCreditAdvice
{
    public class GarmentShippingNoteCreditAdviceService : IGarmentShippingNoteCreditAdviceService
    {
        private readonly IGarmentShippingNoteCreditAdviceRepository _repository;

        public GarmentShippingNoteCreditAdviceService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentShippingNoteCreditAdviceRepository>();
        }

        private GarmentShippingNoteCreditAdviceViewModel MapToViewModel(GarmentShippingNoteCreditAdviceModel model)
        {
            GarmentShippingNoteCreditAdviceViewModel viewModel = new GarmentShippingNoteCreditAdviceViewModel
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
                receiptNo = model.ReceiptNo,
                paymentTerm = model.PaymentTerm,
                noteType = model.NoteType,
                shippingnoteId = model.ShippingNoteId,
                noteNo = model.NoteNo,
              
                date = model.Date,
                amount = model.Amount,
                paidAmount = model.PaidAmount,
                balanceamount = model.BalanceAmount,


                paymentDate = model.PaymentDate,
                nettNego = model.NettNego,

                bankComission = model.BankComission,
                creditInterest = model.CreditInterest,
                bankCharges = model.BankCharges,
                insuranceCharge = model.InsuranceCharge,

                buyer = new Buyer
                {
                    Id = model.BuyerId,
                    Code = model.BuyerCode,
                    Name = model.BuyerName,
                    Address = model.BuyerAddress
                },
                bank = new BankAccount
                {
                    id = model.BankAccountId,
                    accountName = model.BankAccountName,
                    bankAddress = model.BankAddress,
                    AccountNumber = model.BankAccountNo,
                },
              
                documentSendDate = model.DocumentSendDate,
                remark = model.Remark
            };

            return viewModel;
        }

        public async Task<int> Create(GarmentShippingNoteCreditAdviceViewModel viewModel)
        {
            viewModel.buyer = viewModel.buyer ?? new Buyer();
            viewModel.bank = viewModel.bank ?? new BankAccount();
       
            GarmentShippingNoteCreditAdviceModel model = new GarmentShippingNoteCreditAdviceModel(viewModel.shippingnoteId, viewModel.noteType, viewModel.noteNo, viewModel.date.GetValueOrDefault(), viewModel.receiptNo, viewModel.paymentTerm, viewModel.amount, viewModel.paidAmount, viewModel.balanceamount, viewModel.paymentDate.GetValueOrDefault(), viewModel.nettNego,    viewModel.buyer.Id, viewModel.buyer.Code, viewModel.buyer.Name, viewModel.buyer.Address, viewModel.bank.id, viewModel.bank.accountName, viewModel.bank.AccountNumber, viewModel.bank.bankAddress, viewModel.bankComission, viewModel.creditInterest, viewModel.bankCharges, viewModel.insuranceCharge, viewModel.documentSendDate.GetValueOrDefault(), viewModel.remark);

            return await _repository.InsertAsync(model);
        }

        public async Task<int> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();

            List<string> SearchAttributes = new List<string>() { "NoteNo", "BuyerName", "BankAccountName" };
            query = QueryHelper<GarmentShippingNoteCreditAdviceModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingNoteCreditAdviceModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingNoteCreditAdviceModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => new IndexViewModel
                {
                    id = model.Id,
                    noteNo = model.NoteNo,
                    date = model.Date,
                    amount = model.Amount,
                    paidAmount = model.PaidAmount,
                    buyerName = model.BuyerName,
                    bankAccountName = model.BankAccountName
                })
                .ToList();

            return new ListResult<IndexViewModel>(data, page, size, query.Count());
        }

        public ListResult<GarmentShippingNoteCreditAdviceViewModel> ReadData(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();

            List<string> SearchAttributes = new List<string>() { "NoteNo", "BuyerName", "BankAccountName" };
            query = QueryHelper<GarmentShippingNoteCreditAdviceModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingNoteCreditAdviceModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingNoteCreditAdviceModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentShippingNoteCreditAdviceViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingNoteCreditAdviceViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentShippingNoteCreditAdviceViewModel viewModel)
        {
            viewModel.buyer = viewModel.buyer ?? new Buyer();
            viewModel.bank = viewModel.bank ?? new BankAccount();
            GarmentShippingNoteCreditAdviceModel model = new GarmentShippingNoteCreditAdviceModel(viewModel.shippingnoteId, viewModel.noteType, viewModel.noteNo, viewModel.date.GetValueOrDefault(), viewModel.receiptNo, viewModel.paymentTerm, viewModel.amount, viewModel.paidAmount, viewModel.balanceamount, viewModel.paymentDate.GetValueOrDefault(), viewModel.nettNego, viewModel.buyer.Id, viewModel.buyer.Code, viewModel.buyer.Name, viewModel.buyer.Address, viewModel.bank.id, viewModel.bank.accountName, viewModel.bank.AccountNumber, viewModel.bank.bankAddress, viewModel.bankComission, viewModel.creditInterest, viewModel.bankCharges, viewModel.insuranceCharge, viewModel.documentSendDate.GetValueOrDefault(), viewModel.remark);

            return await _repository.UpdateAsync(id, model);
        }
    }
}
