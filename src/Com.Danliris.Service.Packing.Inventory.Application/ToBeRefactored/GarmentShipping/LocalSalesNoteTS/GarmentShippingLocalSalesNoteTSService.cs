using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNoteTS;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNoteTS;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNoteTS
{
    public class GarmentShippingLocalSalesNoteTSService : IGarmentShippingLocalSalesNoteTSService
    {
        private readonly IGarmentShippingLocalSalesNoteTSRepository _repository;
        protected readonly ILogHistoryRepository logHistoryRepository;
        private readonly IServiceProvider serviceProvider;

        public GarmentShippingLocalSalesNoteTSService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentShippingLocalSalesNoteTSRepository>();
            logHistoryRepository = serviceProvider.GetService<ILogHistoryRepository>();
            this.serviceProvider = serviceProvider;
        }

        private GarmentShippingLocalSalesNoteTSViewModel MapToViewModel(GarmentShippingLocalSalesNoteTSModel model)
        {
            GarmentShippingLocalSalesNoteTSViewModel viewModel = new GarmentShippingLocalSalesNoteTSViewModel
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
                //transactionType = new TransactionType
                //{
                //    id = model.TransactionTypeId,
                //    code = model.TransactionTypeCode,
                //    name = model.TransactionTypeName
                //},
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
                //expenditureNo = model.ExpenditureNo,
                //dispositionNo = model.DispositionNo,
                useVat = model.UseVat,
                remark = model.Remark,
                isUsed=model.IsUsed,
                isCL=model.IsCL,
                isDetail=model.IsDetail,
                salesContractId=model.SalesContractId,
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
                //isSubconPackingList = model.IsSubconPackingList,
                items = (model.Items ?? new List<GarmentShippingLocalSalesNoteTSItemModel>()).Select(i => new GarmentShippingLocalSalesNoteTSItemViewModel
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

                    //product = new ProductViewModel
                    //{
                    //    id = i.ProductId,
                    //    code = i.ProductCode,
                    //    name = i.ProductName
                    //},
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
                    //localSalesContractItemId=i.LocalSalesContractItemId,
                    remark = i.Remark,
                    invoiceNo = i.InvoiceNo,
                    packingListId = i.PackingListId
                }).ToList()
            };

            return viewModel;
        }

        private GarmentShippingLocalSalesNoteTSModel MapToModel(GarmentShippingLocalSalesNoteTSViewModel vm)
        {
            var items = (vm.items ?? new List<GarmentShippingLocalSalesNoteTSItemViewModel>()).Select(i =>
            {
                //i.product = i.product ?? new ProductViewModel();
                i.uom = i.uom ?? new UnitOfMeasurement();
                i.packageUom = i.packageUom ?? new UnitOfMeasurement();
                return new GarmentShippingLocalSalesNoteTSItemModel(i.packingListId,/* i.product.id, i.product.code, i.product.name,*/ i.quantity, i.uom.Id.GetValueOrDefault(), i.uom.Unit, i.price,i.packageQuantity,i.packageUom.Id.GetValueOrDefault(), i.packageUom.Unit, i.remark,i.invoiceNo) { Id = i.Id };
            }).ToList();

            //vm.transactionType = vm.transactionType ?? new TransactionType();
            vm.buyer = vm.buyer ?? new Buyer();
            vm.vat = vm.vat ?? new Vat();
            vm.bank = vm.bank ?? new BankAccount();
            return new GarmentShippingLocalSalesNoteTSModel(vm.salesContractNo, vm.salesContractId, vm.paymentType, GenerateNo(vm), vm.date.GetValueOrDefault(),/* vm.transactionType.id, vm.transactionType.code, vm.transactionType.name,*/ vm.buyer.Id, vm.buyer.Code, vm.buyer.Name, vm.buyer.npwp, vm.buyer.KaberType, vm.tempo, /*vm.expenditureNo, vm.dispositionNo,*/ vm.useVat, vm.vat.id, vm.vat.rate, vm.remark, vm.isUsed, vm.isCL, vm.isDetail, vm.isApproveShipping, vm.isApproveFinance, vm.approveShippingBy, vm.approveFinanceBy, vm.approveShippingDate, vm.approveFinanceDate, vm.isRejectedShipping, vm.isRejectedFinance, vm.rejectedReason, vm.bank.id, vm.bank.bankName, vm.bank.AccountNumber, items) { Id = vm.Id };
        }

        private string GenerateNo(GarmentShippingLocalSalesNoteTSViewModel vm)
        {
            var year = DateTime.Now.ToString("yy");

            //var prefix = $"{year}/{(vm.transactionType.code ?? "").Trim().ToUpper()}/";
            var prefix = $"{year}/LJS";
            var lastInvoiceNo = _repository.ReadAll().Where(w => w.NoteNo.StartsWith(prefix))
                .OrderByDescending(o => o.NoteNo)
                .Select(s => int.Parse(s.NoteNo.Replace(prefix, "")))
                .FirstOrDefault();
            var invoiceNo = $"{prefix}{(lastInvoiceNo + 1).ToString("D5")}";

            return invoiceNo;
        }

        public async Task<int> Create(GarmentShippingLocalSalesNoteTSViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Create Nota Penjualan Lokal Terima Subcon - " + model.NoteNo);

            int Created = await _repository.InsertAsync(model);

            return Created;
        }

        public async Task<int> Delete(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Delete Nota Penjualan Lokal Terima Subcon - " + data.NoteNo);

            return await _repository.DeleteAsync(id);
        }

        public ListResult<GarmentShippingLocalSalesNoteTSViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "NoteNo", "BuyerCode", "BuyerName", "DispositionNo"
            };
            query = QueryHelper<GarmentShippingLocalSalesNoteTSModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingLocalSalesNoteTSModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingLocalSalesNoteTSModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentShippingLocalSalesNoteTSViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingLocalSalesNoteTSViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentShippingLocalSalesNoteTSViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Update Nota Penjualan Lokal Terima Subcon - " + model.NoteNo);

            return await _repository.UpdateAsync(id, model);
        }

        //public async Task<int> ApproveFinance(int id)
        //{
        //    var data = await _repository.ReadByIdAsync(id);

        //    //Add Log History
        //    await logHistoryRepository.InsertAsync("SHIPPING", "Approve Nota Penjualan Lokal Terima Subcon - " + data.NoteNo);

        //    return await _repository.ApproveFinanceAsync(id);
        //}

        //public async Task<int> ApproveShipping(int id)
        //{
        //    return await _repository.ApproveShippingAsync(id);
        //}

        //public async Task<int> RejectedShipping(int id, GarmentShippingLocalSalesNoteTSViewModel viewModel)
        //{
        //    var data = await _repository.ReadByIdAsync(id);

        //    //Add Log History
        //    await logHistoryRepository.InsertAsync("SHIPPING", "Reject Nota Penjualan Lokal Terima Subcon - " + data.NoteNo);

        //    var model = MapToModel(viewModel);

        //    return await _repository.RejectShippingAsync(id, model);
        //}

        //public async Task<int> RejectedFinance(int id, GarmentShippingLocalSalesNoteTSViewModel viewModel)
        //{
        //    var model = MapToModel(viewModel);

        //    return await _repository.RejectFinanceAsync(id, model);
        //}

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


        //public IQueryable<GarmentShippingLocalSalesNoteTSViewModel> ReadShippingLocalSalesNoteListNow(int month, int year)
        //{
        //    var queryInv = _repository.ReadAll();

        //    var query = from a in queryInv
        //                where a.Date.AddHours(7).Month == month && a.Date.AddHours(7).Year == year
        //                && a.TransactionTypeCode != "SML" && a.TransactionTypeCode != "LMS"
        //                select new GarmentShippingLocalSalesNoteTSViewModel
        //                {
        //                    Id = a.Id,
        //                    date = a.Date,
        //                    noteNo = a.NoteNo,
        //                    useVat = a.UseVat,
        //                    buyer = new Buyer
        //                    {
        //                        Id = a.BuyerId,
        //                        Code = a.BuyerCode,
        //                        Name = a.BuyerName,
        //                        npwp = a.BuyerNPWP
        //                    },
        //                    items = a.Items.Select(s => new GarmentShippingLocalSalesNoteTSItemViewModel
        //                    {
        //                        price = s.Price,
        //                        quantity = s.Quantity
        //                    }).ToList()
        //                };

        //    return query.AsQueryable();
        //    //throw new NotImplementedException();
        //    }        
        //public IQueryable<LocalSalesNoteFinanceReportViewModel> ReadSalesNoteForFinance(string type, int month, int year, string buyer)
        //{
        //    var salesNote = _repository.ReadAll();

        //    DateTime dateFrom = DateTime.MinValue;
        //    DateTime dateTo = month == 1 ? new DateTime(year, 1, 1) : new DateTime(year, month, 1);
        //    if (type == "now")
        //    {
        //        dateFrom = new DateTime(year, month, 1);
        //        dateTo = month == 12 ? new DateTime(year + 1, 1, 1) : new DateTime(year, month + 1, 1);
        //    }  

        //    var query = from a in salesNote
        //                where a.Date.AddHours(7).Date >= dateFrom && a.Date.AddHours(7).Date < dateTo 
        //            && a.BuyerCode==(buyer!=null ? buyer : a.BuyerCode)
        //            && a.TransactionTypeCode!="SML" && a.TransactionTypeCode!="LMS"
        //            select new LocalSalesNoteFinanceReportViewModel
        //            {
        //                BuyerCode = a.BuyerCode,
        //                BuyerName = a.BuyerName,
        //                Amount =a.UseVat ? (a.Items.Sum(b=>b.Price * b.Quantity) * 110/100) : a.Items.Sum(b => b.Price * b.Quantity),
        //                SalesNoteId = a.Id,
        //                SalesNoteNo = a.NoteNo,
        //                Date=a.Date
        //            };

        //    return query.AsQueryable();
        //}

        //public IQueryable<GarmentShippingLocalSalesNoteTSViewModel> ReadLocalSalesDebtor(string type, int month, int year)
        //{
        //    var salesNote = _repository.ReadAll();

        //    DateTime dateFrom = DateTime.MinValue;
        //    DateTime dateTo = month == 1 ? new DateTime(year, 1, 1) : new DateTime(year, month, 1);
        //    if (type == "now")
        //    {
        //        dateFrom = new DateTime(year, month, 1);
        //        dateTo = month == 12 ? new DateTime(year + 1, 1, 1) : new DateTime(year, month + 1, 1);
        //    }


        //    var query = from a in salesNote
        //                where a.Date.AddHours(7).Date >= dateFrom && a.Date.AddHours(7).Date < dateTo
        //                && a.TransactionTypeCode != "SML" && a.TransactionTypeCode != "LMS"
        //                select new GarmentShippingLocalSalesNoteTSViewModel
        //                {
        //                    buyer = new Buyer
        //                    {
        //                        Id = a.BuyerId,
        //                        Name = a.BuyerName,
        //                        Code = a.BuyerCode
        //                    },
        //                    Amount = a.UseVat ? (a.Items.Sum(b => b.Price * b.Quantity) * 110 / 100) : a.Items.Sum(b => b.Price * b.Quantity),
        //                    Id = a.Id,
        //                    noteNo = a.NoteNo,
        //                    date = a.Date,
        //                    tempo = a.Tempo
        //                };

        //    return query.AsQueryable();
        //}      
    }
}
