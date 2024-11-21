using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.LocalSalesNote.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalMDSalesNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.GarmentShipping.LocalMDSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentMD.LocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.ShippingLocalSalesNote
{
    public class GarmentMDLocalSalesNoteService : IGarmentMDLocalSalesNoteService
    {
        private readonly IGarmentMDLocalSalesNoteRepository _repository;
        protected readonly ILogHistoryRepository logHistoryRepository;
        private readonly IServiceProvider serviceProvider;
        protected readonly IHttpClientService _http;
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider iIdentityProvider;
        public GarmentMDLocalSalesNoteService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentMDLocalSalesNoteRepository>();
            logHistoryRepository = serviceProvider.GetService<ILogHistoryRepository>();
            this.serviceProvider = serviceProvider;
            _http = serviceProvider.GetService<IHttpClientService>();
            _dbContext = serviceProvider.GetService<PackingInventoryDbContext>();
            iIdentityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        private GarmentMDLocalSalesNoteViewModel MapToViewModel(GarmentMDLocalSalesNoteModel model)
        {
            GarmentMDLocalSalesNoteViewModel viewModel = new GarmentMDLocalSalesNoteViewModel
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
                isSubconPackingList = model.IsSubconPackingList,
                bcNo = model.BCNo,
                bcDate = model.BCDate,
                bcType = model.BCType,
                items = (model.Items ?? new List<GarmentMDLocalSalesNoteItemModel>()).Select(i => new GarmentMDLocalSalesNoteItemViewModel
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

                    comodityName = i.ComodityName,
                    quantity = i.Quantity,
                    uom = new UnitOfMeasurement
                    {
                        Id = i.UomId,
                        Unit = i.UomUnit
                    },
                  
                    price = i.Price,
                    remark = i.Remark,
                    details = (i.Details ?? new List<GarmentMDLocalSalesNoteDetailModel>()).Select(s => new GarmentMDLocalSalesNoteDetailViewModel
                    {
                        Active = s.Active,
                        Id = s.Id,
                        CreatedAgent = s.CreatedAgent,
                        CreatedBy = s.CreatedBy,
                        CreatedUtc = s.CreatedUtc,
                        DeletedAgent = s.DeletedAgent,
                        DeletedBy = s.DeletedBy,
                        DeletedUtc = s.DeletedUtc,
                        IsDeleted = s.IsDeleted,
                        LastModifiedAgent = s.LastModifiedAgent,
                        LastModifiedBy = s.LastModifiedBy,
                        LastModifiedUtc = s.LastModifiedUtc,

                        bonNo = s.BonNo,
                        quantity = s.Quantity,
                        uom = new UnitOfMeasurement
                        {
                            Id = s.UomId,
                            Unit = s.UomUnit
                        },
                        bonFrom = s.BonFrom,
                    }).ToList()
                }).ToList()
            };

            return viewModel;
        }

        private GarmentMDLocalSalesNoteModel MapToModel(GarmentMDLocalSalesNoteViewModel vm)
        {
            var items = (vm.items ?? new List<GarmentMDLocalSalesNoteItemViewModel>()).Select(i =>
            {
                i.uom = i.uom ?? new UnitOfMeasurement();
                var detail = (i.details ?? new List<GarmentMDLocalSalesNoteDetailViewModel>()).Select(s =>
                {
                    s.uom = s.uom ?? new UnitOfMeasurement();
                    return new GarmentMDLocalSalesNoteDetailModel(s.bonNo, s.quantity, s.uom.Id.GetValueOrDefault(), s.uom.Unit, s.bonFrom) { Id = s.Id };
                }).ToList();
                return new GarmentMDLocalSalesNoteItemModel(i.localSalesContractId, i.comodityName, i.quantity, i.uom.Id.GetValueOrDefault(), i.uom.Unit, i.price, i.remark, detail) { Id = i.Id };
            }).ToList();

            vm.transactionType = vm.transactionType ?? new TransactionType();
            vm.buyer = vm.buyer ?? new Buyer();
            vm.vat = vm.vat ?? new Vat();
            vm.bank = vm.bank ?? new BankAccount();
            return new GarmentMDLocalSalesNoteModel(vm.salesContractNo, vm.localSalesContractId, vm.paymentType, GenerateNo(vm), vm.date.GetValueOrDefault(), vm.transactionType.id, vm.transactionType.code, vm.transactionType.name, vm.buyer.Id, vm.buyer.Code, vm.buyer.Name, vm.buyer.npwp, vm.buyer.KaberType, vm.tempo, vm.expenditureNo, vm.dispositionNo, vm.useVat, vm.vat.id, vm.vat.rate, vm.remark, vm.isUsed, vm.isCL, vm.isDetail, vm.isApproveShipping, vm.isApproveFinance, vm.approveShippingBy, vm.approveFinanceBy, vm.approveShippingDate, vm.approveFinanceDate, vm.isRejectedShipping, vm.isRejectedFinance, vm.rejectedReason, vm.bank.id, vm.bank.bankName, vm.bank.AccountNumber,vm.bcNo,vm.bcType,vm.bcDate, items) { Id = vm.Id };
        }

        private string GenerateNo(GarmentMDLocalSalesNoteViewModel vm)
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

        public async Task<int> Create(GarmentMDLocalSalesNoteViewModel viewModel)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var model = MapToModel(viewModel);

                    //Add Log History
                    await logHistoryRepository.InsertAsync("MERCHANDISER", "Create Nota Penjualan Lokal - " + model.NoteNo);

                    int Created = await _repository.InsertAsync(model);

                    //take all data from detail that have bonFrom = "SISA" and comodityName = "BARANG JADI"

                    var bonLeftOver = model.Items.Where(s => s.ComodityName == "BARANG JADI").SelectMany(s => s.Details).Where(r => r.BonFrom == "SISA").Select(s => s.BonNo).ToHashSet();
                    //var bonLeftOver = model.Items.Where(s => s.ComodityName == "BARANG JADI").SelectMany(s => s.Details).Where(r => r.BonNo == "SISA").Select(s => s.BonNo).ToHashSet();

                    //update status bon production
                    if (bonLeftOver.Count > 0)
                    {
                        await PutIsLSNInventory(bonLeftOver.ToList(), true);
                    }

                    transaction.Commit();

                    return Created;
                }catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
         
        }

        public async Task<int> Delete(int id)
        {

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var data = await _repository.ReadByIdAsync(id);

                    //Add Log History
                    await logHistoryRepository.InsertAsync("MERCHANDISER", "Delete Nota Penjualan Lokal - " + data.NoteNo);

                    var bonLeftOver = data.Items.Where(s => s.ComodityName == "BARANG JADI").SelectMany(s => s.Details).Where(r => r.BonFrom == "SISA").Select(s => s.BonNo).ToHashSet();
                    //var bonLeftOver = model.Items.Where(s => s.ComodityName == "BARANG JADI").SelectMany(s => s.Details).Where(r => r.BonNo == "SISA").Select(s => s.BonNo).ToHashSet();

                    var Deleted = await _repository.DeleteAsync(id);

                    //update status bon production
                    if (bonLeftOver.Count > 0)
                    {
                        await PutIsLSNInventory(bonLeftOver.ToList(), false);
                    }
                    transaction.Commit();

                    return Deleted;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public ListResult<GarmentMDLocalSalesNoteViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "NoteNo", "BuyerCode", "BuyerName", "DispositionNo"
            };
            query = QueryHelper<GarmentMDLocalSalesNoteModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentMDLocalSalesNoteModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentMDLocalSalesNoteModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentMDLocalSalesNoteViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentMDLocalSalesNoteViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentMDLocalSalesNoteViewModel viewModel)
        {
            var model = MapToModel(viewModel);

            //Add Log History
            await logHistoryRepository.InsertAsync("MERCHANDISER", "Update Nota Penjualan Lokal - " + model.NoteNo);

            return await _repository.UpdateAsync(id, model);
        }

        protected async Task<string> PutIsLSNInventory(List<string> nos, bool isLSN)
        {
            var garmentUnitExpenditureNoteUri = ApplicationSetting.InventoryEndpoint + $"garment/leftover-warehouse-expenditures/finished-goods/is-local-sales-note";
            var garmentUnitExpenditureNoteResponse = await _http.PutAsync(garmentUnitExpenditureNoteUri, iIdentityProvider.Token, new StringContent(JsonConvert.SerializeObject(new { bonNo = nos, isLSN = isLSN }), Encoding.UTF8, "application/json"));

            return garmentUnitExpenditureNoteResponse.EnsureSuccessStatusCode().ToString();
        }


        //public async Task<int> ApproveFinance(int id)
        //{
        //    var data = await _repository.ReadByIdAsync(id);

        //    //Add Log History
        //    await logHistoryRepository.InsertAsync("MERCHANDISER", "Approve Nota Penjualan Lokal - " + data.NoteNo);

        //    return await _repository.ApproveFinanceAsync(id);
        //}

        //public async Task<int> ApproveShipping(int id)
        //{
        //    return await _repository.ApproveShippingAsync(id);
        //}

        //public async Task<int> RejectedShipping(int id, GarmentMDLocalSalesNoteViewModel viewModel)
        //{
        //    var data = await _repository.ReadByIdAsync(id);

        //    //Add Log History
        //    await logHistoryRepository.InsertAsync("MERCHANDISER", "Reject Nota Penjualan Lokal - " + data.NoteNo);

        //    var model = MapToModel(viewModel);

        //    return await _repository.RejectShippingAsync(id, model);
        //}

        //public async Task<int> RejectedFinance(int id, GarmentMDLocalSalesNoteViewModel viewModel)
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

        public IQueryable<GarmentMDLocalSalesNoteViewModel> ReadShippingLocalSalesNoteListNow(int month, int year)
        {
            var queryInv = _repository.ReadAll();

            var query = from a in queryInv
                        where a.Date.AddHours(7).Month == month && a.Date.AddHours(7).Year == year
                        && a.TransactionTypeCode != "SML" && a.TransactionTypeCode != "LMS"
                        select new GarmentMDLocalSalesNoteViewModel
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
                            items = a.Items.Select(s => new GarmentMDLocalSalesNoteItemViewModel
                            {
                                price = s.Price,
                                quantity = s.Quantity
                            }).ToList()
                        };

            return query.AsQueryable();
            //throw new NotImplementedException();
            }
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

        //public IQueryable<GarmentMDLocalSalesNoteViewModel> ReadLocalSalesDebtor(string type, int month, int year)
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
        //                select new GarmentMDLocalSalesNoteViewModel
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
