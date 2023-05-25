using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote
{
    public class GarmentShippingLocalSalesNoteService : IGarmentShippingLocalSalesNoteService
    {
        private readonly IGarmentShippingLocalSalesNoteRepository _repository;       

        private readonly IServiceProvider serviceProvider;

        public GarmentShippingLocalSalesNoteService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentShippingLocalSalesNoteRepository>();

            this.serviceProvider = serviceProvider;
        }

        private GarmentShippingLocalSalesNoteViewModel MapToViewModel(GarmentShippingLocalSalesNoteModel model)
        {
            GarmentShippingLocalSalesNoteViewModel viewModel = new GarmentShippingLocalSalesNoteViewModel
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

                noteNo = model.NoteNo,
                date = model.Date,
                transactionType = new TransactionType
                {
                    id = model.TransactionTypeId,
                    code = model.TransactionTypeCode,
                    name = model.TransactionTypeName
                },
                vat = new Vat
                {
                    id = model.VatId,
                    rate = model.VatRate,
                },
                buyer = new Buyer
                {
                    Id = model.BuyerId,
                    Code = model.BuyerCode,
                    Name = model.BuyerName,
                    npwp = model.BuyerNPWP,
                    KaberType = model.KaberType
                },
                bank = new BankAccount
                {
                    id = model.BankId,
                    bankName = model.BankName,
                    AccountNumber = model.AccountNumber,
                },
                tempo = model.Tempo,
                expenditureNo = model.ExpenditureNo,
                dispositionNo = model.DispositionNo,
                useVat = model.UseVat,
                remark = model.Remark,
                isUsed=model.IsUsed,
                isCL=model.IsCL,
                isDetail=model.IsDetail,
                localSalesContractId=model.LocalSalesContractId,
                salesContractNo=model.SalesContractNo,
                paymentType=model.PaymentType,
                isRejectedFinance=model.IsRejectedFinance,
                isRejectedShipping=model.IsRejectedShipping,
                isApproveFinance=model.IsApproveFinance,
                isApproveShipping=model.IsApproveShipping,
                rejectedReason=model.RejectedReason,
                approveFinanceBy=model.ApproveFinanceBy,
                approveFinanceDate=model.ApproveFinanceDate,
                approveShippingBy=model.ApproveShippingBy,
                approveShippingDate=model.ApproveShippingDate,
                items = (model.Items ?? new List<GarmentShippingLocalSalesNoteItemModel>()).Select(i => new GarmentShippingLocalSalesNoteItemViewModel
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

                    product = new ProductViewModel
                    {
                        id = i.ProductId,
                        code = i.ProductCode,
                        name = i.ProductName
                    },
                    quantity = i.Quantity,
                    uom = new UnitOfMeasurement
                    {
                        Id = i.UomId,
                        Unit = i.UomUnit
                    },
                    packageQuantity = i.PackageQuantity,
                    packageUom = new UnitOfMeasurement
                    {
                        Id = i.PackageUomId,
                        Unit = i.PackageUomUnit
                    },
                    price = i.Price,
                    localSalesContractItemId=i.LocalSalesContractItemId,
                    remark = i.Remark
                }).ToList()
            };

            return viewModel;
        }

        private GarmentShippingLocalSalesNoteModel MapToModel(GarmentShippingLocalSalesNoteViewModel vm)
        {
            var items = (vm.items ?? new List<GarmentShippingLocalSalesNoteItemViewModel>()).Select(i =>
            {
                i.product = i.product ?? new ProductViewModel();
                i.uom = i.uom ?? new UnitOfMeasurement();
                i.packageUom = i.packageUom ?? new UnitOfMeasurement();
                return new GarmentShippingLocalSalesNoteItemModel(i.localSalesContractItemId, i.product.id, i.product.code, i.product.name, i.quantity, i.uom.Id.GetValueOrDefault(), i.uom.Unit, i.price,i.packageQuantity,i.packageUom.Id.GetValueOrDefault(), i.packageUom.Unit, i.remark) { Id = i.Id };
            }).ToList();

            vm.transactionType = vm.transactionType ?? new TransactionType();
            vm.buyer = vm.buyer ?? new Buyer();
            vm.vat = vm.vat ?? new Vat();
            vm.bank = vm.bank ?? new BankAccount();
            return new GarmentShippingLocalSalesNoteModel(vm.salesContractNo, vm.localSalesContractId, vm.paymentType, GenerateNo(vm), vm.date.GetValueOrDefault(), vm.transactionType.id, vm.transactionType.code, vm.transactionType.name, vm.buyer.Id, vm.buyer.Code, vm.buyer.Name, vm.buyer.npwp, vm.buyer.KaberType, vm.tempo, vm.expenditureNo, vm.dispositionNo, vm.useVat, vm.vat.id, vm.vat.rate, vm.remark, vm.isUsed, vm.isCL, vm.isDetail, vm.isApproveShipping, vm.isApproveFinance, vm.approveShippingBy, vm.approveFinanceBy, vm.approveShippingDate, vm.approveFinanceDate, vm.isRejectedShipping, vm.isRejectedFinance, vm.rejectedReason, vm.bank.id, vm.bank.bankName, vm.bank.AccountNumber, items) { Id = vm.Id };
        }

        private string GenerateNo(GarmentShippingLocalSalesNoteViewModel vm)
        {
            var year = DateTime.Now.ToString("yy");

            var prefix = $"{year}/{(vm.transactionType.code ?? "").Trim().ToUpper()}/";

            var lastInvoiceNo = _repository.ReadAll().Where(w => w.NoteNo.StartsWith(prefix))
                .OrderByDescending(o => o.NoteNo)
                .Select(s => int.Parse(s.NoteNo.Replace(prefix, "")))
                .FirstOrDefault();
            var invoiceNo = $"{prefix}{(lastInvoiceNo + 1).ToString("D5")}";

            return invoiceNo;
        }

        public async Task<int> Create(GarmentShippingLocalSalesNoteViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            int Created = await _repository.InsertAsync(model);

            return Created;
        }

        public async Task<int> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public ListResult<GarmentShippingLocalSalesNoteViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "NoteNo", "BuyerCode", "BuyerName", "DispositionNo"
            };
            query = QueryHelper<GarmentShippingLocalSalesNoteModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingLocalSalesNoteModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingLocalSalesNoteModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentShippingLocalSalesNoteViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingLocalSalesNoteViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentShippingLocalSalesNoteViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            return await _repository.UpdateAsync(id, model);
        }

        public async Task<int> ApproveFinance(int id)
        {
            return await _repository.ApproveFinanceAsync(id);
        }

        public async Task<int> ApproveShipping(int id)
        {
            return await _repository.ApproveShippingAsync(id);
        }

        public async Task<int> RejectedShipping(int id, GarmentShippingLocalSalesNoteViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            return await _repository.RejectShippingAsync(id, model);
        }

        public async Task<int> RejectedFinance(int id, GarmentShippingLocalSalesNoteViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            return await _repository.RejectFinanceAsync(id, model);
        }

        public Buyer GetBuyer(int id)
        {
            string buyerUri = "master/garment-leftover-warehouse-buyers";
            IHttpClientService httpClient = (IHttpClientService)serviceProvider.GetService(typeof(IHttpClientService));

            var response = httpClient.GetAsync($"{ApplicationSetting.CoreEndpoint}{buyerUri}/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                Buyer viewModel = JsonConvert.DeserializeObject<Buyer>(result.GetValueOrDefault("data").ToString());
                return viewModel;
            }
            else
            {
                return null;
            }
        }

        public IQueryable<GarmentShippingLocalSalesNoteViewModel> ReadShippingLocalSalesNoteListNow(int month, int year)
        {
            var queryInv = _repository.ReadAll();

            var query = from a in queryInv
                        where a.Date.AddHours(7).Month == month && a.Date.AddHours(7).Year == year
                        && a.TransactionTypeCode != "SML" && a.TransactionTypeCode != "LMS"
                        select new GarmentShippingLocalSalesNoteViewModel
                        {
                            Id = a.Id,
                            date = a.Date,
                            noteNo = a.NoteNo,
                            useVat = a.UseVat,
                            buyer = new Buyer
                            {
                                Id = a.BuyerId,
                                Code = a.BuyerCode,
                                Name = a.BuyerName,
                                npwp = a.BuyerNPWP
                            },
                            items = a.Items.Select(s => new GarmentShippingLocalSalesNoteItemViewModel
                            {
                                price = s.Price,
                                quantity = s.Quantity
                            }).ToList()
                        };

            return query.AsQueryable();
            //throw new NotImplementedException();
            }        
        public IQueryable<LocalSalesNoteFinanceReportViewModel> ReadSalesNoteForFinance(string type, int month, int year, string buyer)
        {
            var salesNote = _repository.ReadAll();

            DateTime dateFrom = DateTime.MinValue;
            DateTime dateTo = month == 1 ? new DateTime(year, 1, 1) : new DateTime(year, month, 1);
            if (type == "now")
            {
                dateFrom = new DateTime(year, month, 1);
                dateTo = month == 12 ? new DateTime(year + 1, 1, 1) : new DateTime(year, month + 1, 1);
            }  
            
            var query = from a in salesNote
                        where a.Date.AddHours(7).Date >= dateFrom && a.Date.AddHours(7).Date < dateTo 
                    && a.BuyerCode==(buyer!=null ? buyer : a.BuyerCode)
                    && a.TransactionTypeCode!="SML" && a.TransactionTypeCode!="LMS"
                    select new LocalSalesNoteFinanceReportViewModel
                    {
                        BuyerCode = a.BuyerCode,
                        BuyerName = a.BuyerName,
                        Amount =a.UseVat ? (a.Items.Sum(b=>b.Price * b.Quantity) * 110/100) : a.Items.Sum(b => b.Price * b.Quantity),
                        SalesNoteId = a.Id,
                        SalesNoteNo = a.NoteNo,
                        Date=a.Date
                    };

            return query.AsQueryable();
        }
        
        public IQueryable<GarmentShippingLocalSalesNoteViewModel> ReadLocalSalesDebtor(string type, int month, int year)
        {
            var salesNote = _repository.ReadAll();

            DateTime dateFrom = DateTime.MinValue;
            DateTime dateTo = month == 1 ? new DateTime(year, 1, 1) : new DateTime(year, month, 1);
            if (type == "now")
            {
                dateFrom = new DateTime(year, month, 1);
                dateTo = month == 12 ? new DateTime(year + 1, 1, 1) : new DateTime(year, month + 1, 1);
            }


            var query = from a in salesNote
                        where a.Date.AddHours(7).Date >= dateFrom && a.Date.AddHours(7).Date < dateTo
                        && a.TransactionTypeCode != "SML" && a.TransactionTypeCode != "LMS"
                        select new GarmentShippingLocalSalesNoteViewModel
                        {
                            buyer = new Buyer
                            {
                                Id = a.BuyerId,
                                Name = a.BuyerName,
                                Code = a.BuyerCode
                            },
                            Amount = a.UseVat ? (a.Items.Sum(b => b.Price * b.Quantity) * 110 / 100) : a.Items.Sum(b => b.Price * b.Quantity),
                            Id = a.Id,
                            noteNo = a.NoteNo,
                            date = a.Date,
                            tempo = a.Tempo
                        };

            return query.AsQueryable();
        }      
    }
}
