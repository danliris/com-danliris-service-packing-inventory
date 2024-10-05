
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AR.DownPayment;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;


namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.DownPayment
{
    public class DownPaymentService : IDownPaymentService
    {
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityService;
        private readonly List<string> _alphabets;
        private const string UserAgent = "finance-service";

        public DownPaymentService(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _identityService = serviceProvider.GetService<IIdentityProvider>();

            _alphabets = GetAlphabets();
        }

        public List<string> GetAlphabets()
        {
            //Declare string container for alphabet
            var result = new List<string>();

            //Loop through the ASCII characters 65 to 90
            for (int i = 65; i <= 90; i++)
            {
                // Convert the int to a char to get the actual character behind the ASCII code
                result.Add(((char)i).ToString());
            }

            return result;
        }

        public ReadResponse<DownPaymentList> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            var query = _dbContext.AR_DownPayments.Where(x => !x.IsDeleted).AsQueryable();

            var searchAttributes = new List<string>()
            {
                "MemoNo",
                "ReceiptNo",
                "InvoiceNo",
            };

            query = QueryHelper<DownPaymentModel>.Search(query, searchAttributes, keyword);

            var filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DownPaymentModel>.Filter(query, filterDictionary);

            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DownPaymentModel>.Order(query, orderDictionary);

            var pageable = new Pageable<DownPaymentModel>(query, page - 1, size);
            var data = pageable.Data.Select(entity => new DownPaymentList()
            {
                MemoNo = entity.MemoNo,
                Remark = entity.Remark,
                ReceiptNo = entity.ReceiptNo,
                Date = entity.Date,
                OffsetDate = entity.OffsetDate,
                InvoiceNo = entity.InvoiceNo,
                Amount = entity.Amount,
                Kurs = entity.Kurs,
                TotalAmount = entity.TotalAmount,
                Month = entity.Month,

            }).ToList();

            int totalData = pageable.TotalCount;

            return new ReadResponse<DownPaymentList>(data, totalData, page, size);
        }

        public Task<int> CreateAsync(DownPaymentModel model)
        {
            //model.DocumentNo = GetDownPaymentNo(model);

            model.FlagForCreate(_identityService.Username, UserAgent);

            _dbContext.AR_DownPayments.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> UploadExcelAsync(List<DownPaymentModel> listData)
        {
            int created = 0;
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var downPayment in listData)
                    {
                        downPayment.FlagForCreate(_identityService.Username, UserAgent);
                        _dbContext.AR_DownPayments.Add(downPayment);
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

        private string GetDownPaymentNo(DownPaymentModel model)
        {
            var now = DateTime.Now;
            var year = now.ToString("yy");
            var month = now.ToString("MM");

            //var documentNo = $"{year}{month}MEM{model.BuyerCode}";

            //var countSameDocumentNo = _dbContext.AR_DownPayments.Count(entity => entity.DocumentNo.Contains(documentNo));

            //if (countSameDocumentNo > 0)
            //{
            //    documentNo += _alphabets[countSameDocumentNo];
            //}

            //return documentNo;
            return "documentNo";
        }

        public Task<DownPaymentModel> ReadByIdAsync(int id)
        {
            return _dbContext.AR_DownPayments.Where(entity => entity.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> UpdateAsync(int id, DownPaymentModel model)
        {
            model.FlagForUpdate(_identityService.Username, UserAgent);

            _dbContext.AR_DownPayments.Update(model);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbContext.AR_DownPayments.Where(entity => entity.Id == id).FirstOrDefault();

            if (model != null)
            {
                model.FlagForDelete(_identityService.Username, UserAgent);

                _dbContext.AR_DownPayments.Update(model);
            }

            return _dbContext.SaveChangesAsync();
        }
    }
}