using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.VBPayment;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.VBPayment;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.VBPayment
{
    public class GarmentShippingVBPaymentService : IGarmentShippingVBPaymentService
    {
        private readonly IGarmentShippingVBPaymentRepository _repository;

        public GarmentShippingVBPaymentService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentShippingVBPaymentRepository>();
        }

        private GarmentShippingVBPaymentViewModel MapToViewModel(GarmentShippingVBPaymentModel model)
        {
            var vm = new GarmentShippingVBPaymentViewModel()
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

                vbNo = model.VBNo,
                buyer = new Buyer
                {
                    Id = model.BuyerId,
                    Code = model.BuyerCode,
                    Name = model.BuyerName,
                },
                paymentDate = model.PaymentDate,
                billValue=model.BillValue,
                emkl=new EMKL
                {
                    Id=model.EMKLId,
                    Code=model.EMKLCode,
                    Name=model.EMKLName
                },
                emklInvoiceNo=model.EMKLInvoiceNo,
                forwarder=new Forwarder
                {
                    id=model.ForwarderId,
                    code=model.ForwarderCode,
                    name=model.ForwarderName
                },
                forwarderInvoiceNo=model.ForwarderInvoiceNo,
                incomeTax=new IncomeTax
                {
                    id=model.IncomeTaxId,
                    rate=model.IncomeTaxRate,
                    name=model.IncomeTaxName
                },
                paymentType=model.PaymentType,
                vatValue=model.VatValue,
                vbDate=model.VBDate,
                
                units = model.Units.Select(i => new GarmentShippingVBPaymentUnitViewModel
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

                    unit = new Unit
                    {
                        Id = i.UnitId,
                        Code = i.UnitCode,
                        Name = i.UnitName,
                    },
                    billValue=i.BillValue,
                    vbPaymentId=i.VBPaymentId

                }).ToList(),

                invoices = model.Invoices.Select(i => new GarmentShippingVBPaymentInvoiceViewModel
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

                    vbPaymentId = i.VBPaymentId,
                    invoiceId=i.InvoiceId,
                    invoiceNo=i.InvoiceNo

                }).ToList()
            };
            return vm;
        }

        private GarmentShippingVBPaymentModel MapToModel(GarmentShippingVBPaymentViewModel viewModel)
        {
            var units = (viewModel.units ?? new List<GarmentShippingVBPaymentUnitViewModel>()).Select(i =>
            {
                i.unit = i.unit ?? new Unit();
                return new GarmentShippingVBPaymentUnitModel(i.unit.Id, i.unit.Code, i.unit.Name,i.billValue)
                {
                    Id = i.Id
                };
            }).ToList();

            var invoices = (viewModel.invoices ?? new List<GarmentShippingVBPaymentInvoiceViewModel>()).Select(i =>
            {
                return new GarmentShippingVBPaymentInvoiceModel(i.invoiceId, i.invoiceNo)
                {
                    Id = i.Id
                };
            }).ToList();

            viewModel.emkl = viewModel.emkl ?? new EMKL();
            viewModel.forwarder = viewModel.forwarder ?? new Forwarder();
            viewModel.buyer = viewModel.buyer ?? new Buyer();
            viewModel.incomeTax = viewModel.incomeTax ?? new IncomeTax();
            viewModel.vbNo = GenerateNo(viewModel);
            GarmentShippingVBPaymentModel garmentPackingListModel = new GarmentShippingVBPaymentModel(viewModel.vbNo, viewModel.vbDate,viewModel.paymentType,viewModel.buyer.Id, viewModel.buyer.Code,
                viewModel.buyer.Name, viewModel.emkl.Id, viewModel.emkl.Name, viewModel.emkl.Code, viewModel.forwarder.id, viewModel.forwarder.code, viewModel.forwarder.name, viewModel.emklInvoiceNo,
                viewModel.forwarderInvoiceNo, viewModel.billValue, viewModel.vatValue, viewModel.paymentDate, viewModel.incomeTax.id, viewModel.incomeTax.name, viewModel.incomeTax.rate,
                units, invoices);

            return garmentPackingListModel;
        }

        private string GenerateNo(GarmentShippingVBPaymentViewModel viewModel)
        {
            var year = DateTime.Now.ToString("yy");
            var month = DateTime.Now.ToString("MM");

            var prefix = $"VB/{year}/{month}";

            var lastNo = _repository.ReadAll().Where(w => w.VBNo.StartsWith(prefix))
                .OrderByDescending(o => o.VBNo)
                .Select(s => int.Parse(s.VBNo.Replace(prefix, "")))
                .FirstOrDefault();
            var invoiceNo = $"{prefix}{(lastNo + 1).ToString("D5")}";

            return invoiceNo;
        }

        public async Task<int> Create(GarmentShippingVBPaymentViewModel viewModel)
        {
            GarmentShippingVBPaymentModel garmentShippingVBPaymentModel = MapToModel(viewModel);

            int Created = await _repository.InsertAsync(garmentShippingVBPaymentModel);

            return Created;
        }

        public async Task<int> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public ListResult<GarmentShippingVBPaymentViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "VBNo", "BuyerCode", "BuyerName","EMKLName","ForwarderName"
            };
            query = QueryHelper<GarmentShippingVBPaymentModel>.Search(query, SearchAttributes, keyword, ignoreDot: true);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingVBPaymentModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingVBPaymentModel>.Order(query, OrderDictionary, ignoreDot: true);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => new GarmentShippingVBPaymentViewModel
                {
                    Id=model.Id,
                    vbNo=model.VBNo,
                    vbDate=model.VBDate,
                    paymentDate=model.PaymentDate,
                    paymentType=model.PaymentType,
                    buyer=new Buyer
                    {

                    },
                    emkl = new EMKL
                    {
                        Id = model.EMKLId,
                        Code = model.EMKLCode,
                        Name = model.EMKLName
                    },
                    emklInvoiceNo = model.EMKLInvoiceNo,
                    forwarder = new Forwarder
                    {
                        id = model.ForwarderId,
                        code = model.ForwarderCode,
                        name = model.ForwarderName
                    },
                    forwarderInvoiceNo = model.ForwarderInvoiceNo,
                })
                .ToList();

            return new ListResult<GarmentShippingVBPaymentViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingVBPaymentViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentShippingVBPaymentViewModel viewModel)
        {
            GarmentShippingVBPaymentModel garmentShippingVBPaymentModel = MapToModel(viewModel);

            return await _repository.UpdateAsync(id, garmentShippingVBPaymentModel);
        }
    }
}
