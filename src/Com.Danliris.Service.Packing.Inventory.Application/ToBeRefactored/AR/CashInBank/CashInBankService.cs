
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AR.CashInBank;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.CashInBank
{
    public class CashInBankService : ICashInBankService
    {
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityService;
        private const string UserAgent = "finance-service";

        public CashInBankService(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _identityService = serviceProvider.GetService<IIdentityProvider>();
        }

        public ReadResponse<CashInBankViewModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            var query = _dbContext.AR_CashInBank.AsQueryable();

            var searchAttributes = new List<string>()
            {
                "InvoiceNo",
            };

            query = QueryHelper<CashInBankModel>.Search(query, searchAttributes, keyword);

            var filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<CashInBankModel>.Filter(query, filterDictionary);

            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<CashInBankModel>.Order(query, orderDictionary);

            var pageable = new Pageable<CashInBankModel>(query, page - 1, size);

            var data = pageable.Data.Select(entity => new CashInBankViewModel()
            {
                ReceiptNo = entity.ReceiptNo,
                ReceiptDate = entity.ReceiptDate != null ? entity.ReceiptDate.Value.ToString("dd-MM-yyyy") : "",
                BuyerCode = entity.BuyerCode,
                InvoiceNo = entity.InvoiceNo,
                Receipt = new ReceiptViewModel()
                {
                    ReceiptAmount = entity.ReceiptAmount,
                    ReceiptKurs = entity.ReceiptKurs,
                    ReceiptTotalAmount = entity.ReceiptTotalAmount
                },
                Liquid = new LiquidAmountViewModel()
                {
                    LiquidAmount = entity.LiquidAmount,
                    LiquidTotalAmount = entity.LiquidTotalAmount
                },
                BookBalance = new BookBalanceViewModel()
                {
                    BookBalanceKurs = entity.BookBalanceKurs,
                    BookBalanceTotalAmount = entity.BookBalanceTotalAmount
                },
                DifferenceKurs = entity.DifferenceKurs,
                Cost = new CostViewModel()
                {
                    COA = entity.COA,
                    UnitCode = entity.UnitCode,
                    Remark = entity.Remark,
                    SupportingDocument = entity.SupportingDocument,
                    Amount = entity.Amount,
                    TotalAmount = entity.TotalAmount
                },
                Month = entity.Month

            }).OrderByDescending(x=> x.ReceiptDate).ThenBy(x => x.ReceiptNo).ThenByDescending(x => x.Receipt.ReceiptAmount).ToList();

            int totalData = pageable.TotalCount;

            return new ReadResponse<CashInBankViewModel>(data, totalData, page,size);
        }

        public Task<int> CreateAsync(CashInBankModel model)
        {

            model.FlagForCreate(_identityService.Username, UserAgent);

            _dbContext.AR_CashInBank.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> UploadExcelAsync(List<CashInBankModel> listData)
        {
            int created = 0;
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var data in listData)
                    {
                        data.FlagForCreate(_identityService.Username, UserAgent);
                        _dbContext.AR_CashInBank.Add(data);
                        created += await _dbContext.SaveChangesAsync();
                    }

                    transaction.Commit();
                    return created;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }

        }
    }
}
