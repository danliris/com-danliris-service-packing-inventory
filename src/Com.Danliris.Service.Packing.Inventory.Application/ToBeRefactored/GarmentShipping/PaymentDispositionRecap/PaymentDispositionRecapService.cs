using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDispositionRecap;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDispositionRecap;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDispositionRecap
{
    public class PaymentDispositionRecapService : IPaymentDispositionRecapService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IGarmentShippingPaymentDispositionRecapRepository _recapRepository;
        private readonly IGarmentShippingPaymentDispositionRepository _paymentDispositionRepository;
        private readonly IGarmentShippingInvoiceRepository _invoiceRepository;
        private readonly IGarmentPackingListRepository _packingListRepository;
        private readonly IGarmentShippingPaymentDispositionRepository _garmentShippingPaymentDispositionRepository;

        public PaymentDispositionRecapService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            _recapRepository = serviceProvider.GetService<IGarmentShippingPaymentDispositionRecapRepository>();
            _paymentDispositionRepository = serviceProvider.GetService<IGarmentShippingPaymentDispositionRepository>();
            _invoiceRepository = serviceProvider.GetService<IGarmentShippingInvoiceRepository>();
            _packingListRepository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _garmentShippingPaymentDispositionRepository = serviceProvider.GetService<IGarmentShippingPaymentDispositionRepository>();
        }

        private PaymentDispositionRecapViewModel MapToViewModel(GarmentShippingPaymentDispositionRecapModel model)
        {
            PaymentDispositionRecapViewModel viewModel = new PaymentDispositionRecapViewModel
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

                recapNo = model.RecapNo,
                date = model.Date,
                emkl = new EMKL
                {
                    Id = model.EmklId,
                    Code = model.EMKLCode,
                    Name = model.EMKLName,
                    address = model.EMKLAddress,
                    npwp = model.EMKLNPWP,
                },

                items = (model.Items ?? new List<GarmentShippingPaymentDispositionRecapItemModel>()).Select(i => new PaymentDispositionRecapItemViewModel
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

                    paymentDisposition = new GarmentShippingPaymentDispositionViewModel
                    {
                        Id = i.PaymentDispositionId
                    },
                    service = i.Service,
                    othersPayment = i.OthersPayment,
                    truckingPayment = i.TruckingPayment,
                    vatService = i.VatService,
                    amountService = i.AmountService
                }).ToList()
            };

            return viewModel;
        }

        private GarmentShippingPaymentDispositionRecapModel MapToModel(PaymentDispositionRecapViewModel vm)
        {
            vm.recapNo = vm.recapNo ?? GenerateNo(vm);
            vm.emkl = vm.emkl ?? new EMKL();

            var items = (vm.items ?? new List<PaymentDispositionRecapItemViewModel>()).Select(i =>
            {
                i.paymentDisposition = i.paymentDisposition ?? new GarmentShippingPaymentDispositionViewModel();

                return new GarmentShippingPaymentDispositionRecapItemModel(i.paymentDisposition.Id, i.service, i.othersPayment, i.truckingPayment, i.vatService, i.amountService) { Id = i.Id };
            }).ToList();

            return new GarmentShippingPaymentDispositionRecapModel(vm.recapNo, vm.date.GetValueOrDefault(), vm.emkl.Id, vm.emkl.Code, vm.emkl.Name, vm.emkl.address, vm.emkl.npwp, items) { Id = vm.Id };
        }

        private string GenerateNo(PaymentDispositionRecapViewModel vm)
        {
            var now = DateTime.Now;

            var prefix = "DL/";
            var suffix = $"/EMKL/GmtShp/{now.ToString("MM")}/{now.ToString("yyyy")}";

            var lastNo = _recapRepository.ReadAll().Where(w => w.RecapNo.StartsWith(prefix) && w.RecapNo.EndsWith(suffix))
                .OrderByDescending(o => o.RecapNo)
                .Select(s => int.Parse(s.RecapNo.Replace(prefix, "").Replace(suffix, "")))
                .FirstOrDefault();
            var newNo = $"{prefix}{(lastNo + 1).ToString("D4")}{suffix}";

            return newNo;
        }

        public async Task<int> Create(PaymentDispositionRecapViewModel viewModel)
        {
            GarmentShippingPaymentDispositionRecapModel model = MapToModel(viewModel);

            return await _recapRepository.InsertAsync(model);
        }

        public async Task<int> Delete(int id)
        {
            return await _recapRepository.DeleteAsync(id);
        }

        public ListResult<PaymentDispositionRecapViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _recapRepository.ReadAll();

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingPaymentDispositionRecapModel>.Filter(query, FilterDictionary);

            List<string> SearchAttributes = new List<string>()
            {
                "RecapNo", "EMKLCode", "EMKLName", "EMKLAddress", "EMKLNPWP"
            };
            query = QueryHelper<GarmentShippingPaymentDispositionRecapModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingPaymentDispositionRecapModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<PaymentDispositionRecapViewModel>(data, page, size, query.Count());
        }

        public async Task<PaymentDispositionRecapViewModel> ReadById(int id)
        {
            var data = await _recapRepository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);
            foreach (var item in viewModel.items)
            {
                var dispoQuery = _paymentDispositionRepository.ReadAll();
                item.paymentDisposition = dispoQuery
                    .Where(w => w.Id == item.paymentDisposition.Id)
                    .Select(s => new GarmentShippingPaymentDispositionViewModel
                    {
                        Id = s.Id,
                        dispositionNo = s.DispositionNo,

                        invoiceNumber = s.InvoiceNumber,
                        invoiceDate = s.InvoiceDate,

                        billValue = s.BillValue,
                        vatValue = s.VatValue,
                        incomeTaxValue = s.IncomeTaxValue,
                        invoiceTaxNumber=s.InvoiceTaxNumber,
                        invoiceDetails = s.InvoiceDetails.Select(d => new GarmentShippingPaymentDispositionInvoiceDetailViewModel
                        {
                            Id = d.Id,
                            invoiceNo = d.InvoiceNo,
                            invoiceId = d.InvoiceId,
                            quantity = d.Quantity,
                            volume = d.Volume,
                            grossWeight = d.GrossWeight,
                            chargeableWeight = d.ChargeableWeight,
                            totalCarton = d.TotalCarton,
                        }).ToList(),
                        amount = s.BillValue + s.VatValue,
                        paid = s.BillValue + s.VatValue - s.IncomeTaxValue + Convert.ToDecimal(item.othersPayment),
                    })
                    .Single();

                var qtyByUnits = new Dictionary<string, double>();
                item.paymentDisposition.percentage = new Dictionary<string, double>();
                item.paymentDisposition.amountPerUnit = new Dictionary<string, double>();

                foreach (var detail in item.paymentDisposition.invoiceDetails)
                {
                    var invQUery = _invoiceRepository.ReadAll();
                    detail.invoice = invQUery
                        .Where(w => w.Id == detail.invoiceId)
                        .Select(s => new Invoice
                        {
                            packingListId = s.PackingListId,
                            BuyerAgent = new BuyerAgent
                            {
                                Id = s.BuyerAgentId,
                                Code = s.BuyerAgentCode,
                                Name = s.BuyerAgentName
                            },
                            items = s.Items.Select(i => new InvoiceItem
                            {
                                unit = i.UnitCode,
                                quantity = i.Quantity
                            }).ToList()
                        })
                        .Single();
                    var units = detail.invoice.items.Select(i => i.unit).Distinct();
                    detail.invoice.unit = string.Join(", ", units);

                    var plQuery = _packingListRepository.ReadAll();
                    detail.packingList = plQuery
                        .Where(w => w.Id == detail.invoice.packingListId)
                        .Select(s => new PackingList
                        {
                            totalCBM = s.Measurements.Sum(m => m.Length * m.Width * m.Height * m.CartonsQuantity / 1000000)
                        })
                        .Single();
                    detail.packingList.totalCBM = Math.Round(detail.packingList.totalCBM, 3, MidpointRounding.AwayFromZero);

                    foreach (var unit in units)
                    {
                        var qtyByUnit = detail.invoice.items.Where(i => i.unit == unit).Sum(i => i.quantity);
                        qtyByUnits[unit] = qtyByUnits.GetValueOrDefault(unit) + qtyByUnit;
                    }
                }

                var totalQuantity = item.paymentDisposition.invoiceDetails.Sum(d => d.quantity);
                foreach (var unit in qtyByUnits.Keys)
                {
                    item.paymentDisposition.percentage[unit] = Math.Round(qtyByUnits[unit] / (double)totalQuantity * 100, 2, MidpointRounding.AwayFromZero);
                    item.paymentDisposition.amountPerUnit[unit] = Math.Round(item.paymentDisposition.percentage[unit] * (double)item.paymentDisposition.paid / 100, 2, MidpointRounding.AwayFromZero);
                }
            }

            return viewModel;
        }

        public async Task<int> Update(int id, PaymentDispositionRecapViewModel viewModel)
        {
            GarmentShippingPaymentDispositionRecapModel model = MapToModel(viewModel);
            foreach(var item in model.Items)
            {
                var vmItem = viewModel.items.FirstOrDefault(i => i.Id == item.Id);
                if(vmItem != null)
                {
                    var paymentDisposition = await _garmentShippingPaymentDispositionRepository.ReadByIdAsync(item.PaymentDispositionId);
                    if(paymentDisposition != null)
                    {
                        paymentDisposition.SetIncomeTaxValue(vmItem.paymentDisposition.incomeTaxValue, "", "");
                        item.SetPaymentDisposition(paymentDisposition);
                    }
                }
            }
            return await _recapRepository.UpdateAsync(id, model);
        }
    }
}
