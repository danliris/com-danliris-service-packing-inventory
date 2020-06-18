﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
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

        public GarmentShippingCreditAdviceService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentShippingCreditAdviceRepository>();
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

                packingListId = model.PackingListId,
                invoiceId = model.InvoiceId,
                invoiceNo = model.InvoiceNo,

                date = model.Date,
                amount = model.Amount,
                amountToBePaid = model.AmountToBePaid,

                valas = model.Valas,
                lcType = model.LCType,
                inkaso = model.Inkaso,
                disconto = model.Disconto,
                srNo = model.SRNo,
                negoDate = model.NegoDate,
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
                    Name = model.BuyerName,
                    Address = model.BuyerAddress
                },
                bank = new BankAccount
                {
                    id = model.BankAccountId,
                    accountName = model.BankAccountName,
                    bankAddress = model.BankAddress
                },
                creditInterest = model.CreditInterest,
                bankCharges = model.BankCharges,
                documentPresente = model.DocumentPresente,
                remark = model.Remark
            };

            return viewModel;
        }

        public async Task<int> Create(GarmentShippingCreditAdviceViewModel viewModel)
        {
            viewModel.buyer = viewModel.buyer ?? new Buyer();
            viewModel.bank = viewModel.bank ?? new BankAccount();
            GarmentShippingCreditAdviceModel model = new GarmentShippingCreditAdviceModel(viewModel.packingListId, viewModel.invoiceId, viewModel.invoiceNo, viewModel.date.GetValueOrDefault(), viewModel.amount, viewModel.amountToBePaid, viewModel.valas, viewModel.lcType, viewModel.inkaso, viewModel.disconto, viewModel.srNo, viewModel.negoDate.GetValueOrDefault(), viewModel.condition, viewModel.bankComission, viewModel.discrepancyFee, viewModel.nettNego, viewModel.btbCADate.GetValueOrDefault(), viewModel.btbAmount, viewModel.btbRatio, viewModel.btbRate, viewModel.btbTransfer, viewModel.btbMaterial, viewModel.billDays, viewModel.billAmount, viewModel.billCA, viewModel.buyer.Id, viewModel.buyer.Name, viewModel.buyer.Address, viewModel.bank.id, viewModel.bank.accountName, viewModel.bank.bankAddress, viewModel.creditInterest, viewModel.bankCharges, viewModel.documentPresente.GetValueOrDefault(), viewModel.remark);

            return await _repository.InsertAsync(model);
        }

        public async Task<int> Delete(int id)
        {
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
            GarmentShippingCreditAdviceModel model = new GarmentShippingCreditAdviceModel(viewModel.packingListId, viewModel.invoiceId, viewModel.invoiceNo, viewModel.date.GetValueOrDefault(), viewModel.amount, viewModel.amountToBePaid, viewModel.valas, viewModel.lcType, viewModel.inkaso, viewModel.disconto, viewModel.srNo, viewModel.negoDate.GetValueOrDefault(), viewModel.condition, viewModel.bankComission, viewModel.discrepancyFee, viewModel.nettNego, viewModel.btbCADate.GetValueOrDefault(), viewModel.btbAmount, viewModel.btbRatio, viewModel.btbRate, viewModel.btbTransfer, viewModel.btbMaterial, viewModel.billDays, viewModel.billAmount, viewModel.billCA, viewModel.buyer.Id, viewModel.buyer.Name, viewModel.buyer.Address, viewModel.bank.id, viewModel.bank.accountName, viewModel.bank.bankAddress, viewModel.creditInterest, viewModel.bankCharges, viewModel.documentPresente.GetValueOrDefault(), viewModel.remark);

            return await _repository.UpdateAsync(id, model);
        }
    }
}
