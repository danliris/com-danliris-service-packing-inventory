using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CreditAdvice
{
    public class GarmentShippingCreditAdviceService : IGarmentShippingCreditAdviceService
    {
        private readonly IGarmentShippingCreditAdviceRepository _repository;
        protected readonly ILogHistoryRepository logHistoryRepository;
        public GarmentShippingCreditAdviceService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentShippingCreditAdviceRepository>();
            logHistoryRepository = serviceProvider.GetService<ILogHistoryRepository>();
        }

        private GarmentShippingCreditAdviceViewModel MapToViewModel(GarmentShippingCreditAdviceModel model)
        {
            GarmentShippingCreditAdviceViewModel viewModel = new GarmentShippingCreditAdviceViewModel
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
                packingListId = model.PackingListId,
                invoiceId = model.InvoiceId,
                invoiceNo = model.InvoiceNo,
                paymentTerm = model.PaymentTerm,

                lcNo = model.LCNo,
                date = model.Date,
                amount = model.Amount,
                amountToBePaid = model.AmountToBePaid,
                amountPaid = model.AmountPaid,
                balanceamount = model.BalanceAmount,

                valas = model.Valas,
                lcType = model.LCType,
                inkaso = model.Inkaso,
                disconto = model.Disconto,
                srNo = model.SRNo,
                negoDate = model.NegoDate,
                paymentDate = model.PaymentDate,
                condition = model.Condition,
                bankComission = model.BankComission,
                discrepancyFee = model.DiscrepancyFee,
                nettNego = model.NettNego,

                btbCADate = model.BTBCADate,
                btbAmount = model.BTBAmount,
                btbRatio = model.BTBRatio,
                btbRate = model.BTBRate,
                btbTransfer = model.BTBTransfer,
                btbMaterial = model.BTBMaterial,

                billDays = model.BillDays,
                billAmount = model.BillAmount,
                billCA = model.BillCA,
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
                creditInterest = model.CreditInterest,
                bankCharges = model.BankCharges,
                otherCharge = model.OtherCharge,
                documentPresente = model.DocumentPresente,
                cargoPolicyNo = model.CargoPolicyNo,
                cargoPolicyDate = model.CargoPolicyDate,
                cargoPolicyValue = model.CargoPolicyValue,
                accountsReceivablePolicyNo = model.AccountsReceivablePolicyNo,
                accountsReceivablePolicyDate = model.AccountsReceivablePolicyDate,
                accountsReceivablePolicyValue = model.AccountsReceivablePolicyValue,
                documentSendDate = model.DocumentSendDate,
                remark = model.Remark
            };

            return viewModel;
        }

        public async Task<int> Create(GarmentShippingCreditAdviceViewModel viewModel)
        {
            viewModel.buyer = viewModel.buyer ?? new Buyer();
            viewModel.bank = viewModel.bank ?? new BankAccount();
            GarmentShippingCreditAdviceModel model = new GarmentShippingCreditAdviceModel(viewModel.packingListId, viewModel.invoiceId, viewModel.invoiceNo, viewModel.date.GetValueOrDefault(), viewModel.amount, viewModel.amountToBePaid, viewModel.amountPaid, viewModel.balanceamount, viewModel.paymentTerm, viewModel.receiptNo, viewModel.lcNo, viewModel.valas, viewModel.lcType, viewModel.inkaso, viewModel.disconto, viewModel.srNo, viewModel.negoDate.GetValueOrDefault(), viewModel.paymentDate.GetValueOrDefault(), viewModel.condition, viewModel.bankComission, viewModel.discrepancyFee, viewModel.nettNego, viewModel.btbCADate.GetValueOrDefault(), viewModel.btbAmount, viewModel.btbRatio, viewModel.btbRate, viewModel.btbTransfer, viewModel.btbMaterial, viewModel.billDays, viewModel.billAmount, viewModel.billCA, viewModel.buyer.Id, viewModel.buyer.Code, viewModel.buyer.Name, viewModel.buyer.Address, viewModel.bank.id, viewModel.bank.accountName, viewModel.bank.AccountNumber, viewModel.bank.bankAddress, viewModel.creditInterest, viewModel.bankCharges, viewModel.otherCharge, viewModel.documentPresente.GetValueOrDefault(), viewModel.cargoPolicyNo, viewModel.cargoPolicyDate.GetValueOrDefault(), viewModel.cargoPolicyValue, viewModel.accountsReceivablePolicyNo, viewModel.accountsReceivablePolicyDate.GetValueOrDefault(), viewModel.accountsReceivablePolicyValue, viewModel.documentSendDate.GetValueOrDefault(), viewModel.remark);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Create Credit Advice - " + model.InvoiceNo);

            return await _repository.InsertAsync(model);
        }

        public async Task<int> Delete(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Delete Credit Advice - " + data.InvoiceNo);

            return await _repository.DeleteAsync(id);
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();

            List<string> SearchAttributes = new List<string>() { "InvoiceNo", "BuyerName", "BankAccountName" };
            query = QueryHelper<GarmentShippingCreditAdviceModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingCreditAdviceModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingCreditAdviceModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => new IndexViewModel
                {
                    id = model.Id,
                    invoiceNo = model.InvoiceNo,
                    date = model.Date,
                    amount = model.Amount,
                    amountToBePaid = model.AmountToBePaid,
                    buyerName = model.BuyerName,
                    bankAccountName = model.BankAccountName
                })
                .ToList();

            return new ListResult<IndexViewModel>(data, page, size, query.Count());
        }

        public ListResult<GarmentShippingCreditAdviceViewModel> ReadData(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();

            List<string> SearchAttributes = new List<string>() { "InvoiceNo", "BuyerName", "BankAccountName" };
            query = QueryHelper<GarmentShippingCreditAdviceModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingCreditAdviceModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingCreditAdviceModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentShippingCreditAdviceViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingCreditAdviceViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentShippingCreditAdviceViewModel viewModel)
        {
            viewModel.buyer = viewModel.buyer ?? new Buyer();
            viewModel.bank = viewModel.bank ?? new BankAccount();
            GarmentShippingCreditAdviceModel model = new GarmentShippingCreditAdviceModel(viewModel.packingListId, viewModel.invoiceId, viewModel.invoiceNo, viewModel.date.GetValueOrDefault(), viewModel.amount, viewModel.amountToBePaid, viewModel.amountPaid, viewModel.balanceamount, viewModel.paymentTerm, viewModel.receiptNo, viewModel.lcNo, viewModel.valas, viewModel.lcType, viewModel.inkaso, viewModel.disconto, viewModel.srNo, viewModel.negoDate.GetValueOrDefault(), viewModel.paymentDate.GetValueOrDefault(), viewModel.condition, viewModel.bankComission, viewModel.discrepancyFee, viewModel.nettNego, viewModel.btbCADate.GetValueOrDefault(), viewModel.btbAmount, viewModel.btbRatio, viewModel.btbRate, viewModel.btbTransfer, viewModel.btbMaterial, viewModel.billDays, viewModel.billAmount, viewModel.billCA, viewModel.buyer.Id, viewModel.buyer.Code, viewModel.buyer.Name, viewModel.buyer.Address, viewModel.bank.id, viewModel.bank.accountName, viewModel.bank.AccountNumber, viewModel.bank.bankAddress, viewModel.creditInterest, viewModel.bankCharges, viewModel.otherCharge, viewModel.documentPresente.GetValueOrDefault(), viewModel.cargoPolicyNo, viewModel.cargoPolicyDate.GetValueOrDefault(), viewModel.cargoPolicyValue, viewModel.accountsReceivablePolicyNo, viewModel.accountsReceivablePolicyDate.GetValueOrDefault(), viewModel.accountsReceivablePolicyValue, viewModel.documentSendDate.GetValueOrDefault(), viewModel.remark);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Update Credit Advice - " + model.InvoiceNo);

            return await _repository.UpdateAsync(id, model);
        }
    }
}
